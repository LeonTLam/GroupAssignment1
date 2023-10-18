using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using GroupAssignment1.ViewModels;

namespace GroupAssignment1.Controllers
{
    public class HousingController : Controller
    {
        private readonly HousingDbContext _housingDbContext;

        public HousingController(HousingDbContext housingDbContext)
        {
            _housingDbContext = housingDbContext;
        }

        public IActionResult Table()
        {
            List<Housing> housings = _housingDbContext.Housing.ToList();
            var housingListViewModel = new HousingListViewModel(housings, "Table");
            return View(housingListViewModel);
        }

        public IActionResult Grid()
        {
            List<Housing> housings = _housingDbContext.Housing.ToList();
            var housingListViewModel = new HousingListViewModel(housings, "Grid");
            return View(housingListViewModel);
        }

        public IActionResult Details(int id)
        {
            List<Housing> housings = _housingDbContext.Housing.ToList();
            var housing = housings.FirstOrDefault(i => i.HousingId == id);
            if (housing == null)
                return NotFound();

            return View(housing);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Housing housing)
        {
            if (ModelState.IsValid)
            {
                _housingDbContext.Housing.Add(housing);
                _housingDbContext.SaveChanges();
                return RedirectToAction(nameof(Table));
            }
            return View(housing);
        }

        [HttpGet]
        public IActionResult Update(int id) 
        {
            var item = _housingDbContext.Housing.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Housing housing)
        {
            if (!ModelState.IsValid)
            {
                _housingDbContext.Housing.Update(housing);
                _housingDbContext.SaveChanges();
                return RedirectToAction(nameof(Table));
            }
            return View(housing);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var housing = _housingDbContext.Housing.Find(id);
            if (housing == null)
            {
                return NotFound();
            }
            return View(housing);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var housing = _housingDbContext.Housing.Find(id);
            if (housing == null) {
                return NotFound();
            }
            _housingDbContext.Housing.Remove(housing);
            _housingDbContext.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
    }
}
