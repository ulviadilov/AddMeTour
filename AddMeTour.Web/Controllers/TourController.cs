using AddMeTour.Entity.ViewModels.MainTour;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly ILanguageService _languageService;

        public TourController(ITourService tourService, ICategoryService categoryService, ICountryService countryService, ILanguageService languageService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _countryService = countryService;
            _languageService = languageService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            var languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            List<TourViewModel> tours = await _tourService.GetAllToursNonDeletedAsync();
            MainTourViewModel tourVM = new MainTourViewModel
            {
                Tours = tours,
                Categories = categories,
                Countries = countries,
                Languages = languages
            };
            return View(tourVM);
        }
    }
}
