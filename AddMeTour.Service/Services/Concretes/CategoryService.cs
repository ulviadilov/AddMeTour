using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesNonDeletedAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive);
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel> GetCategoryByGuidAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task CreateCategoryAsync(CategoryAddViewModel categoryAddVM)
        {
            var category = _mapper.Map<Category>(categoryAddVM);
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CategoryUpdateViewModel> UpdateCategoryByGuidAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            return _mapper.Map<CategoryUpdateViewModel>(category);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateViewModel categoryUpdateVM)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.IsActive && x.Id == categoryUpdateVM.Id);

            if (category != null)
            {
                _mapper.Map(categoryUpdateVM, category);
                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                // Eğer belirtilen kategori bulunamazsa, uygun bir durumu ele alabilirsiniz.
                // Örneğin, bir exception fırlatabilir veya bir hata logu oluşturabilirsiniz.
                throw new InvalidOperationException("Belirtilen kategori bulunamadı.");
            }
        }

        public async Task SoftDeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            if (category != null)
            {
                category.IsActive = false;
                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveAsync();
            }
            // else: Handle the case where the category is not found.
        }

        public async Task HardDeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            if (category != null)
            {
                await _unitOfWork.GetRepository<Category>().DeleteAsync(category);
                await _unitOfWork.SaveAsync();
            }
            // else: Handle the case where the category is not found.
        }

        public async Task RecoverCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            if (category != null)
            {
                category.IsActive = true;
                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveAsync();
            }
            // else: Handle the case where the category is not found.
        }

        public async Task<List<CategoryViewModel>> GetAllPassiveCategories()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsActive);
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }
    }
}
