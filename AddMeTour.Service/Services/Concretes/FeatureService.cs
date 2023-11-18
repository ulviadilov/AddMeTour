using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities;
using AddMeTour.Entity.Enums;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstraction;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace AddMeTour.Service.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public FeatureService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<FeatureViewModel>> GetAllFeaturesNonDeletedAsync()
        {
            var features = await _unitOfWork.GetRepository<Feature>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<FeatureViewModel>>(features);
            
            return map;
            
        }

        public async Task<FeatureViewModel> GetFeatureByGuidNonDeletedAsync(Guid id)
        {
            Feature feature = await _unitOfWork.GetRepository<Feature>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<FeatureViewModel>(feature);
            return map;
        }

        public async Task CreateFeatureAsync(FeatureAddViewModel featureAddVM)
        {
            Feature feature = new Feature
            {
                Title = featureAddVM.Title,
                Description = featureAddVM.Description,
                IsActive = featureAddVM.IsActive,
                Id = Guid.NewGuid(),
                ImageUrl = featureAddVM.ImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "features"))
            };
            await _unitOfWork.GetRepository<Feature>().AddAsync(feature);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateFeatureAsync(FeatureViewModel featureVM)
        {
            Feature feature = await _unitOfWork.GetRepository<Feature>().GetAsync(x => x.IsActive && x.Id == featureVM.Id);
            feature.Title = featureVM.Title;
            feature.Description = featureVM.Description;

            await _unitOfWork.GetRepository<Feature>().UpdateAsync(feature);
            await _unitOfWork.SaveAsync();
        }

        public async Task SafeDeleteFeatureAsync(Guid featureId)
        {
            Feature feature = await _unitOfWork.GetRepository<Feature>().GetByGuidAsync(featureId);
            feature.IsActive = false;
            await _unitOfWork.GetRepository<Feature>().UpdateAsync(feature);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteAsync(FeatureViewModel featureVm)
        {
            Feature feature = await _unitOfWork.GetRepository<Feature>().GetAsync(x => x.Id == featureVm.Id);
            await _unitOfWork.GetRepository<Feature>().DeleteAsync(feature);
            await _unitOfWork.SaveAsync();
        }

    }
}
