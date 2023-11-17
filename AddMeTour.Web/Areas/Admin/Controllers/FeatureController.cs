using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeatureService _featureService;

        public FeatureController(IMapper mapper, IUnitOfWork unitOfWork,IFeatureService featureService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _featureService = featureService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var features = await _featureService.GetAllFeaturesNonDeletedAsync();
            return View(features);
        }

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(FeatureAddViewModel featureAddVM)
        //{
        //    var map = _mapper.Map<Feature>(featureAddVM);
        //    var result = await _validator.ValidateAsync(map);
        //    if (result.IsValid)
        //    {
        //        await _featureService.CreateFeatureAsync(featureAddVM);
        //        RedirectToAction("Index","Feature");
        //    }
        //    else
        //    {
                
        //    }
        //}
    }
}
