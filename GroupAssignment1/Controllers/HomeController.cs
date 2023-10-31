using GroupAssignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using GroupAssignment1.ViewModels;
using GroupAssignment1.DAL;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Authorization;

namespace GroupAssignment1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHousingRepository _housingRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHousingRepository housingRepository)
        {
            _logger = logger;
            _housingRepository = housingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allHousings = await _housingRepository.GetAll();
            // You have the list of all housing objects in the allHousings variable

            return View(allHousings);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}