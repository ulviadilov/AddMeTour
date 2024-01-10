using AddMeTour.Entity.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ISettingService
    {
        Task<List<SettingViewModel>> GetAllSettingsAsync();
        Task<SettingViewModel> GetSettingByGuidAsync(Guid id);
        Task<SettingUpdateViewModel> UpdateSettingByGuidAsync(Guid id);
        Task UpdateSettingAsync(SettingUpdateViewModel settingViewModel);
    }
}
