using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MastheadController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMastheadService _mastheadService;

        public MastheadController(IMapper mapper,IUnitOfWork unitOfWork, IMastheadService mastheadService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mastheadService = mastheadService;
        }
        public async Task<IActionResult> Index()
        {
            var mastheads = await _mastheadService.GetAllMastheadsNonDeletedAsync();
            return View(mastheads);
        }

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(MastheadAddViewModel mastheadAddVM)
        //{
        //    var map = _mapper.Map<Masthead>(mastheadAddVM);
        //    if (mastheadAddVM.BigImageFile != null)
        //    {
        //        string result = mastheadAddVM.BigImageFile.CheckValidate("image/", 3000);
        //        if (result.Length > 0)
        //        {
        //            ModelState.AddModelError("BigImageFile", result);
        //        }
        //    }
        //    if (mastheadAddVM.SmallImageFile1 != null)
        //    {
        //        string result = mastheadAddVM.SmallImageFile1.CheckValidate("image/", 3000);
        //        if (result.Length > 0)
        //        {
        //            ModelState.AddModelError("SmallImageFile1", result);
        //        }
        //    }
        //    if (mastheadAddVM.SmallImageFile2 != null)
        //    {
        //        string result = mastheadAddVM.SmallImageFile2.CheckValidate("image/", 3000);
        //        if (result.Length > 0)
        //        {
        //            ModelState.AddModelError("SmallImageFile2", result);
        //        }
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return View(mastheadAddVM);
        //    }
        //    await _mastheadService.CreateMastheadAsync(mastheadAddVM);
        //    return RedirectToAction("Index");
        //}


        [HttpGet]
        public async Task<IActionResult> Update(Guid mastheadId)
        {
            if (mastheadId == Guid.Empty) return NotFound();
            var masthead = await _mastheadService.UpdateMastheadById(mastheadId);
            return View(masthead);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MastheadUpdateViewModel mastheadUpVM)
        {
            if (mastheadUpVM.BigImageFile != null)
            {
                string result = mastheadUpVM.BigImageFile.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("BigImageFile", result);
                }
            }
            if (mastheadUpVM.SmallImageFile1 != null)
            {
                string result = mastheadUpVM.SmallImageFile1.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("SmallImageFile1", result);
                }
            }
            if (mastheadUpVM.SmallImageFile2 != null)
            {
                string result = mastheadUpVM.SmallImageFile2.CheckValidate("image/", 3000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("SmallImageFile2", result);
                }
            }
            if (!ModelState.IsValid) return View(mastheadUpVM);

            await _mastheadService.UpdateMastheadAsync(mastheadUpVM);
            return RedirectToAction("Index");
        }




    }
}
