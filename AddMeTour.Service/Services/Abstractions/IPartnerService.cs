using AddMeTour.Entity.ViewModels.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IPartnerService
    {
        Task<List<PartnerViewModel>> GetAllPartnersNonDeletedAsync();
        Task<PartnerViewModel> GetPartnerByGuidNonDeletedAsync(Guid id);
        Task CreatePartnerAsync(PartnerAddViewModel PartnerAddVM);
        PartnerUpdateViewModel UpdatePartnerById(Guid id);
        Task UpdatePartnerAsync(PartnerUpdateViewModel PartnerUpVM);
        Task SafeDeletePartnerAsync(Guid PartnerId);
        Task HardDeleteAsync(Guid id);
        Task RecoverPartnerAsync(Guid id);
        Task<List<PartnerViewModel>> GetAllPassivePartners();
    }
}
