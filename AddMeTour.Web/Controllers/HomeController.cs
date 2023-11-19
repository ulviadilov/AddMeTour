using AddMeTour.Entity.ViewModels.Home;
using AddMeTour.Entity.ViewModels.Masthead;
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

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService, IMastheadService mastheadService)
        {
            _logger = logger;
            _featureService = featureService;
            _mastheadService = mastheadService;
        }

        public async Task<IActionResult> Index()
        {
            List<MastheadViewModel> mastheads = await _mastheadService.GetAllMastheadsNonDeletedAsync();
            IndexVM indexVM = new IndexVM
            {
                Features =  await _featureService.GetAllFeaturesNonDeletedAsync(),
                Masthead = mastheads.FirstOrDefault()
            };
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}