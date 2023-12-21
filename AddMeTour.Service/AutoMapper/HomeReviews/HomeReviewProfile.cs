using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.HomeReviews;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.HomeReviews
{
    public class HomeReviewProfile : Profile
    {
        public HomeReviewProfile() 
        {
            CreateMap<HomeReviewViewModel, HomeReview>().ReverseMap();
            CreateMap<HomeReviewAddViewModel, HomeReview>().ReverseMap();
            CreateMap<HomeReviewUpdateViewModel, HomeReview>().ReverseMap();
        }
    }
}
