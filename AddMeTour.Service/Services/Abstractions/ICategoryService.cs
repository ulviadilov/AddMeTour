using AddMeTour.Entity.ViewModels.Tour.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesNonDeletedAsync();
        Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id);
        Task CreateCategoryAsync(CategoryAddViewModel categoryAddVM);
        Task<CategoryUpdateViewModel> UpdateCategoryByGuidAsync(Guid id);
        Task UpdateCategoryAsync(CategoryUpdateViewModel categoryUpdateVM);
        Task SoftDeleteCategoryAsync(Guid id);
        Task HardDeleteCategoryAsync(Guid id);
        Task RecoverCategoryAsync(Guid id);
        Task<List<CategoryViewModel>> GetAllPassiveCategories();
    }
}
