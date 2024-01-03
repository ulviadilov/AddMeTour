using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tours.Languages
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<LanguageViewModel, Language>().ReverseMap();
            CreateMap<LanguageAddViewModel, Language>().ReverseMap();
        }
    }
}
