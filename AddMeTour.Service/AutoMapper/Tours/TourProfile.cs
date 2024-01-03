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
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            CreateMap<TourViewModel, Entity.Entities.Tour.Tour>().ReverseMap();
            CreateMap<TourAddViewModel, Entity.Entities.Tour.Tour>().ReverseMap();
            CreateMap<TourUpdateViewModel, Entity.Entities.Tour.Tour>().ReverseMap();
        }
    }
}
