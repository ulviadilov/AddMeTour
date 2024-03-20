using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tours
{
    public class GuaranteedTimeProfile : Profile
    {
        public GuaranteedTimeProfile()
        {
            CreateMap<GuaranteedTimeViewModel, GuaranteedTime>().ReverseMap();
            CreateMap<GuaranteedTimeAddViewModel, GuaranteedTime>().ReverseMap();
        }
    }
}
