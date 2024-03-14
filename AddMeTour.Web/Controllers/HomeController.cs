using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Gallery;
using AddMeTour.Entity.ViewModels.Home;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels.Partners;
using AddMeTour.Entity.ViewModels.Rating;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Helpers.Pagination;
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
        private readonly ICategoryService _categoryService;
        private readonly IPartnerService _partnerService;
        private readonly IGalleryService _galleryService;

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService, IMastheadService mastheadService, IRatingService ratingService, IHomeReviewService homeReviewService, ITourService tourService, ICategoryService categoryService, IPartnerService partnerService, IGalleryService galleryService)
        {
            _logger = logger;
            _featureService = featureService;
            _mastheadService = mastheadService;
            _ratingService = ratingService;
            _homeReviewService = homeReviewService;
            _tourService = tourService;
            _categoryService = categoryService;
            _partnerService = partnerService;
            _galleryService = galleryService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            List<MastheadViewModel> mastheads = await _mastheadService.GetAllMastheadsNonDeletedAsync();
            List<RatingViewModel> ratings = await _ratingService.GetAllRatingsNonDeletedAsync();
            List<Country> countries = await _tourService.GetAllCountriesAsync();
            List<CategoryViewModel> categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            List<PartnerViewModel> partners = await _partnerService.GetAllPartnersNonDeletedAsync();
            List<GalleryViewModel> galleryImages = await _galleryService.GetAllGallerysNonDeletedAsync();
            var tours = await _tourService.GetAllBestToursNonDeletedAsync();
            var query = tours.AsQueryable();
            var paginated = PaginatedList<TourViewModel>.Create(query,6,page);
            IndexVM indexVM = new IndexVM
            {
                Features =  await _featureService.GetAllFeaturesNonDeletedAsync(),
                Masthead = mastheads.FirstOrDefault(),
                Rating = ratings.FirstOrDefault(),
                homeReviews = await _homeReviewService.GetAllHomeReviewsNonDeletedAsync(),
                Tours = paginated,
                Countries = countries,
                Categories = categories,
                Partners = partners,
                GalleryImages = galleryImages
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
        
        public async Task<IActionResult> Contact()
        {
            return View();
        }


        public async Task<IActionResult> Gallery()
        {
            var galleryImages = await _galleryService.GetAllGallerysNonDeletedAsync();
            return View(galleryImages);
        }

        public async Task<IActionResult> Review()
        {
            return View();
        }
    }
}