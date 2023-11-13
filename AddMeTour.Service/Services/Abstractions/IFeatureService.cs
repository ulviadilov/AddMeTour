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
        Task<List<FeatureViewModel>> GetAllFeaturesAsync();
    }
}
