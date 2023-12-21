using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.ViewModels.HomeReviews;
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
    public class HomeReviewService: IHomeReviewService
    {
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        public HomeReviewService(IMapper mapper, IHostingEnvironment env, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _env = env;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HomeReviewViewModel>> GetAllHomeReviewsNonDeletedAsync()
        {
            var homeReviews = await _unitOfWork.GetRepository<HomeReview>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<HomeReviewViewModel>>(homeReviews);
            return map;
        }

        public async Task<HomeReviewViewModel> GetHomeReviewByGuidNonDeletedAsync(Guid id)
        {
            HomeReview homeReview = await _unitOfWork.GetRepository<HomeReview>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<HomeReviewViewModel>(homeReview);
            return map;
        }

        public async Task CreateHomeReviewAsync(HomeReviewAddViewModel homeReviewAddVM)
        {
            HomeReview homeReview = new HomeReview
            {
                Title = homeReviewAddVM.Title,
                Description = homeReviewAddVM.Description,
                IsActive = homeReviewAddVM.IsActive,
                Id = homeReviewAddVM.Id,
                AvatarUrl = homeReviewAddVM.AvatarImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "avatars")),
                UserCountry = homeReviewAddVM.UserCountry,
                Username = homeReviewAddVM.Username
            };
            await _unitOfWork.GetRepository<HomeReview>().AddAsync(homeReview);
            await _unitOfWork.SaveAsync();
        }

        public HomeReviewUpdateViewModel UpdateHomeReviewById(Guid id)
        {
            var homeReview = _unitOfWork.GetRepository<HomeReview>().GetByGuidAsync(id);
            HomeReviewUpdateViewModel homeReviewUpdateVM = new HomeReviewUpdateViewModel
            {
                Description = homeReview.Result.Description,
                Id = homeReview.Result.Id,
                Title = homeReview.Result.Title,
                UserCountry = homeReview.Result.UserCountry,
                Username = homeReview.Result.Username
                
            };
            return homeReviewUpdateVM;
        }

        public async Task UpdateFeatureAsync(HomeReviewUpdateViewModel homeReviewUpdateVM)
        {
            HomeReview homeReview = await _unitOfWork.GetRepository<HomeReview>().GetAsync(x => x.IsActive && x.Id == homeReviewUpdateVM.Id);
            if (homeReviewUpdateVM.AvatarImageFile is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "avatars", homeReview.AvatarUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                homeReview.AvatarUrl = homeReviewUpdateVM.AvatarImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "avatars"));
            }
            homeReview.Title = homeReviewUpdateVM.Title;
            homeReview.Description = homeReviewUpdateVM.Description;
            homeReview.Username = homeReviewUpdateVM.Username;
            homeReview.UserCountry = homeReviewUpdateVM.UserCountry;

            await _unitOfWork.GetRepository<HomeReview>().UpdateAsync(homeReview);
            await _unitOfWork.SaveAsync();
        }

        public async Task SafeDeleteHomeReviewAsync(Guid homeReviewId)
        {
            HomeReview homeReview = await _unitOfWork.GetRepository<HomeReview>().GetByGuidAsync(homeReviewId);
            homeReview.IsActive = false;
            await _unitOfWork.GetRepository<HomeReview>().UpdateAsync(homeReview);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            HomeReview homeReview = await _unitOfWork.GetRepository<HomeReview>().GetByGuidAsync(id);
            string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "avatars", homeReview.AvatarUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            await _unitOfWork.GetRepository<HomeReview>().DeleteAsync(homeReview);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverHomeReviewAsync(Guid id)
        {
            HomeReview homeReview = await _unitOfWork.GetRepository<HomeReview>().GetByGuidAsync(id);
            homeReview.IsActive = true;
        }

        public async Task<List<HomeReviewViewModel>> GetAllPassiveHomeReviews()
        {
            var reviews = await _unitOfWork.GetRepository<HomeReview>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<HomeReviewViewModel>>(reviews);
            return map;
        }

    }
}
