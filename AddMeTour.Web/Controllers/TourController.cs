using AddMeTour.Entity.ViewModels.Home;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Web.ViewModels;
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
        public async Task<IActionResult> Index(int page = 1)
        {
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            var languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            List<TourViewModel> tours = await _tourService.GetAllToursNonDeletedAsync();
            var paginated = PaginatedList<TourViewModel>.Create(tours.AsQueryable(), 6, page);

            MainTourViewModel tourVM = new MainTourViewModel
            {
                Tours = paginated,
                Categories = categories,
                Countries = countries,
                Languages = languages
            };
            return View(tourVM);
        }

        public async Task<IActionResult> Category(Guid id, int page = 1)
        {
            var category = await _categoryService.GetCategoryByGuidAsync(id);
            if (category == null) return NotFound();
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            var query = tours.AsQueryable();
            var paginated = PaginatedList<TourViewModel>.Create(query, 6, page);
            SearchByCategoryViewModel tourVM = new SearchByCategoryViewModel
            {
                Category = category,
                Tours = paginated,
                Countries = countries
            };
            return View(tourVM);
        }


        public async Task<IActionResult> Country(Guid id, int page = 1)
        {
            var country = await _countryService.GetCountryByGuidAsync(id);
            if (country == null) return NotFound();
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            var countries = await _countryService.GetAllCountriesNonDeletedAsync();
            var query = tours.AsQueryable();
            var paginated = PaginatedList<TourViewModel>.Create(query, 6, page);
            SearchByCountryViewModel tourVM = new SearchByCountryViewModel
            {
                Country = country,
                Tours = paginated,
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
