using GroupAssignment1.Areas.Identity.Data;
using GroupAssignment1.DAL;
using GroupAssignment1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupAssignment1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUserController> _logger;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, ILogger<ApplicationUserController> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Table()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                _logger.LogError("[ApplicationUserController] User list not found while executing _userRepository.GetAll()");
                return NotFound("User list not found");
            }
            var userListViewModel = new UserListViewModel(users, "Table");
            return View(userListViewModel);
        }

    }
}
