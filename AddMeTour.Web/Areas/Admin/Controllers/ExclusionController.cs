using AddMeTour.Entity.ViewModels.Tour.Exclusion;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class ExclusionController : Controller
    {
        private readonly IExclusionService _exclusionService;

        public ExclusionController(IExclusionService exclusionService)
        {
            _exclusionService = exclusionService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var exclusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            var query = exclusions.AsQueryable();
            var paginated = PaginatedList<ExclusionViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExclusionAddViewModel exclusionAddVM)
        {
            if (!ModelState.IsValid && exclusionAddVM is null) return NotFound();
            await _exclusionService.CreateExclusionAsync(exclusionAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            return View(await _exclusionService.UpdateExclusionByGuidAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExclusionViewModel exclusionVM)
        {
            if (exclusionVM is null && !ModelState.IsValid) return NotFound();
            await _exclusionService.UpdateExclusionAsync(exclusionVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoftDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _exclusionService.SoftDeleteExclusionAsync(Id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _exclusionService.HardDeleteExclusionAsync(Id);
            return RedirectToAction("DeletedExclusions");
        }

        public async Task<IActionResult> DeletedExclusions(int page = 1)
        {
            var exclusions = await _exclusionService.GetAllPassiveExclusions();
            var query = exclusions.AsQueryable();
            var paginated = PaginatedList<ExclusionViewModel>.Create(query,6,page);
            return View(paginated);
        }

        public async Task<IActionResult> RecoverExclusion(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _exclusionService.RecoverExclusionAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
