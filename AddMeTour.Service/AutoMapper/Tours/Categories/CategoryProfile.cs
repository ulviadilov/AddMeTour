using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tours.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryAddViewModel, Category>().ReverseMap();
            CreateMap<CategoryUpdateViewModel, Category>().ReverseMap();
        }


    }
}
