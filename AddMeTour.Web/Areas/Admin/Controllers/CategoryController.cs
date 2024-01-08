using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeletedAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAddViewModel categoryAddVM)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryAddVM);
                return RedirectToAction("Index");
            }

            return View(categoryAddVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            var categoryUpdateVM = await _categoryService.UpdateCategoryByGuidAsync(categoryId);
            if (categoryUpdateVM == null)
            {
                return NotFound();
            }

            return View(categoryUpdateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateViewModel categoryUpdateVM)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(categoryUpdateVM);
                return RedirectToAction("Index");
            }

            return View(categoryUpdateVM);
        }

        public async Task<IActionResult> SoftDelete(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            await _categoryService.SoftDeleteCategoryAsync(categoryId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HardDelete(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            await _categoryService.HardDeleteCategoryAsync(categoryId);
            return RedirectToAction("DeletedCategories");
        }

        public async Task<IActionResult> DeletedCategories()
        {
            var deletedCategories = await _categoryService.GetAllPassiveCategories();
            return View(deletedCategories);
        }

        public async Task<IActionResult> RecoverCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            await _categoryService.RecoverCategoryAsync(categoryId);
            return RedirectToAction("Index");
        }
    }
}
