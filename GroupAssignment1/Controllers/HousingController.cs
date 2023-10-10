using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using GroupAssignment1.ViewModels;

namespace GroupAssignment1.Controllers
{
    public class HousingController : Controller
    {
        public IActionResult Table()
        {
            var housings = GetHousings();
            var housingListViewModel = new HousingListViewModel(housings, "Table");
            return View(housingListViewModel);
        }

        public IActionResult Grid()
        {
            var housings = GetHousings();
            var housingListViewModel = new HousingListViewModel(housings, "Grid");
            return View(housingListViewModel);
        }

        public IActionResult Details(int id)
        {
            var housings = GetHousings();
            var housing = housings.FirstOrDefault(i => i.HousingId == id);
            if (housing == null)
                return NotFound();
            
            return View(housing);
        }

        public List<Housing> GetHousings()
        {
            var housings = new List<Housing>();
            var housing1 = new Housing
            {
                HousingId = 1,
                Name = "Første",
                Rent = 250.00,
                Description = "Noe noe noe, huset noe noe noe, leie noe noe noe, lokasjon noe noe.",
                ImageUrl = "/images/housing1.jpg"

            };
            var housing2 = new Housing
            {
                HousingId = 2,
                Name = "Andre",
                Rent = 300.00,
                Description = "Noe noe noe, huset noe noe noe, leie noe noe noe, lokasjon noe noe.",
                ImageUrl = "/images/housing1.jpg"

            };
            var housing3 = new Housing
            {
                HousingId = 3,
                Name = "Tredje",
                Rent = 450.00,
                Description = "Noe noe noe, huset noe noe noe, leie noe noe noe, lokasjon noe noe.",
                ImageUrl = "/images/housing1.jpg"

            };

            housings.Add(housing1);
            housings.Add(housing2);
            housings.Add(housing3);

            return housings;
        }
    }
}
