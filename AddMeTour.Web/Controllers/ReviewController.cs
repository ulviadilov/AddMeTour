using AddMeTour.Controllers;
using AddMeTour.Entity.ViewModels.Review;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Controllers;

public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsyncReview(AddReviewVM reviewVM)
    {
        await _reviewService.CreateReviewAsync(reviewVM);
        return RedirectToAction("Review","home");

    }


}
