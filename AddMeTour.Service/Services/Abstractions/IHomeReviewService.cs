using AddMeTour.Entity.ViewModels.HomeReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IHomeReviewService
    {
        Task<List<HomeReviewViewModel>> GetAllHomeReviewsNonDeletedAsync();
        Task<HomeReviewViewModel> GetHomeReviewByGuidNonDeletedAsync(Guid id);
        Task CreateHomeReviewAsync(HomeReviewAddViewModel homeReviewAddVM);
        HomeReviewUpdateViewModel UpdateHomeReviewById(Guid id);
        Task UpdateFeatureAsync(HomeReviewUpdateViewModel homeReviewUpdateVM);
        Task SafeDeleteHomeReviewAsync(Guid homeReviewId);
        Task HardDeleteAsync(Guid id);
        Task RecoverHomeReviewAsync(Guid id);
        Task<List<HomeReviewViewModel>> GetAllPassiveHomeReviews();
    }
}
