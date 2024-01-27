using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Partners;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstraction;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class PartnerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPartnerService _PartnerService;

        public PartnerController(IMapper mapper, IPartnerService PartnerService)
        {
            _mapper = mapper;
            _PartnerService = PartnerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var Partners = await _PartnerService.GetAllPartnersNonDeletedAsync();
            var query = Partners.AsQueryable();
            var paginated = PaginatedList<PartnerViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartnerAddViewModel PartnerAddVM)
        {
            var map = _mapper.Map<Partner>(PartnerAddVM);
            if (!ModelState.IsValid)
            {
                return View(PartnerAddVM);
            }
            if (PartnerAddVM.ImageFile != null)
            {
                string result = PartnerAddVM.ImageFile.CheckValidate("/image",3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            await _PartnerService.CreatePartnerAsync(PartnerAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(Guid PartnerId)
        {
            if (PartnerId == Guid.Empty) return NotFound();
            var Partner = _PartnerService.UpdatePartnerById(PartnerId);
            return View(Partner);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PartnerUpdateViewModel PartnerUpVM)
        {
            if (PartnerUpVM.ImageFile != null)
            {
                string result = PartnerUpVM.ImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            if (!ModelState.IsValid) return View(PartnerUpVM);

            await _PartnerService.UpdatePartnerAsync(PartnerUpVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SafeDelete(Guid PartnerId)
        {
            if (PartnerId == Guid.Empty) return NotFound();
            await _PartnerService.SafeDeletePartnerAsync(PartnerId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedPartners(int page = 1)
        {
            var deletedPartners = await _PartnerService.GetAllPassivePartners();
            var query = deletedPartners.AsQueryable();
            var paginated = PaginatedList<PartnerViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        public async Task<IActionResult> HardDelete(Guid PartnerId)
        {
            if (PartnerId == Guid.Empty) return NotFound();
            await _PartnerService.HardDeleteAsync(PartnerId);
            return RedirectToAction("DeletedPartners");
        }

        public async Task<IActionResult> Recover(Guid PartnerId)
        {
            if (PartnerId == Guid.Empty) return NotFound();
            await _PartnerService.RecoverPartnerAsync(PartnerId);
            return RedirectToAction("DeletedPartners");
        }
    }
}
