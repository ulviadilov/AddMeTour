using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.Entities.Home;

namespace AddMeTour.Service.Services.Concretes
{
    public class MastheadService : IMastheadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public MastheadService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        public async Task<MastheadViewModel> GetMastheadByGuidNonDeletedAsync(Guid id)
        {
            Masthead masthead = await _unitOfWork.GetRepository<Masthead>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<MastheadViewModel>(masthead);
            return map;
        }


        public async Task<List<MastheadViewModel>> GetAllMastheadsNonDeletedAsync()
        {
            var masthead = await _unitOfWork.GetRepository<Masthead>().GetAllAsync(x => x.IsActive);
            var map = _mapper.Map<List<MastheadViewModel>>(masthead);
            return map;
        }

        public async Task CreateMastheadAsync(MastheadAddViewModel mastheadAddVM)
        {
            Masthead masthead = new Masthead
            {
                TitleYellow = mastheadAddVM.TitleYellow,
                TitleWhite = mastheadAddVM.TitleWhite,
                Description = mastheadAddVM.Description,
                BigImageUrl = mastheadAddVM.BigImageFile.SaveFile(Path.Combine(_env.WebRootPath,"assets","img","masthead","2")),
                IsActive = mastheadAddVM.IsActive,
                Id = mastheadAddVM.Id,
                SmallImageUrl1 = mastheadAddVM.SmallImageFile1.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "masthead","2")),
                SmallImageUrl2 = mastheadAddVM.SmallImageFile2.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "masthead","2"))
            };
            await _unitOfWork.GetRepository<Masthead>().AddAsync(masthead);
            await _unitOfWork.SaveAsync();
        }

        public  MastheadUpdateViewModel UpdateMastheadById(Guid id)
        {
            var masthead = _unitOfWork.GetRepository<Masthead>().GetByGuidAsync(id);
            MastheadUpdateViewModel mastheadUpVM = new MastheadUpdateViewModel
            {
                Description = masthead.Result.Description,
                Id = masthead.Result.Id,
                TitleYellow = masthead.Result.TitleYellow,
                TitleWhite = masthead.Result.TitleWhite,
                
            };
            return mastheadUpVM;
        }

        public async Task UpdateMastheadAsync(MastheadUpdateViewModel mastheadUpVM)
        {
            Masthead masthead = await _unitOfWork.GetRepository<Masthead>().GetAsync(x => x.IsActive && x.Id == mastheadUpVM.Id);
            if (mastheadUpVM.BigImageFile is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "masthead","2", masthead.BigImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                masthead.BigImageUrl = mastheadUpVM.BigImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "masthead","2"));
            }
            if (mastheadUpVM.SmallImageFile1 is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "masthead", "2", masthead.SmallImageUrl1);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                masthead.SmallImageUrl1 = mastheadUpVM.SmallImageFile1.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "masthead", "2"));
            }
            if (mastheadUpVM.SmallImageFile2 is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "masthead", "2", masthead.SmallImageUrl2);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                masthead.SmallImageUrl2 = mastheadUpVM.SmallImageFile2.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "masthead", "2"));
            }
            masthead.TitleYellow = mastheadUpVM.TitleYellow;
            masthead.TitleWhite = mastheadUpVM.TitleWhite;
            masthead.Description = mastheadUpVM.Description;

            await _unitOfWork.GetRepository<Masthead>().UpdateAsync(masthead);
            await _unitOfWork.SaveAsync();
        }


    }
}
