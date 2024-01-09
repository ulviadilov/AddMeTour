using AddMeTour.Entity.ViewModels.Home;
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
        private readonly IInclusionService _inclusionService;
        private readonly IExclusionService _exclusionService;

        public TourController(ITourService tourService, ICategoryService categoryService, ICountryService countryService, ILanguageService languageService, IInclusionService inclusionService, IExclusionService exclusionService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _countryService = countryService;
            _languageService = languageService;
            _inclusionService = inclusionService;
            _exclusionService = exclusionService;
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

        public async Task<IActionResult> Category(Guid id)
        {
            var category = await _categoryService.GetCategoryByGuidAsync(id);
            if (category == null) return NotFound();
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            SearchByCategoryViewModel tourVM = new SearchByCategoryViewModel
            {
                Category = category,
                Tours = tours,
                Countries = countries
            };
            return View(tourVM);
        }


        public async Task<IActionResult> Country(Guid id)
        {
            var country = await _countryService.GetCountryByGuidAsync(id);
            if (country == null) return NotFound();
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            SearchByCountryViewModel tourVM = new SearchByCountryViewModel
            {
                Country = country,
                Tours = tours,
                Categories = categories,
                Countries = countries
            };
            return View(tourVM);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            var tour = tours.FirstOrDefault(x => x.Id == id);
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            var languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            var bestTours = await _tourService.GetAllBestToursNonDeletedAsync();
            var inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            var exlusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            if (tour == null) return NotFound();
            TourDetailViewModel tourDetailVM = new TourDetailViewModel
            {
                Tour = tour,
                Categories = categories,
                Languages = languages,
                Countries = countries,
                Tours = bestTours,
                Inclusions = inclusions,
                Exclusions = exlusions
            };
            return View(tourDetailVM);
        }
    }
}
