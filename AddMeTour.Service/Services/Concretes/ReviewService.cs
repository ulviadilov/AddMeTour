using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Review;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Review;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;

namespace AddMeTour.Service.Services.Concretes;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task CreateReviewAsync(AddReviewVM reviewAddVM)
    {
        var review = _mapper.Map<Review>(reviewAddVM);
        await _unitOfWork.GetRepository<Review>().AddAsync(review);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<ReviewVM>> GetAllPassiveReviews()
    {
        var reviews = await _unitOfWork.GetRepository<Review>().GetAllAsync(x => !x.IsActive);
        return _mapper.Map<List<ReviewVM>>(reviews);
    }

    public async Task<List<ReviewVM>> GetAllReviewsAsync()
    {
        var reviews = await _unitOfWork.GetRepository<Review>().GetAllAsync();
        return _mapper.Map<List<ReviewVM>>(reviews);
    }

    public async Task<ReviewVM> GetReviewByGuidAsync(Guid id)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetByGuidAsync(id);
        return _mapper.Map<ReviewVM>(review);
    }

    public async Task HardDeleteReviewAsync(Guid id)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetByGuidAsync(id);
        if (review != null)
        {
            await _unitOfWork.GetRepository<Review>().DeleteAsync(review);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task RecoverReviewAsync(Guid id)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetByGuidAsync(id);
        if (review != null)
        {
            review.IsActive = true;
            await _unitOfWork.GetRepository<Review>().UpdateAsync(review);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task SoftDeleteReviewAsync(Guid id)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetByGuidAsync(id);
        if (review != null)
        {
            review.IsActive = false;
            await _unitOfWork.GetRepository<Review>().UpdateAsync(review);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task UpdateReviewAsync(ReviewVM reviewVM)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetAsync(x =>x.Id == reviewVM.Id);

        if (review != null)
        {
            _mapper.Map(reviewVM, review);
            await _unitOfWork.GetRepository<Review>().UpdateAsync(review);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new InvalidOperationException("Belirtilen review bulunamdi!.");
        }
    }
}
