using AddMeTour.Entity.ViewModels.Tour.Exclusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IExclusionService
    {
        Task<List<ExclusionViewModel>> GetAllExclusionsNonDeletedAsync();
        Task<ExclusionViewModel> GetExclusionByGuidAsync(Guid id);
        Task CreateExclusionAsync(ExclusionAddViewModel exclusionAddVM);
        Task<ExclusionViewModel> UpdateExclusionByGuidAsync(Guid id);
        Task UpdateExclusionAsync(ExclusionViewModel exclusionVM);
        Task SoftDeleteExclusionAsync(Guid id);
        Task HardDeleteExclusionAsync(Guid id);
        Task RecoverExclusionAsync(Guid id);
        Task<List<ExclusionViewModel>> GetAllPassiveExclusions();
    }
}
