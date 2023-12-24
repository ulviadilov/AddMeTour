using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class InclusionService : IInclusionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InclusionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<InclusionViewModel>> GetAllInclusionsNonDeletedAsync()
        {
            var inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<InclusionViewModel>>(inclusions);
            return map;
        }

        public async Task<InclusionViewModel> GetInclusionByGuidAsync(Guid id)
        {
            var inclusion = await _unitOfWork.GetRepository<Inclusion>().GetByGuidAsync(id);
            var map = _mapper.Map<InclusionViewModel>(inclusion);
            return map;
        }

        public async Task CreateInclusionAsync(InclusionAddViewModel inclusionAddVM)
        {
            Inclusion inclusion = new Inclusion
            {
                InclusionString = inclusionAddVM.InclusionString,
                Id = inclusionAddVM.Id,
                IsActive = inclusionAddVM.IsActive
            };
            await _unitOfWork.GetRepository<Inclusion>().AddAsync(inclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task<InclusionUpdateViewModel> UpdateInclusionByGuidAsync(Guid id)
        {
            var inclusion = await _unitOfWork.GetRepository<Inclusion>().GetByGuidAsync(id);
            InclusionUpdateViewModel inclusionUpdateVM = new InclusionUpdateViewModel
            {
                InclusionString = inclusion.InclusionString,
                Id = inclusion.Id,
                IsActive = inclusion.IsActive
            };
            return inclusionUpdateVM;
        }

        public async Task UpdateInclusionAsync(InclusionUpdateViewModel inclusionUpdateVM)
        {
            Inclusion inclusion = await _unitOfWork.GetRepository<Inclusion>().GetAsync(x => x.IsActive == true && x.Id == inclusionUpdateVM.Id);
            inclusion.IsActive = inclusionUpdateVM.IsActive;
            inclusion.InclusionString = inclusionUpdateVM.InclusionString;
            await _unitOfWork.GetRepository<Inclusion>().UpdateAsync(inclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task SoftDeleteInclusionAsync(Guid id)
        {
            Inclusion inclusion = await _unitOfWork.GetRepository<Inclusion>().GetByGuidAsync(id);
            inclusion.IsActive = false;
            await _unitOfWork.GetRepository<Inclusion>().UpdateAsync(inclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteInclusionAsync(Guid id)
        {
            Inclusion inclusion = await _unitOfWork.GetRepository<Inclusion>().GetByGuidAsync(id);
            await _unitOfWork.GetRepository<Inclusion>().DeleteAsync(inclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverInclusionAsync(Guid id)
        {
            Inclusion inclusion = await _unitOfWork.GetRepository<Inclusion>().GetByGuidAsync(id);
            inclusion.IsActive = true;
            await _unitOfWork.GetRepository<Inclusion>().UpdateAsync(inclusion);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<InclusionViewModel>> GetAllPassiveInclusions()
        {
            List<Inclusion> inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<InclusionViewModel>>(inclusions);
            return map;
        }
    }
}
