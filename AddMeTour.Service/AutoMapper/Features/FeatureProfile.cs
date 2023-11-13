using AddMeTour.Entity.Entities;
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
        protected FeatureProfile()
        {
            CreateMap<FeatureViewModel, Feature>().ReverseMap();
        }
    }
}
