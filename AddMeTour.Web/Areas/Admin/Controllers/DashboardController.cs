using AddMeTour.Service.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
