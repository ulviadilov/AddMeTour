using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class GuaranteedTimeController : Controller
    {
        private readonly IGuaranteedTimeService _GuaranteedTimeService;
        private readonly ITourService _tourService;

        public GuaranteedTimeController(IGuaranteedTimeService GuaranteedTimeService, ITourService tourService)
        {
            _GuaranteedTimeService = GuaranteedTimeService ?? throw new ArgumentNullException(nameof(GuaranteedTimeService));
            _tourService = tourService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var GuaranteedTimes = await _GuaranteedTimeService.GetAllDatesNonDeletedAsync();
            var query = GuaranteedTimes.AsQueryable();
            var paginatedGuaranteedTimes = PaginatedList<GuaranteedTimeViewModel>.Create(query, 6, page);
            return View(paginatedGuaranteedTimes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Tours = await _tourService.GetAllGuaranteedToursAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuaranteedTimeAddViewModel GuaranteedTimeAddVM)
        {
            ViewBag.Tours = await _tourService.GetAllGuaranteedToursAsync();
            if (ModelState.IsValid)
            {
                await _GuaranteedTimeService.CreateGuaranteedTimeAsync(GuaranteedTimeAddVM);
                return RedirectToAction("Index");
            }

            return View(GuaranteedTimeAddVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid GuaranteedTimeId)
        {
            ViewBag.Tours = await _tourService.GetAllGuaranteedToursAsync();
            if (GuaranteedTimeId == Guid.Empty)
            {
                return NotFound();
            }

            var GuaranteedTimeUpdateVM = await _GuaranteedTimeService.UpdateGuaranteedTimeByGuidAsync(GuaranteedTimeId);
            if (GuaranteedTimeUpdateVM == null)
            {
                return NotFound();
            }

            return View(GuaranteedTimeUpdateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GuaranteedTimeViewModel GuaranteedTimeUpdateVM)
        {
            ViewBag.Tours = await _tourService.GetAllGuaranteedToursAsync();
            if (ModelState.IsValid)
            {
                await _GuaranteedTimeService.UpdateGuaranteedTimeAsync(GuaranteedTimeUpdateVM);
                return RedirectToAction("Index");
            }

            return View(GuaranteedTimeUpdateVM);
        }

        public async Task<IActionResult> SoftDelete(Guid GuaranteedTimeId)
        {
            if (GuaranteedTimeId == Guid.Empty)
            {
                return NotFound();
            }

            await _GuaranteedTimeService.SoftDeleteGuaranteedTimeAsync(GuaranteedTimeId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HardDelete(Guid GuaranteedTimeId)
        {
            if (GuaranteedTimeId == Guid.Empty)
            {
                return NotFound();
            }

            await _GuaranteedTimeService.HardDeleteGuaranteedTimeAsync(GuaranteedTimeId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedGuaranteedTimes(int page = 1)
        {
            var deletedGuaranteedTimes = await _GuaranteedTimeService.GetAllPassiveGuaranteedTimes();
            var query = deletedGuaranteedTimes.AsQueryable();
            var paginatedGuaranteedTimes = PaginatedList<GuaranteedTimeViewModel>.Create(query, 6, page);
            return View(paginatedGuaranteedTimes);
        }

        public async Task<IActionResult> RecoverGuaranteedTime(Guid GuaranteedTimeId)
        {
            if (GuaranteedTimeId == Guid.Empty)
            {
                return NotFound();
            }

            await _GuaranteedTimeService.RecoverGuaranteedTimeAsync(GuaranteedTimeId);
            return RedirectToAction("Index");
        }
    }
}
