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
        public async Task<List<FeatureViewModel>> GetAllFeaturesAsync()
        {
            var features = await _unitOfWork.GetRepository<Feature>().GetAllAsync();
            var map = _mapper.Map<List<FeatureViewModel>>(features);
            return map;
        }

        public async Task<FeatureViewModel> GetFeatureByGuidAsync(Guid id)
        {
            Feature feature = await _unitOfWork.GetRepository<Feature>().GetByGuidAsync(id);
            var map = _mapper.Map<FeatureViewModel>(feature);
            return map;
        }
    }
}
