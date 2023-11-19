using AddMeTour.Entity.ViewModels;
using AddMeTour.Entity.ViewModels.Masthead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IMastheadService
    {
        Task<MastheadViewModel> GetMastheadByGuidNonDeletedAsync(Guid id);
        Task<MastheadUpdateViewModel> UpdateMastheadById(Guid id);
        Task CreateMastheadAsync(MastheadAddViewModel mastheadAddVM);
        Task UpdateMastheadAsync(MastheadUpdateViewModel mastheadUpVM);
        Task<List<MastheadViewModel>> GetAllMastheadsNonDeletedAsync();
    }
}
