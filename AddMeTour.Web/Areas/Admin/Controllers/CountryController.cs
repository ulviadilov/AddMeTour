using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(ICountryService countryService, IUnitOfWork unitOfWork)
        {
            _countryService = countryService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _countryService.GetAllCountriesNonDeletedAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryAddViewModel countryAddVM)
        {
            if (countryAddVM == null && !ModelState.IsValid) return View(countryAddVM);
            await _countryService.CreateCountryAsync(countryAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid countryId)
        {
            if (countryId == Guid.Empty) return NotFound();
            return View(await _countryService.UpdateCountryByGuidAsync(countryId));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CountryUpdateViewModel countryUpdateVM)
        {
            if (!ModelState.IsValid) return NotFound();
            await _countryService.UpdateCountryAsync(countryUpdateVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoftDelete(Guid countryId)
        {
            if (countryId == Guid.Empty) return NotFound();
            await _countryService.SoftDeleteCountryAsync(countryId);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid countryId)
        {
            if (countryId == Guid.Empty) return NotFound();
            await _countryService.HardDeleteCountryAsync(countryId);
            return RedirectToAction("DeletedCountries");
        }

        public async Task<IActionResult> DeletedCountries()
        {
            return View(await _countryService.GetAllPassiveCountries());
        }

        public async Task<IActionResult> RecoverCountry(Guid countryId)
        {
            if (countryId == Guid.Empty) return NotFound();
            await _countryService.RecoverCountryAsync(countryId);
            return RedirectToAction("Index");
        }
    }
}
