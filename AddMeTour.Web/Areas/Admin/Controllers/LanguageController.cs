using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var lang = await _languageService.GetAllLanguagesNonDeletedAsync();
            var query = lang.AsQueryable();
            var paginated = PaginatedList<LanguageViewModel>.Create(query, 6, page);
            return View(paginated);
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
            if (await _languageService.GetLanguageByGuidAsync(Id) is null) return NotFound();
            await _languageService.SoftDeleteLanguageAsync(Id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _languageService.HardDeleteLanguageAsync(Id);
            return RedirectToAction("DeletedLanguages");
        }

        public async Task<IActionResult> DeletedLanguages(int page = 1)
        {
            var lang = await _languageService.GetAllPassiveLanguages();
            var query = lang.AsQueryable();
            var paginated = PaginatedList<LanguageViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        public async Task<IActionResult> RecoverLanguage(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _languageService.RecoverLanguageAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
