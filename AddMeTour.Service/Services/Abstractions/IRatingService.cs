using AddMeTour.Entity.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IRatingService
    {
        Task<RatingViewModel> GetRatingByGuidNonDeletedAsync(Guid id);
        Task<List<RatingViewModel>> GetAllRatingsNonDeletedAsync();
        Task CreateRatingAsync(RatingAddViewModel ratingAddVM);
        RatingUpdateViewModel UpdateRatingById(Guid id);
        Task UpdateRatingAsync(RatingUpdateViewModel ratingUpdateVM);
    }
}
