using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using Microsoft.EntityFrameworkCore;
using GroupAssignment1.ViewModels;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using GroupAssignment1.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using GroupAssignment1.Areas.Identity.Data;

namespace GroupAssignment1.Controllers
{
    public class OrderController : Controller
    {
        private readonly HousingDbContext _orderDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(HousingDbContext orderDbContext,UserManager<ApplicationUser> userManager, IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderDbContext = orderDbContext;
            _orderRepository = orderRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Table()
        {
            var orders = await _orderRepository.GetAll();
            if (orders == null)
            {
                _logger.LogError("[OrderController] Order list not found while executing _orderRepository.GetAll()");
                return NotFound("Order list not found");
            }
            var orderListViewModel = new OrderListViewModel(orders, "Table");
            return View(orderListViewModel);
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> TableUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _logger.LogError("[OrderController] User not found while executing _userManager.GetUserAsync(HttpContext.User)");
                return NotFound("User list not found");
            }

            var orders = await _orderRepository.GetAll();
            if (orders == null)
            {
                _logger.LogError("[OrderController] Order list not found while executing _orderRepository.GetAll()");
                return NotFound("Order list not found");
            }

            var filteredOrders = orders.Where(order => order.CustomerId == user.Id);
            
            if (filteredOrders == null)
            {
                _logger.LogError("[OrderController] Order list not found for user while executing _orderRepository.GetAll()");
                return NotFound("Order list for user not found");
            }

            var orderListViewModel = new OrderListViewModel(filteredOrders, "Table");
            return View(orderListViewModel);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> CreateOrder(int id)
        {
            var housing = await _orderRepository.GetHousingById(id);

            if (housing == null)
            {
                _logger.LogError("[OrderController] Housing not found while executing _orderRepository.GetHousingById()");
                return NotFound("Housing list not found");
            }

            ViewData["Housing"] = housing;
            
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _logger.LogError("[OrderController] User not found while executing _userManager.GetUserAsync(HttpContext.User)");
                return NotFound("User not found");
            }

            var newOrder = new Order();
            newOrder.StartDate = order.StartDate;
            newOrder.EndDate = order.EndDate;
            newOrder.HousingId = order.HousingId;
            newOrder.CustomerId = user.Id;

            TimeSpan difference = order.EndDate - order.StartDate;
            
            var currentHousing = await _orderRepository.GetHousingById(newOrder.HousingId);
            if (currentHousing == null)
            {
                _logger.LogError("[OrderController] Housing not found while executing _orderRepository.GetHousingById()");
                return NotFound("Housing list not found @Post");
            }
           
            newOrder.TotalPrice = difference.Days * currentHousing.Rent;


            if (order.StartDate >= order.EndDate)
            {
                ModelState.AddModelError(nameof(order.StartDate),
                    "Start Date can't be larger than or alike End Date");
                ModelState.AddModelError(nameof(order.EndDate),
                    "End Date can't be less than or alike Start Date");
            }
            if (ModelState.IsValid)
            {
                bool returnOk = await _orderRepository.CreateOrder(newOrder);
                if (returnOk)
                {
                    return RedirectToAction("ConfirmOrder", "Transaction", newOrder);
                }
            }
            _logger.LogWarning("[OrderController] Order creation failed {@order}", order);
            return View(order);
        }
    }
}
