using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Rating;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Ratings
{
    public class RatingProfile : Profile
    {
        public RatingProfile() 
        {
            CreateMap<RatingViewModel,Rating>().ReverseMap();
            CreateMap<RatingAddViewModel,Rating>().ReverseMap();
            CreateMap<RatingUpdateViewModel,Rating>().ReverseMap();
        }
    }
}
