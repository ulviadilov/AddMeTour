using AddMeTour.Entity.ViewModels.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IGuaranteedTimeService
    {
        Task<List<GuaranteedTimeViewModel>> GetAllDatesNonDeletedAsync();
        Task<GuaranteedTimeViewModel> GetGuaranteedTimeByGuidAsync(Guid id);
        Task CreateGuaranteedTimeAsync(GuaranteedTimeAddViewModel GuaranteedTimeAddVM);
        Task<GuaranteedTimeViewModel> UpdateGuaranteedTimeByGuidAsync(Guid id);
        Task UpdateGuaranteedTimeAsync(GuaranteedTimeViewModel GuaranteedTimeVM);
        Task SoftDeleteGuaranteedTimeAsync(Guid id);
        Task HardDeleteGuaranteedTimeAsync(Guid id);

    }
}
