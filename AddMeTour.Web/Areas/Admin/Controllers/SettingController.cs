using AddMeTour.Entity.ViewModels.Setting;
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
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int page = 1 )
        {
            var setting = await _settingService.GetAllSettingsAsync();
            var query = setting.AsQueryable();
            var paginated = PaginatedList<SettingViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var settingVM = await _settingService.UpdateSettingByGuidAsync(id);
            if (settingVM == null) return NotFound();
            return View(settingVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SettingUpdateViewModel settingUpdateVM)
        {
            if (ModelState.IsValid)
            {
                await _settingService.UpdateSettingAsync(settingUpdateVM);
                return RedirectToAction("Index");
            }
            return View(settingUpdateVM);
        }
    }
}
