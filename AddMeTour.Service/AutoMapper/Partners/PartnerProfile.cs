using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Partners;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Partners
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<PartnerViewModel, Partner>().ReverseMap();
            CreateMap<PartnerAddViewModel, Partner>().ReverseMap();
            CreateMap<PartnerUpdateViewModel, Partner>().ReverseMap();

        }
    }
}
