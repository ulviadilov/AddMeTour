using AddMeTour.Entity.Entities.Tour;
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
        private readonly ITourService _tourService;

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService, IMastheadService mastheadService, IRatingService ratingService, IHomeReviewService homeReviewService, ITourService tourService )
        {
            _logger = logger;
            _featureService = featureService;
            _mastheadService = mastheadService;
            _ratingService = ratingService;
            _homeReviewService = homeReviewService;
            _tourService = tourService;
        }

        public async Task<IActionResult> Index()
        {
            List<MastheadViewModel> mastheads = await _mastheadService.GetAllMastheadsNonDeletedAsync();
            List<RatingViewModel> ratings = await _ratingService.GetAllRatingsNonDeletedAsync();
            List<Country> countries = await _tourService.GetAllCountriesAsync();
            IndexVM indexVM = new IndexVM
            {
                Features =  await _featureService.GetAllFeaturesNonDeletedAsync(),
                Masthead = mastheads.FirstOrDefault(),
                Rating = ratings.FirstOrDefault(),
                homeReviews = await _homeReviewService.GetAllHomeReviewsNonDeletedAsync(),
                Tours = await _tourService.GetAllBestToursNonDeletedAsync(),
                Countries = countries
            };
            return View(indexVM);
        }

        public async Task<IActionResult> About()
        {
            AboutViewModel aboutVM = new AboutViewModel
            { 
                Features = await _featureService.GetAllFeaturesNonDeletedAsync()
            };

            return View(aboutVM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}