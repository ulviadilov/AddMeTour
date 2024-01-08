using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _languageService.GetAllLanguagesNonDeletedAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageAddViewModel languageAddVM)
        {
            if (!ModelState.IsValid && languageAddVM is null) return NotFound();
            await _languageService.CreateLanguageAsync(languageAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            return View(await _languageService.UpdateLanguageByGuidAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(LanguageViewModel languageVM)
        {
            if (languageVM is null || !ModelState.IsValid) return NotFound();
            await _languageService.UpdateLanguageAsync(languageVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoftDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _languageService.SoftDeleteLanguageAsync(Id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _languageService.HardDeleteLanguageAsync(Id);
            return RedirectToAction("DeletedLanguages");
        }

        public async Task<IActionResult> DeletedLanguages()
        {
            return View(await _languageService.GetAllPassiveLanguages());
        }

        public async Task<IActionResult> RecoverLanguage(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _languageService.RecoverLanguageAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
