using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound(int code)
        {
            ViewData["This page Not Found"] = code;
            return View();
        }
    }
}
