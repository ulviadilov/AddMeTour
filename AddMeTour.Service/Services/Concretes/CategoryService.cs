using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Data.UnitOfWorks.Concretes;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<CategoryViewModel>> GetAllCategoriesNonDeletedAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive == true);
            var map =  _mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }

        public async Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            var map = _mapper.Map<CategoryViewModel>(category);
            return map;
        }

        public async Task CreateCategoryAsync(CategoryAddViewModel categoryAddVM)
        {
            Category category = new Category
            {
                CategoryName = categoryAddVM.CategoryName,
                Id = categoryAddVM.Id,
                IsActive = categoryAddVM.IsActive
            };
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CategoryUpdateViewModel> UpdateCategoryByGuidAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            CategoryUpdateViewModel categoryUpdateVM = new CategoryUpdateViewModel
            {
                CategoryName=category.CategoryName,
                Id = category.Id,
                IsActive = category.IsActive
            };
            return categoryUpdateVM;
        }

        public async Task UpdateCategoryAsync(CategoryUpdateViewModel categoryUpdateVM)
        {
            Category category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.IsActive == true && x.Id == categoryUpdateVM.Id);
            category.IsActive = categoryUpdateVM.IsActive;
            category.CategoryName = categoryUpdateVM.CategoryName;
            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task SoftDeleteCategoryAsync(Guid id)
        {
            Category category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            category.IsActive = false;
            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteCategoryAsync(Guid id)
        {
            Category category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            await _unitOfWork.GetRepository<Category>().DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverCategoryAsync(Guid id)
        {
            Category category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            category.IsActive = true;
            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<CategoryViewModel>> GetAllPassiveCategories()
        {
            List<Category> categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }

    }
}
