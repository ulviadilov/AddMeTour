using AddMeTour.Service.AutoMapper.Tour.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ILanguageService
    {
        Task<List<LanguageViewModel>> GetAllLanguagesNonDeletedAsync();
        Task<LanguageViewModel> GetLanguageByGuidAsync(Guid id);
        Task CreateLanguageAsync(LanguageAddViewModel languageAddVM);
        Task<LanguageViewModel> UpdateLanguageByGuidAsync(Guid id);
        Task UpdateLanguageAsync(LanguageViewModel languageVM);
        Task SoftDeleteLanguageAsync(Guid id);
        Task HardDeleteLanguageAsync(Guid id);
        Task RecoverLanguageAsync(Guid id);
        Task<List<LanguageViewModel>> GetAllPassiveLanguages();
    }
}
