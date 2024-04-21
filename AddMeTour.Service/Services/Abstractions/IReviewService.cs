using AddMeTour.Entity.ViewModels.Review;
using AddMeTour.Entity.ViewModels.Tour.Category;

namespace AddMeTour.Service.Services.Abstractions;

public interface IReviewService
{
    Task<List<ReviewVM>> GetAllReviewsAsync();
    Task<ReviewVM> GetReviewByGuidAsync(Guid id);
    Task CreateReviewAsync(AddReviewVM reviewAddVM);
    Task UpdateReviewAsync(ReviewVM reviewVM);

    Task SoftDeleteReviewAsync(Guid id);
    Task HardDeleteReviewAsync(Guid id);
    Task RecoverReviewAsync(Guid id);
    Task<List<ReviewVM>> GetAllPassiveReviews();
}
