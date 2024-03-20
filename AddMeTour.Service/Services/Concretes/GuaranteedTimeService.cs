using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class GuaranteedTimeService : IGuaranteedTimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GuaranteedTimeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GuaranteedTimeViewModel>> GetAllDatesNonDeletedAsync()
        {
            var dates = await _unitOfWork.GetRepository<GuaranteedTime>().GetAllAsync(x => x.IsActive);
            return _mapper.Map<List<GuaranteedTimeViewModel>>(dates);
        }

        public async Task<GuaranteedTimeViewModel> GetGuaranteedTimeByGuidAsync(Guid id)
        {
            var date = await _unitOfWork.GetRepository<GuaranteedTime>().GetByGuidAsync(id);
            return _mapper.Map<GuaranteedTimeViewModel>(date);
        }

        public async Task CreateGuaranteedTimeAsync(GuaranteedTimeAddViewModel GuaranteedTimeAddVM)
        {
            var GuaranteedTime = _mapper.Map<GuaranteedTime>(GuaranteedTimeAddVM);
            await _unitOfWork.GetRepository<GuaranteedTime>().AddAsync(GuaranteedTime);
            await _unitOfWork.SaveAsync();
        }

        public async Task<GuaranteedTimeViewModel> UpdateGuaranteedTimeByGuidAsync(Guid id)
        {
            var GuaranteedTime = await _unitOfWork.GetRepository<GuaranteedTime>().GetByGuidAsync(id);
            return _mapper.Map<GuaranteedTimeViewModel>(GuaranteedTime);
        }

        public async Task UpdateGuaranteedTimeAsync(GuaranteedTimeViewModel GuaranteedTimeVM)
        {
            var GuaranteedTime = await _unitOfWork.GetRepository<GuaranteedTime>().GetAsync(x => x.IsActive && x.Id == GuaranteedTimeVM.Id);

            if (GuaranteedTime != null)
            {
                _mapper.Map(GuaranteedTimeVM, GuaranteedTime);
                await _unitOfWork.GetRepository<GuaranteedTime>().UpdateAsync(GuaranteedTime);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("Belirtilen kategori bulunamadı.");
            }
        }

        public async Task SoftDeleteGuaranteedTimeAsync(Guid id)
        {
            var GuaranteedTime = await _unitOfWork.GetRepository<GuaranteedTime>().GetByGuidAsync(id);
            if (GuaranteedTime != null)
            {
                GuaranteedTime.IsActive = false;
                await _unitOfWork.GetRepository<GuaranteedTime>().UpdateAsync(GuaranteedTime);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task HardDeleteGuaranteedTimeAsync(Guid id)
        {
            var GuaranteedTime = await _unitOfWork.GetRepository<GuaranteedTime>().GetByGuidAsync(id);
            if (GuaranteedTime != null)
            {
                await _unitOfWork.GetRepository<GuaranteedTime>().DeleteAsync(GuaranteedTime);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task RecoverGuaranteedTimeAsync(Guid id)
        {
            var GuaranteedTime = await _unitOfWork.GetRepository<GuaranteedTime>().GetByGuidAsync(id);
            if (GuaranteedTime != null)
            {
                GuaranteedTime.IsActive = true;
                await _unitOfWork.GetRepository<GuaranteedTime>().UpdateAsync(GuaranteedTime);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<List<GuaranteedTimeViewModel>> GetAllPassiveGuaranteedTimes()
        {
            var GuaranteedTimes = await _unitOfWork.GetRepository<GuaranteedTime>().GetAllAsync(x => !x.IsActive);
            return _mapper.Map<List<GuaranteedTimeViewModel>>(GuaranteedTimes);
        }

    }
}
