using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels.Rating;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RatingViewModel> GetRatingByGuidNonDeletedAsync(Guid id)
        {
            Rating rating = await _unitOfWork.GetRepository<Rating>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<RatingViewModel>(rating);
            return map;
        }

        public async Task<List<RatingViewModel>> GetAllRatingsNonDeletedAsync()
        {
            var rating = await _unitOfWork.GetRepository<Rating>().GetAllAsync(x => x.IsActive);
            var map = _mapper.Map<List<RatingViewModel>>(rating);
            return map;
        }


        public async Task CreateRatingAsync(RatingAddViewModel ratingAddVM)
        {
            Rating rating = new Rating
            {
                Title1 = ratingAddVM.Title1,
                Title2 = ratingAddVM.Title2,
                Description = ratingAddVM.Description,
                IsActive = ratingAddVM.IsActice,
                Id = ratingAddVM.Id,
                OverallRating = ratingAddVM.OverallRating,
                TotalGuestCount = ratingAddVM.TotalGuestCount
            };
            await _unitOfWork.GetRepository<Rating>().AddAsync(rating);
            await _unitOfWork.SaveAsync();
        }

        public RatingUpdateViewModel UpdateRatingById(Guid id)
        {
            var rating = _unitOfWork.GetRepository<Rating>().GetByGuidAsync(id);
            RatingUpdateViewModel ratingUpdateVM = new RatingUpdateViewModel
            {
                Title1 = rating.Result.Title1,
                Title2 = rating.Result.Title2,
                Description = rating.Result.Description,
                Id = rating.Result.Id,
                OverallRating = rating.Result.OverallRating,
                TotalGuestCount = rating.Result.TotalGuestCount
            };
            return ratingUpdateVM;
        }

        public async Task UpdateRatingAsync(RatingUpdateViewModel ratingUpdateVM)
        {
            Rating rating = await _unitOfWork.GetRepository<Rating>().GetAsync(x => x.IsActive && x.Id == ratingUpdateVM.Id);
            rating.Title1 = ratingUpdateVM.Title1;
            rating.Title2 = ratingUpdateVM.Title2;
            rating.Description = ratingUpdateVM.Description;
            rating.OverallRating = ratingUpdateVM.OverallRating;
            rating.TotalGuestCount = ratingUpdateVM.TotalGuestCount;

            await _unitOfWork.GetRepository<Rating>().UpdateAsync(rating);
            await _unitOfWork.SaveAsync();
        }
    }
}
