using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models; 

namespace GroupAssignment1.Controllers
{
    public class HousingController : Controller
    {
        public IActionResult Table()
        {
            var housings = new List<Housing>();
            var housing1 = new Housing();

            housing1.HousingId = 1;
            housing1.Name = "Hus1";
            housing1.Rent = 500;

            housings.Add(housing1);

            ViewBag.CurrentViewName = "List of housings";
            return View(housings);
        }
    }
}
