using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFeatureService _featureService;

        public FeatureController(IMapper mapper,  IFeatureService featureService)
        {
            _mapper = mapper;
            _featureService = featureService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var features = await _featureService.GetAllFeaturesNonDeletedAsync();
            return View(features);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeatureAddViewModel featureAddVM)
        {
            var map = _mapper.Map<Feature>(featureAddVM);
            if (featureAddVM.ImageFile != null)
            {
                string result = featureAddVM.ImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            if (!ModelState.IsValid)
            {
                return View(featureAddVM);
            }
            await _featureService.CreateFeatureAsync(featureAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public  IActionResult Update(Guid featureId)
        {
            if (featureId == Guid.Empty) return NotFound();
            var feature =  _featureService.UpdateFeatureById(featureId);
            return View(feature);
        }


        [HttpPost]
        public async Task<IActionResult> Update (FeatureUpdateViewModel featureUpVM)
        {
            if (featureUpVM.ImageFile != null)
            {
                string result = featureUpVM.ImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("ImageFile", result);
                }
            }
            if (!ModelState.IsValid) return View(featureUpVM);

            await _featureService.UpdateFeatureAsync(featureUpVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SafeDelete(Guid featureId)
        {
            if(featureId == Guid.Empty) return NotFound(); 
            await _featureService.SafeDeleteFeatureAsync(featureId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedFeatures()
        {
            var deletedFeatures = await _featureService.GetAllPassiveFeatures();
            return View(deletedFeatures);
        }

        public async Task<IActionResult> HardDelete(Guid featureId)
        {
            if (featureId == Guid.Empty) return NotFound();
            await _featureService.HardDeleteAsync(featureId);
            return RedirectToAction("DeletedFeatures");
        }

        public async Task<IActionResult> Recover(Guid featureId)
        {
            if (featureId == Guid.Empty) return NotFound();
            await _featureService.RecoverFeatureAsync(featureId);
            return RedirectToAction("DeletedFeatures");
        }

    }
}
