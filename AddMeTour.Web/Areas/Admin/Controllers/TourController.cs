using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TourController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly IInclusionService _inclusionService;
        private readonly IExclusionService _exclusionService;
        private readonly ILanguageService _languageService;

        public TourController(IMapper mapper, ITourService tourService, ICategoryService  categoryService, ICountryService countryService, IInclusionService inclusionService, IExclusionService exclusionService ,
            ILanguageService languageService)
        {
            _mapper = mapper;
            _tourService = tourService;
            _categoryService = categoryService;
            _countryService = countryService;
            _inclusionService = inclusionService;
            _exclusionService = exclusionService;
            _languageService = languageService;
        }
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllToursNonDeletedAsync();
            return View(tours);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            ViewBag.Countries = await _countryService.GetAllCountriesNonDeletedAsync();
            ViewBag.Inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            ViewBag.Exclusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            ViewBag.Languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TourAddViewModel tourAddVM)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            ViewBag.Countries = await _countryService.GetAllCountriesNonDeletedAsync();
            ViewBag.Inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            ViewBag.Exclusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            ViewBag.Languages = await _languageService.GetAllLanguagesNonDeletedAsync();

            var map = _mapper.Map<Tour>(tourAddVM);
            if (!ModelState.IsValid)
            {
                return View(tourAddVM);
            }
            if (tourAddVM.PosterImageFile is not null)
            {
                string result = tourAddVM.PosterImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("PosterImageFile", result);
                }
            }
            if (tourAddVM.ImageFiles is not null)
            {
                foreach (IFormFile image in tourAddVM.ImageFiles)
                {
                    string result = image.CheckValidate("image/", 3000);
                    if (result.Length > 0)
                    {
                        ModelState.AddModelError("ImageFiles", result);
                    }
                }
            }
            await _tourService.CreateTourAsync(tourAddVM);
            return RedirectToAction("Index");
        }
    }
}
