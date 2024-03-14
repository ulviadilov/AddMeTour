using AddMeTour.Entity.ViewModels.Review;
using AutoMapper;

namespace AddMeTour.Service.AutoMapper.Review;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewVM, Entity.Entities.Review.Review>().ReverseMap();
        CreateMap<AddReviewVM, Entity.Entities.Review.Review>().ReverseMap();
    }
}
