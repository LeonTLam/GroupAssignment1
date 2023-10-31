using Microsoft.AspNetCore.Mvc;

namespace GroupAssignment1.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
