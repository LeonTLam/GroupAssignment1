using Microsoft.AspNetCore.Mvc;

namespace GroupAssignment1.Controllers
{
    public class HousingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
