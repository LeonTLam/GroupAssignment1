using GroupAssignment1.Areas.Identity.Data;
using GroupAssignment1.DAL;
using GroupAssignment1.Models;
using GroupAssignment1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupAssignment1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly HousingDbContext _transactionDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(HousingDbContext transactionDbContext,IOrderRepository orderRepository, ITransactionRepository transactionRepository, ILogger<TransactionController> logger, UserManager<ApplicationUser> userManager)
        {
            _transactionDbContext = transactionDbContext;
            _orderRepository = orderRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Table()
        {
            var transcations = await _transactionRepository.GetAll();
            if (transcations == null)
            {
                _logger.LogError("[TransactionController] Transaction list not found while executing _transactionRepository.GetAll()");
                return NotFound("Transaction list not found");
            }
            var transactionViewModel = new TransactionListViewModel(transcations, "Table");
            return View(transactionViewModel);
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> TableUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _logger.LogError("[TransactionController] User not found while executing _userManager.GetUserAsync(HttpContext.User)");
                return NotFound("User list not found");
            }

            var transactions = await _transactionRepository.GetAll();
            if (transactions == null)
            {
                _logger.LogError("[TransactionController] Transaction list not found while executing _transactionRepository.GetAll()");
                return NotFound("Transaction list not found");
            }

            var filteredTransactions = transactions.Where(transaction => transaction.CustomerId == user.Id);

            if (filteredTransactions == null)
            {
                _logger.LogError("[OrderController] Transaction list not found for user while executing _transactionRepository.GetAll()");
                return NotFound("Transaction list for user not found");
            }

            var transactionViewModel = new TransactionListViewModel(filteredTransactions, "Table");
            return View(transactionViewModel);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> ConfirmOrder(Order order)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _logger.LogError("[OrderController] User not found while executing _userManager.GetUserAsync(HttpContext.User)");
                return NotFound("User not found");
            }

            if (order == null)
            {
                _logger.LogError("[TransactionController] Order not available while executing _orderController.CreateOrder(order)");
                return NotFound("Order not available");
            }

            ViewData["Order"] = order;
            ViewData["User"] = user;

            return View();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(Transaction transaction, Order order)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _logger.LogError("[TransactionController] User not found while executing _userManager.GetUserAsync(HttpContext.User)");
                return NotFound("User not found");
            }

            var newTransaction = new Transaction();
            newTransaction.Amount = transaction.Amount;
            newTransaction.TransactionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            newTransaction.CustomerId = transaction.CustomerId;
            newTransaction.OrderId = transaction.OrderId;
            
            if (ModelState.IsValid)
            {
                await _orderRepository.CreateOrder(order);
                bool returnOk = await _transactionRepository.Create(newTransaction);
                if(returnOk) { }
                return RedirectToAction("OrderComplete", newTransaction);
                
            }
            
            _logger.LogWarning("[TransactionController] Transaction creation failed {@transaction}", transaction);
            await _orderRepository.Delete(order.OrderId);
            return RedirectToAction("CreateOrder","Order", new { Order = order });
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> OrderCompleteAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null)
            {
                _logger.LogError("[Transactioncontroller] transaction not found for the TransactionId {TransactionId:0000}", id);
                return NotFound("Transaction not found for the TransactionId");
            }
            return View(transaction);
        }
    }
}
