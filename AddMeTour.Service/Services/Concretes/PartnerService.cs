using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Partners;
using AddMeTour.Entity.ViewModels.Partners;
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
    public class PartnerService :  IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public PartnerService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<PartnerViewModel>> GetAllPartnersNonDeletedAsync()
        {
            var Partners = await _unitOfWork.GetRepository<Partner>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<PartnerViewModel>>(Partners);

            return map;
        }

        public async Task<PartnerViewModel> GetPartnerByGuidNonDeletedAsync(Guid id)
        {
            Partner Partner = await _unitOfWork.GetRepository<Partner>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<PartnerViewModel>(Partner);
            return map;
        }

        public async Task CreatePartnerAsync(PartnerAddViewModel PartnerAddVM)
        {
            Partner Partner = new Partner
            {
                Id = PartnerAddVM.Id,
                ImageUrl = PartnerAddVM.ImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "Partners"))
            };
            await _unitOfWork.GetRepository<Partner>().AddAsync(Partner);
            await _unitOfWork.SaveAsync();
        }

        public PartnerUpdateViewModel UpdatePartnerById(Guid id)
        {
            var Partner = _unitOfWork.GetRepository<Partner>().GetByGuidAsync(id);
            PartnerUpdateViewModel PartnerUpdateVM = new PartnerUpdateViewModel
            {
                Id = id
            };
            return PartnerUpdateVM;
        }

        public async Task UpdatePartnerAsync(PartnerUpdateViewModel PartnerUpVM)
        {
            Partner Partner = await _unitOfWork.GetRepository<Partner>().GetAsync(x => x.IsActive && x.Id == PartnerUpVM.Id);
            if (PartnerUpVM.ImageFile is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "Partners", Partner.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                Partner.ImageUrl = PartnerUpVM.ImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "Partners"));
            }

            await _unitOfWork.GetRepository<Partner>().UpdateAsync(Partner);
            await _unitOfWork.SaveAsync();
        }

        public async Task SafeDeletePartnerAsync(Guid PartnerId)
        {
            Partner Partner = await _unitOfWork.GetRepository<Partner>().GetByGuidAsync(PartnerId);
            Partner.IsActive = false;
            await _unitOfWork.GetRepository<Partner>().UpdateAsync(Partner);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            Partner Partner = await _unitOfWork.GetRepository<Partner>().GetByGuidAsync(id);
            string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "Partners", Partner.ImageUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            await _unitOfWork.GetRepository<Partner>().DeleteAsync(Partner);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverPartnerAsync(Guid id)
        {
            Partner Partner = await _unitOfWork.GetRepository<Partner>().GetByGuidAsync(id);
            Partner.IsActive = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<PartnerViewModel>> GetAllPassivePartners()
        {
            var Partners = await _unitOfWork.GetRepository<Partner>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<PartnerViewModel>>(Partners);
            return map;
        }

    }
}
