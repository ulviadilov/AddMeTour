using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tour.Inclusions
{
    public class InclusionProfile : Profile
    {
        public InclusionProfile()
        {
            CreateMap<InclusionViewModel, Inclusion>().ReverseMap();
            CreateMap<InclusionAddViewModel, Inclusion>().ReverseMap();
            CreateMap<InclusionUpdateViewModel, Inclusion>().ReverseMap();
        }
    }
}
