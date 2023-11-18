using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstraction
{
    public interface IFeatureService
    {
        Task<List<FeatureViewModel>> GetAllFeaturesNonDeletedAsync();
        Task<FeatureViewModel> GetFeatureByGuidNonDeletedAsync(Guid id);
        Task CreateFeatureAsync(FeatureAddViewModel featureAddVM);
        Task SafeDeleteFeatureAsync(Guid featureId);
        Task UpdateFeatureAsync(FeatureUpdateViewModel featureUpVM);
        Task<FeatureUpdateViewModel> UpdateFeatureById(Guid id);
        Task<List<FeatureViewModel>> GetAllPassiveFeatures();
        Task HardDeleteAsync(Guid id);
        Task RecoverFeatureAsync(Guid id);
    }
}
