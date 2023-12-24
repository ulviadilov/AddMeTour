using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IInclusionService
    {
        Task<List<InclusionViewModel>> GetAllInclusionsNonDeletedAsync();
        Task<InclusionViewModel> GetInclusionByGuidAsync(Guid id);
        Task CreateInclusionAsync(InclusionAddViewModel inclusionAddVM);
        Task<InclusionUpdateViewModel> UpdateInclusionByGuidAsync(Guid id);
        Task UpdateInclusionAsync(InclusionUpdateViewModel inclusionUpdateVM);
        Task SoftDeleteInclusionAsync(Guid id);
        Task HardDeleteInclusionAsync(Guid id);
        Task RecoverInclusionAsync(Guid id);
        Task<List<InclusionViewModel>> GetAllPassiveInclusions();
    }
}
