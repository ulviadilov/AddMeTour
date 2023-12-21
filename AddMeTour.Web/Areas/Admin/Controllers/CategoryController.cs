using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(ICategoryService categoryService, IUnitOfWork unitOfWork)
        {
            _categoryService = categoryService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategoriesNonDeletedAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddViewModel categoryAddVM)
        {
            if (categoryAddVM == null && !ModelState.IsValid) return View(categoryAddVM);
            await _categoryService.CreateCategoryAsync(categoryAddVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            if (categoryId == Guid.Empty) return NotFound();
            return View(await _categoryService.UpdateCategoryByGuidAsync(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel categoryUpdateVM)
        {
            if(!ModelState.IsValid) return NotFound();
            await _categoryService.UpdateCategoryAsync(categoryUpdateVM);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> 
    }
}
