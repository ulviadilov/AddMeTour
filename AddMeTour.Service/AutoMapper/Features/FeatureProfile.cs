using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Features;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Features
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<FeatureViewModel, Feature>().ReverseMap();
            CreateMap<FeatureAddViewModel, Feature>().ReverseMap();
            CreateMap<FeatureUpdateViewModel, Feature>().ReverseMap();
        }
    }
}
