using AddMeTour.Entity.ViewModels.Home;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels.Rating;
using AddMeTour.Service.Services.Abstraction;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AddMeTour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureService _featureService;
        private readonly IMastheadService _mastheadService;
        private readonly IRatingService _ratingService;
        private readonly IHomeReviewService _homeReviewService;

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService, IMastheadService mastheadService, IRatingService ratingService, IHomeReviewService homeReviewService )
        {
            _logger = logger;
            _featureService = featureService;
            _mastheadService = mastheadService;
            _ratingService = ratingService;
            _homeReviewService = homeReviewService;
        }

        public async Task<IActionResult> Index()
        {
            List<MastheadViewModel> mastheads = await _mastheadService.GetAllMastheadsNonDeletedAsync();
            List<RatingViewModel> ratings = await _ratingService.GetAllRatingsNonDeletedAsync();
            IndexVM indexVM = new IndexVM
            {
                Features =  await _featureService.GetAllFeaturesNonDeletedAsync(),
                Masthead = mastheads.FirstOrDefault(),
                Rating = ratings.FirstOrDefault(),
                homeReviews = await _homeReviewService.GetAllHomeReviewsNonDeletedAsync()
            };
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}