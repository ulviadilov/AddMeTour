using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.HomeReviews;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class HomeReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHomeReviewService _homeReviewService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeReviewController(IMapper mapper, IHomeReviewService homeReviewService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _homeReviewService = homeReviewService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<HomeReviewViewModel> reviews = await _homeReviewService.GetAllHomeReviewsNonDeletedAsync();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(HomeReviewAddViewModel homeReviewAddVM)
        {
            var mapper = _mapper.Map<HomeReview>(homeReviewAddVM);
            if (homeReviewAddVM.AvatarImageFile is not null)
            {
                string result = homeReviewAddVM.AvatarImageFile.CheckValidate("image/" , 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("AvatarImageFile", result);
                }
            }
            if (!ModelState.IsValid)
            {
                return View(homeReviewAddVM);
            }

            await _homeReviewService.CreateHomeReviewAsync(homeReviewAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(Guid homeReviewId)
        {
            if (homeReviewId == Guid.Empty) return NotFound();
            var homeReview = _homeReviewService.UpdateHomeReviewById(homeReviewId);
            return View(homeReview);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HomeReviewUpdateViewModel homeReviewUpdateVM)
        {
            if (homeReviewUpdateVM.AvatarImageFile is not null)
            {
                string result = homeReviewUpdateVM.AvatarImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("AvatarImageFile", result);
                }
            }
            if (!ModelState.IsValid) return View(homeReviewUpdateVM);
            await _homeReviewService.UpdateFeatureAsync(homeReviewUpdateVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SafeDelete(Guid homeReviewId)
        {
            if (homeReviewId == Guid.Empty) return NotFound();
            await _homeReviewService.SafeDeleteHomeReviewAsync(homeReviewId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedReviews()
        {
            var deletedReviews = await _homeReviewService.GetAllPassiveHomeReviews();
            return View(deletedReviews);
        }

        public async Task<IActionResult> HardDelete(Guid homeReviewId)
        {
            if (homeReviewId == Guid.Empty) return NotFound();
            await _homeReviewService.HardDeleteAsync(homeReviewId);
            return RedirectToAction("DeletedReviews");
        }

        public async Task<IActionResult> Recover(Guid homeReviewId)
        {
            if (homeReviewId == Guid.Empty) return NotFound();
            await _homeReviewService.RecoverHomeReviewAsync(homeReviewId);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("DeletedReviews");
        }

    }
}
