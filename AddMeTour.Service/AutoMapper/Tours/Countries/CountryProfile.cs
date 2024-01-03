using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tours.Countries
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryViewModel, Country>().ReverseMap();
            CreateMap<CountryAddViewModel, Country>().ReverseMap();
            CreateMap<CountryUpdateViewModel, Country>().ReverseMap();
        }
    }
}
