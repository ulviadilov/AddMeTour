using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Setting;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Settings
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<SettingViewModel, Setting>().ReverseMap();
            CreateMap<SettingUpdateViewModel, Setting>().ReverseMap();    
        }
    }
}
