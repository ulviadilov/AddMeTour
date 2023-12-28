using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InclusionController : Controller
    {
        private readonly IInclusionService _inclusionService;

        public InclusionController(IInclusionService inclusionService)
        {
            _inclusionService = inclusionService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _inclusionService.GetAllInclusionsNonDeletedAsync());
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

        public async Task<IActionResult> DeletedInclusions()
        {
            return View(await _inclusionService.GetAllPassiveInclusions());
        }

        public async Task<IActionResult> RecoverInclusion(Guid Id)
        {
            if (Id == Guid.Empty) return NotFound();
            await _inclusionService.RecoverInclusionAsync(Id);
            return RedirectToAction("Index");
        }

    }
}
