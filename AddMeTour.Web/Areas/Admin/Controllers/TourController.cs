using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        public TourController(IMapper mapper, ITourService tourService, ICategoryService categoryService, ICountryService countryService,
            IInclusionService inclusionService, IExclusionService exclusionService, ILanguageService languageService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tourService = tourService ?? throw new ArgumentNullException(nameof(tourService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _inclusionService = inclusionService ?? throw new ArgumentNullException(nameof(inclusionService));
            _exclusionService = exclusionService ?? throw new ArgumentNullException(nameof(exclusionService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
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
                    return View(tourAddVM);
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
                        return View(tourAddVM);
                    }
                }
            }

            await _tourService.CreateTourAsync(tourAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            ViewBag.Countries = await _countryService.GetAllCountriesNonDeletedAsync();
            ViewBag.Inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            ViewBag.Exclusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            ViewBag.Languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            var tour = await _tourService.UpdateTourByGuidAsync(id);
            if (tour is null)
            {
                return NotFound();
            }

            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TourUpdateViewModel tourUpdateVM)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            ViewBag.Countries = await _countryService.GetAllCountriesNonDeletedAsync();
            ViewBag.Inclusions = await _inclusionService.GetAllInclusionsNonDeletedAsync();
            ViewBag.Exclusions = await _exclusionService.GetAllExclusionsNonDeletedAsync();
            ViewBag.Languages = await _languageService.GetAllLanguagesNonDeletedAsync();
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            if (tourUpdateVM.PosterImageFile is not null)
            {
                string result = tourUpdateVM.PosterImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("PosterImageFile", result);
                    return View(tourUpdateVM);
                }
            }

            if (tourUpdateVM.ImageFiles is not null)
            {
                foreach (IFormFile image in tourUpdateVM.ImageFiles)
                {
                    string result = image.CheckValidate("image/", 3000);
                    if (result.Length > 0)
                    {
                        ModelState.AddModelError("ImageFiles", result);
                        return View(tourUpdateVM);
                    }
                }
            }

            await _tourService.UpdateTourAsync(tourUpdateVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SafeDelete(Guid id)
        {
            var tour = await _tourService.GetTourByGuidAsync(id);
            if (tour is null)
            {
                return NotFound();
            }

            await _tourService.SafeDeleteTourAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HardDelete(Guid id)
        {
            var tour = await _tourService.GetTourByGuidAsync(id);
            if (tour is null)
            {
                return NotFound();
            }

            await _tourService.HardDeleteAsync(id);
            return RedirectToAction("DeletedTours");
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            var tour = await _tourService.GetTourByGuidAsync(id);
            if (tour is null)
            {
                return NotFound();
            }

            await _tourService.RecoverTourAsync(id);
            return RedirectToAction("DeletedTours");
        }

        public async Task<IActionResult> DeletedTours()
        {
            var tours = await _tourService.GetAllPassiveTours();
            return View(tours);
        }
    }
}
