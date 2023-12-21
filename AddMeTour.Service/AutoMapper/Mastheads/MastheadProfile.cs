using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Masthead;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Mastheads
{
    public class MastheadProfile : Profile
    {
        public MastheadProfile() 
        {
            CreateMap<MastheadViewModel, Masthead>().ReverseMap();
            CreateMap<MastheadAddViewModel, Masthead>().ReverseMap();
            CreateMap<MastheadUpdateViewModel, Masthead>().ReverseMap();
        }
    }
}
