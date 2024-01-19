using AddMeTour.Entity.ViewModels.Tour.Destination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IDestinationService
    {
        Task<List<DestinationViewModel>> GetAllDestinationsNonDeletedAsync();
        Task<DestinationViewModel> GetDestinationByGuidAsync(Guid id);
        Task CreateDestinationAsync(DestinationAddViewModel DestinationAddVM);
        Task<DestinationViewModel> UpdateDestinationByGuidAsync(Guid id);
        Task UpdateDestinationAsync(DestinationViewModel destinationVM);
        Task SoftDeleteDestinationAsync(Guid id);
        Task HardDeleteDestinationAsync(Guid id);
        Task RecoverDestinationAsync(Guid id);
        Task<List<DestinationViewModel>> GetAllPassiveDestinations();
    }
}
