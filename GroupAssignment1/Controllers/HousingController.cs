using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public List<Order> OrderConsole()
        {
            return _housingDbContext.Orders.ToList();
        }

        public async Task<IActionResult> Table()
        {
            List<Housing> housings = await _housingDbContext.Housings.ToListAsync();
            var housingListViewModel = new HousingListViewModel(housings, "Table");
            return View(housingListViewModel);
        }

        public async Task<IActionResult> Grid()
        {
            List<Housing> housings = await _housingDbContext.Housings.ToListAsync();
            var housingListViewModel = new HousingListViewModel(housings, "Grid");
            return View(housingListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var housing = await _housingDbContext.Housings.FirstOrDefaultAsync(i => i.HousingId == id);
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
        public async Task<IActionResult> Create(Housing housing)
        {
            if (ModelState.IsValid)
            {
                _housingDbContext.Housings.Add(housing);
                await _housingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            return View(housing);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var item = await _housingDbContext.Housings.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Housing housing)
        {
            if (ModelState.IsValid)
            {
                _housingDbContext.Housings.Update(housing);
                await _housingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            return View(housing);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var housing = await _housingDbContext.Housings.FindAsync(id);
            if (housing == null)
            {
                return NotFound();
            }
            return View(housing);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var housing = await _housingDbContext.Housings.FindAsync(id);
            if (housing == null) {
                return NotFound();
            }
            _housingDbContext.Housings.Remove(housing);
            await _housingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
    }
}
