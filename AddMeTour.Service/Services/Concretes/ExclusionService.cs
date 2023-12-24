using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Exclusion;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class ExclusionService : IExclusionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExclusionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<ExclusionViewModel>> GetAllExclusionsNonDeletedAsync()
        {
            var exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<ExclusionViewModel>>(exclusions);
            return map;
        }

        public async Task<ExclusionViewModel> GetExclusionByGuidAsync(Guid id)
        {
            var exclusion = await _unitOfWork.GetRepository<Exclusion>().GetByGuidAsync(id);
            var map = _mapper.Map<ExclusionViewModel>(exclusion);
            return map;
        }

        public async Task CreateExclusionAsync(ExclusionAddViewModel exclusionAddVM)
        {
            Exclusion exclusion = new Exclusion
            {
                ExclusionString = exclusionAddVM.ExcluisonString,
                Id = exclusionAddVM.Id,
                IsActive = exclusionAddVM.IsActive
            };
            await _unitOfWork.GetRepository<Exclusion>().AddAsync(exclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ExclusionViewModel> UpdateExclusionByGuidAsync(Guid id)
        {
            var exclusion = await _unitOfWork.GetRepository<Exclusion>().GetByGuidAsync(id);
            ExclusionViewModel exclusionVM = new ExclusionViewModel
            {
                ExclusionString = exclusion.ExclusionString,
                Id = exclusion.Id,
                IsActive = exclusion.IsActive
            };
            return exclusionVM;
        }

        public async Task UpdateExclusionAsync(ExclusionViewModel exclusionVM)
        {
            Exclusion exclusion = await _unitOfWork.GetRepository<Exclusion>().GetAsync(x => x.IsActive == true && x.Id == exclusionVM.Id);
            exclusion.IsActive = exclusionVM.IsActive;
            exclusion.ExclusionString = exclusionVM.ExclusionString;
            await _unitOfWork.GetRepository<Exclusion>().UpdateAsync(exclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task SoftDeleteExclusionAsync(Guid id)
        {
            Exclusion exclusion = await _unitOfWork.GetRepository<Exclusion>().GetByGuidAsync(id);
            exclusion.IsActive = false;
            await _unitOfWork.GetRepository<Exclusion>().UpdateAsync(exclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteExclusionAsync(Guid id)
        {
            Exclusion exclusion = await _unitOfWork.GetRepository<Exclusion>().GetByGuidAsync(id);
            await _unitOfWork.GetRepository<Exclusion>().DeleteAsync(exclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverExclusionAsync(Guid id)
        {
            Exclusion exclusion = await _unitOfWork.GetRepository<Exclusion>().GetByGuidAsync(id);
            exclusion.IsActive = true;
            await _unitOfWork.GetRepository<Exclusion>().UpdateAsync(exclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ExclusionViewModel>> GetAllPassiveExclusions()
        {
            List<Exclusion> exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<ExclusionViewModel>>(exclusions);
            return map;
        }
    }
}
