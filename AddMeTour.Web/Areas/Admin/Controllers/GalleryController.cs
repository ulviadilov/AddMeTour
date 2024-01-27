using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Gallery;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin,Developer")]
    public class GalleryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGalleryService _GalleryService;

        public GalleryController(IMapper mapper, IGalleryService GalleryService)
        {
            _mapper = mapper;
            _GalleryService = GalleryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var Gallerys = await _GalleryService.GetAllGallerysNonDeletedAsync();
            var query = Gallerys.AsQueryable();
            var paginated = PaginatedList<GalleryViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GalleryAddViewModel GalleryAddVM)
        {
            var map = _mapper.Map<GalleryImage>(GalleryAddVM);
            if (!ModelState.IsValid)
            {
                return View(GalleryAddVM);
            }
            if (GalleryAddVM.ImageFile != null)
            {
                string result = GalleryAddVM.ImageFile.CheckValidate("/image",3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            await _GalleryService.CreateGalleryAsync(GalleryAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(Guid GalleryId)
        {
            if (GalleryId == Guid.Empty) return NotFound();
            var Gallery = _GalleryService.UpdateGalleryById(GalleryId);
            return View(Gallery);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GalleryUpdateViewModel GalleryUpVM)
        {
            if (GalleryUpVM.ImageFile != null)
            {
                string result = GalleryUpVM.ImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            if (!ModelState.IsValid) return View(GalleryUpVM);

            await _GalleryService.UpdateGalleryAsync(GalleryUpVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SafeDelete(Guid GalleryId)
        {
            if (GalleryId == Guid.Empty) return NotFound();
            await _GalleryService.SafeDeleteGalleryAsync(GalleryId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedGallerys(int page = 1)
        {
            var deletedGallerys = await _GalleryService.GetAllPassiveGallerys();
            var query = deletedGallerys.AsQueryable();
            var paginated = PaginatedList<GalleryViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        public async Task<IActionResult> HardDelete(Guid GalleryId)
        {
            if (GalleryId == Guid.Empty) return NotFound();
            await _GalleryService.HardDeleteAsync(GalleryId);
            return RedirectToAction("DeletedGallerys");
        }

        public async Task<IActionResult> Recover(Guid GalleryId)
        {
            if (GalleryId == Guid.Empty) return NotFound();
            await _GalleryService.RecoverGalleryAsync(GalleryId);
            return RedirectToAction("DeletedGallerys");
        }
    }
}
