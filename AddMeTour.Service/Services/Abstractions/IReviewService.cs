using AddMeTour.Entity.ViewModels.Review;

namespace AddMeTour.Service.Services.Abstractions;

public interface IReviewService
{
    Task<List<ReviewVM>> GetAllReviewsNonDeletedAsync();
    Task<ReviewVM> GetReviewByGuidAsync(Guid id);
    Task CreateReviewAsync(AddReviewVM reviewAddVM);
    Task SoftDeleteReviewAsync(Guid id);
    Task HardDeleteReviewAsync(Guid id);
    Task RecoverReviewAsync(Guid id);
    Task<List<ReviewVM>> GetAllPassiveReviews();
}
