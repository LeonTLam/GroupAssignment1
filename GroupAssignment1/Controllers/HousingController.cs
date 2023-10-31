using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using GroupAssignment1.ViewModels;
using GroupAssignment1.DAL;
using Microsoft.AspNetCore.Authorization;

namespace GroupAssignment1.Controllers
{
    public class HousingController : Controller
    {
        private readonly IHousingRepository _housingRepository;
        private readonly ILogger<HousingController> _logger;


        public HousingController(IHousingRepository housingRepository, ILogger<HousingController> logger)
        {
            _logger = logger;
            _housingRepository = housingRepository;
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Table()
        {
            var housings = await _housingRepository.GetAll();
            if (housings == null)
            {
                _logger.LogError("[HousingController] Housing list not found while executing _HousingRepository.GetAll()");
                return NotFound("Housing list not found");
            }
            var housingListViewModel = new HousingListViewModel(housings, "Table");
            return View(housingListViewModel);
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Grid()
        {
            var housings = await _housingRepository.GetAll();
            if (housings == null)
            {
                _logger.LogError("[HousingController] Housing list not found while executing _HousingRepository.GetAll()");
                return NotFound("Housing list not found");
            }
            var housingListViewModel = new HousingListViewModel(housings, "Grid");
            return View(housingListViewModel);
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int id)
        {
            var housing = await _housingRepository.GetHousingById(id);
            if (housing == null)
            {
                _logger.LogError("[HousingController] Housing not found for the HousingId {HousingId:0000}", id);
                return NotFound("Housing not found for the HousingId");
            }
            return View(housing);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Housing housing)
        {

            if (housing.StartDate >= housing.EndDate)
            {
                _logger.LogWarning("[HousingController] Housing creation failed {@housing}", housing);
                return View(housing);
            }
            if (ModelState.IsValid)
            {
                
                bool returnOk = await _housingRepository.Create(housing);
                if (returnOk)
                {
                    return RedirectToAction(nameof(Table));
                }
            }
            _logger.LogWarning("[HousingController] Housing creation failed {@housing}", housing);
                return View(housing);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var housing = await _housingRepository.GetHousingById(id);
            if (housing == null)
            {
                _logger.LogError("[HousingController] Housing not found when updating the HousingId {HousingId:0000}", id);
                return BadRequest("Housing not found for the HousingId");
            }
            return View(housing);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(Housing housing)
        {
            if (housing.StartDate >= housing.EndDate)
            {
                _logger.LogWarning("[HousingController] Housing creation failed {@housing}", housing);
                return View(housing);
            }
            if (ModelState.IsValid)
            {
                bool returnOk = await _housingRepository.Update(housing);
                if (returnOk)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogWarning("[HousingController] Housing update failed {@housing}", housing);
            return View(housing);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var housing = await _housingRepository.GetHousingById(id);
            if (housing == null)
            {
                _logger.LogError("[HousingController] Housing not found for the HousingId {HousingId:0000}", id);
                return BadRequest("Housing not found for the HousingId");
            }
            return View(housing);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool returnOk = await _housingRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("[HousingController] Housing deletion failed for the HousingId {HousingId:0000}", id);
                return BadRequest("Housing deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}
