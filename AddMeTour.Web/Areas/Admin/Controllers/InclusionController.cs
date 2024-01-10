using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
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
    public class InclusionController : Controller
    {
        private readonly IInclusionService _inclusionService;

        public InclusionController(IInclusionService inclusionService)
        {
            _inclusionService = inclusionService;
        }

        public async Task<IActionResult> Index(int page =1 )
        {
            var inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            var query = inclusions.AsQueryable();
            var paginated = PaginatedList<InclusionViewModel>.Create(query,6,page);
            return View(paginated);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InclusionAddViewModel inclusionAddVM)
        {
            if (!ModelState.IsValid && inclusionAddVM is null) return NotFound();
            await _inclusionService.CreateInclusionAsync(inclusionAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            return View(await _inclusionService.UpdateInclusionByGuidAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(InclusionUpdateViewModel inclusionUpdateVM)
        {
            if(inclusionUpdateVM is null && !ModelState.IsValid) return NotFound();
            await _inclusionService.UpdateInclusionAsync(inclusionUpdateVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoftDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _inclusionService.SoftDeleteInclusionAsync(Id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _inclusionService.HardDeleteInclusionAsync(Id);
            return RedirectToAction("DeletedInclusions");
        }

        public async Task<IActionResult> DeletedInclusions(int page = 1)
        {
            var inc = await _inclusionService.GetAllPassiveInclusions();
            var query = inc.AsQueryable();
            var paginated = PaginatedList<InclusionViewModel>.Create(query,6,page);
            return View(paginated);
        }

        public async Task<IActionResult> RecoverInclusion(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _inclusionService.RecoverInclusionAsync(Id);
            return RedirectToAction("Index");
        }

    }
}
