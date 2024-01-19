using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Entity.ViewModels.Tour.Destination;
using AddMeTour.Service.Helpers.Pagination;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ITourService _tourService;

        public DestinationController(IDestinationService destinationService, ITourService tourService)
        {
            _destinationService = destinationService;
            _tourService = tourService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var destinations = await _destinationService.GetAllDestinationsNonDeletedAsync();
            var query = destinations.AsQueryable();
            var paginated = PaginatedList<DestinationViewModel>.Create(query, 6, page);

            return View(paginated);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Tours = await _tourService.GetAllToursNonDeletedAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(DestinationAddViewModel destinationAddVM)
        {
            ViewBag.Tours = await _tourService.GetAllToursNonDeletedAsync();
            if (destinationAddVM == null && !ModelState.IsValid) return View(destinationAddVM);
            await _destinationService.CreateDestinationAsync(destinationAddVM);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Tours = await _tourService.GetAllToursNonDeletedAsync();
            if (id == Guid.Empty) return NotFound();
            return View(await _destinationService.UpdateDestinationByGuidAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(DestinationViewModel destinationVM)
        {
            ViewBag.Tours = await _tourService.GetAllToursNonDeletedAsync();
            if (!ModelState.IsValid) return View(destinationVM);
            await _destinationService.UpdateDestinationAsync(destinationVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoftDelete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await _destinationService.SoftDeleteDestinationAsync(id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> HardDelete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await _destinationService.HardDeleteDestinationAsync(id);
            return RedirectToAction("DeletedDestinations");
        }

        public async Task<IActionResult> DeletedDestinations(int page = 1)
        {
            var destinations = await _destinationService.GetAllPassiveDestinations();
            var query = destinations.AsQueryable();
            var paginated = PaginatedList<DestinationViewModel>.Create(query, 6, page);
            return View(paginated);
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await _destinationService.RecoverDestinationAsync(id);
            return RedirectToAction("Index");
        }




    }
}
