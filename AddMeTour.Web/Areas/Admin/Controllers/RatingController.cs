using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels.Rating;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concretes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RatingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;
        private readonly IUnitOfWork _unitOfWork;
        public RatingController(IMapper mapper, IUnitOfWork unitOfWork, IRatingService ratingService)
        {
            _mapper = mapper;
            _ratingService = ratingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<RatingViewModel> ratingList = await _ratingService.GetAllRatingsNonDeletedAsync();
            return View(ratingList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingAddViewModel ratingAddVM)
        {
            var map = _mapper.Map<Rating>(ratingAddVM);
            if (!ModelState.IsValid) return View();
            await _ratingService.CreateRatingAsync(ratingAddVM);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(Guid ratingId)
        {
            if (ratingId == Guid.Empty) return NotFound();
            var rating = _ratingService.UpdateRatingById(ratingId);
            return View(rating);
        }


        [HttpPost]
        public async Task<IActionResult> Update(RatingUpdateViewModel ratingUpdateVM)
        {
            if (!ModelState.IsValid) return View(ratingUpdateVM);
            await _ratingService.UpdateRatingAsync(ratingUpdateVM);
            return RedirectToAction("Index");
        }




    }
}
