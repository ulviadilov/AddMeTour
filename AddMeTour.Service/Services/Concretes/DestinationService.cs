using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Destination;
using AddMeTour.Entity.ViewModels.Tour.Destination;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class DestinationService : IDestinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DestinationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<DestinationViewModel>> GetAllDestinationsNonDeletedAsync()
        {
            var destinations  = await _unitOfWork.GetRepository<Destination>().GetAllAsync(x => x.IsActive);
            return _mapper.Map<List<DestinationViewModel>>(destinations);
        }

        public async Task<DestinationViewModel> GetDestinationByGuidAsync(Guid id)
        {
            var Destination = await _unitOfWork.GetRepository<Destination>().GetByGuidAsync(id);
            return _mapper.Map<DestinationViewModel>(Destination);
        }

        public async Task CreateDestinationAsync(DestinationAddViewModel DestinationAddVM)
        {
            var Destination = _mapper.Map<Destination>(DestinationAddVM);
            await _unitOfWork.GetRepository<Destination>().AddAsync(Destination);
            await _unitOfWork.SaveAsync();
        }

        public async Task<DestinationViewModel> UpdateDestinationByGuidAsync(Guid id)
        {
            var destination = await _unitOfWork.GetRepository<Destination>().GetByGuidAsync(id);
            return _mapper.Map<DestinationViewModel>(destination);
        }

        public async Task UpdateDestinationAsync(DestinationViewModel destinationVM)
        {
            var destination = await _unitOfWork.GetRepository<Destination>().GetAsync(x => x.IsActive && x.Id == destinationVM.Id);

            if (destination != null)
            {
                _mapper.Map(destinationVM, destination);
                await _unitOfWork.GetRepository<Destination>().UpdateAsync(destination);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("Belirtilen kategori bulunamadı.");
            }
        }

        public async Task SoftDeleteDestinationAsync(Guid id)
        {
            var Destination = await _unitOfWork.GetRepository<Destination>().GetByGuidAsync(id);
            if (Destination != null)
            {
                Destination.IsActive = false;
                await _unitOfWork.GetRepository<Destination>().UpdateAsync(Destination);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task HardDeleteDestinationAsync(Guid id)
        {
            var Destination = await _unitOfWork.GetRepository<Destination>().GetByGuidAsync(id);
            if (Destination != null)
            {
                await _unitOfWork.GetRepository<Destination>().DeleteAsync(Destination);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task RecoverDestinationAsync(Guid id)
        {
            var Destination = await _unitOfWork.GetRepository<Destination>().GetByGuidAsync(id);
            if (Destination != null)
            {
                Destination.IsActive = true;
                await _unitOfWork.GetRepository<Destination>().UpdateAsync(Destination);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<List<DestinationViewModel>> GetAllPassiveDestinations()
        {
            var destinations = await _unitOfWork.GetRepository<Destination>().GetAllAsync(x => !x.IsActive);
            return _mapper.Map<List<DestinationViewModel>>(destinations);
        }

    }
}
