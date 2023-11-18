using AddMeTour.Entity.ViewModels.Home;
using AddMeTour.Service.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AddMeTour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureService _featureService;

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService)
        {
            _logger = logger;
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = new IndexVM
            {
                Features =  await _featureService.GetAllFeaturesNonDeletedAsync()
            };
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}