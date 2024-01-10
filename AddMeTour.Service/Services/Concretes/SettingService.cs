using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Setting;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SettingViewModel>> GetAllSettingsAsync()
        {
            var settings = await _unitOfWork.GetRepository<Setting>().GetAllAsync();
            var map = _mapper.Map<List<SettingViewModel>>(settings);
            return map;
        }

        public async Task<SettingViewModel> GetSettingByGuidAsync(Guid id)
        {
            var setting = await _unitOfWork.GetRepository<Setting>().GetByGuidAsync(id);
            return _mapper.Map<SettingViewModel>(setting);
        }

        public async Task<SettingUpdateViewModel> UpdateSettingByGuidAsync(Guid id)
        {
            var setting = await _unitOfWork.GetRepository<Setting>().GetByGuidAsync(id);
            return _mapper.Map<SettingUpdateViewModel>(setting);
        }

        public async Task UpdateSettingAsync(SettingUpdateViewModel settingUpVM)
        {
            var setting = await _unitOfWork.GetRepository<Setting>().GetAsync(x => x.IsActive && x.Id == settingUpVM.Id);

            if (setting != null)
            {
                _mapper.Map(settingUpVM, setting);
                await _unitOfWork.GetRepository<Setting>().UpdateAsync(setting);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("Belirtilen kategori bulunamadı.");
            }
        }



    }
}
