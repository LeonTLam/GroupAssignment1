using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GroupAssignment1.DAL;

namespace GroupAssignment1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HousingDbContext _housingDbContext;

        public CustomerController(HousingDbContext housingDbContext)
        {
            _housingDbContext = housingDbContext;
        }

        public async Task<IActionResult> Table()
        {
            List<Customer> customers = await _housingDbContext.Customers.ToListAsync();
            return View(customers);
        }
    }
}
