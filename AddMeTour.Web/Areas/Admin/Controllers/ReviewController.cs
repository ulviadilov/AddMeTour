using AddMeTour.Entity.ViewModels.Review;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page =1)
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            var paginated = PaginatedList<ReviewVM>.Create(reviews.AsQueryable().OrderByDescending(x=>x.Created),9, page);
            reviews = paginated;
            return View(paginated);
        }
        public async Task<IActionResult> ReviewActivater(Guid id)
        {
            var review = await _reviewService.GetReviewByGuidAsync(id);
            review.isActive = true;
            await _reviewService.UpdateReviewAsync(review);
            return RedirectToAction("index", "review");
        }
        public async Task<IActionResult> ReviewDeactivater(Guid id)
        {
            var review = await _reviewService.GetReviewByGuidAsync(id);
            review.isActive = false;
            await _reviewService.UpdateReviewAsync(review);
            return RedirectToAction("index", "review");
        }
        public async Task<IActionResult> ReviewRemover(Guid id)
        {
            await _reviewService.HardDeleteReviewAsync(id);
            return RedirectToAction("index", "review");
        }
    }
}
