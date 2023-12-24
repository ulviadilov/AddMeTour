using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<LanguageViewModel>> GetAllLanguagesNonDeletedAsync()
        {
            var languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<LanguageViewModel>>(languages);
            return map;
        }

        public async Task<LanguageViewModel> GetLanguageByGuidAsync(Guid id)
        {
            var language = await _unitOfWork.GetRepository<Language>().GetByGuidAsync(id);
            var map = _mapper.Map<LanguageViewModel>(language);
            return map;
        }

        public async Task CreateLanguageAsync(LanguageAddViewModel languageAddVM)
        {
            Language language = new Language
            {
                LanguageName = languageAddVM.LanguageName,
                Id = languageAddVM.Id,
                IsActive = languageAddVM.IsActive
            };
            await _unitOfWork.GetRepository<Language>().AddAsync(language);
            await _unitOfWork.SaveAsync();
        }

        public async Task<LanguageViewModel> UpdateLanguageByGuidAsync(Guid id)
        {
            var language = await _unitOfWork.GetRepository<Language>().GetByGuidAsync(id);
            LanguageViewModel languageVM = new LanguageViewModel
            {
                LanguageName = language.LanguageName,
                Id = language.Id,
                IsActive = language.IsActive
            };
            return languageVM;
        }

        public async Task UpdateLanguageAsync(LanguageViewModel languageVM)
        {
            Language language = await _unitOfWork.GetRepository<Language>().GetAsync(x => x.IsActive == true && x.Id == languageVM.Id);
            language.IsActive = languageVM.IsActive;
            language.LanguageName = languageVM.LanguageName;
            await _unitOfWork.GetRepository<Language>().UpdateAsync(language);
            await _unitOfWork.SaveAsync();
        }

        public async Task SoftDeleteLanguageAsync(Guid id)
        {
            Language language = await _unitOfWork.GetRepository<Language>().GetByGuidAsync(id);
            language.IsActive = false;
            await _unitOfWork.GetRepository<Language>().UpdateAsync(language);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteLanguageAsync(Guid id)
        {
            Language language = await _unitOfWork.GetRepository<Language>().GetByGuidAsync(id);
            await _unitOfWork.GetRepository<Language>().DeleteAsync(language);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverLanguageAsync(Guid id)
        {
            Language language = await _unitOfWork.GetRepository<Language>().GetByGuidAsync(id);
            language.IsActive = true;
            await _unitOfWork.GetRepository<Language>().UpdateAsync(language);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<LanguageViewModel>> GetAllPassiveLanguages()
        {
            List<Language> languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<LanguageViewModel>>(languages);
            return map;
        }
    }
}
