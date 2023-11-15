using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Service.Services.Abstraction;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeatureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task CreateFeatureAsync(FeatureViewModel featureVM)
        {
            Feature feature = new Feature
            {
                Title = featureVM.Title,
                Description = featureVM.Description,
                IsActive = true,
                Id = Guid.NewGuid()
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
