using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using Microsoft.EntityFrameworkCore;
using GroupAssignment1.ViewModels;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using GroupAssignment1.DAL;
using System.ComponentModel.DataAnnotations;

namespace GroupAssignment1.Controllers
{
    public class OrderController : Controller
    {
        private readonly HousingDbContext _orderDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(HousingDbContext orderDbContext, IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderDbContext = orderDbContext;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        

        public async Task<IActionResult> Table()
        {
            var orders = await _orderRepository.GetAll();
            if (orders == null)
            {
                _logger.LogError("[HousingController] Housing list not found while executing _HousingRepository.GetAll()");
                return NotFound("Housing list not found");
            }
            var orderListViewModel = new OrderListViewModel(orders, "Table");
            return View(orderListViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateOrder(int id)
        {
            var housing = await _orderRepository.GetHousingById(id);
            

            if (housing == null)
            {
                _logger.LogError("[OrderController] Housing not found while executing _OrderRepository.GetHousingById()");
                return NotFound("Housing list not found");
            }

            ViewData["Housing"] = housing;
            

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(Order order)
        {

            var newOrder = new Order();
            newOrder.StartDate = order.StartDate;
            newOrder.EndDate = order.EndDate;
            newOrder.CustomerId = order.CustomerId;
            newOrder.HousingId = order.HousingId;

            TimeSpan difference = order.EndDate - order.StartDate;
            var currentHousing = await _orderRepository.GetHousingById(order.HousingId);

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
                    return RedirectToAction("Index", "Home", new {area = "Home"});
                }
            } 
            
            _logger.LogWarning("[OrderController] Order creation failed {@order}", order);
            return View(order);
        }
    }
}
