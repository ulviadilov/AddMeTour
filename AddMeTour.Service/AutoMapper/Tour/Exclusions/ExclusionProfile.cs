using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Exclusion;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tour.Exclusions
{
    public class ExclusionProfile : Profile
    {
        public ExclusionProfile()
        {
            CreateMap<ExclusionViewModel, Exclusion>().ReverseMap();
            CreateMap<ExclusionAddViewModel, Exclusion>().ReverseMap();      
        }
    }
}
