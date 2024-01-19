using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Destination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tours.Destinations
{
    public class DestinationProfile : Profile
    {
        public DestinationProfile()
        {
            CreateMap<DestinationViewModel, Destination>().ReverseMap();
            CreateMap<DestinationAddViewModel, Destination>().ReverseMap();
        }
    }
}
