using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
