using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Gallery;
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
    public class GalleryService : IGalleryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public GalleryService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<GalleryViewModel>> GetAllGallerysNonDeletedAsync()
        {
            var Gallerys = await _unitOfWork.GetRepository<GalleryImage>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<GalleryViewModel>>(Gallerys);

            return map;
        }

        public async Task<GalleryViewModel> GetGalleryByGuidNonDeletedAsync(Guid id)
        {
            GalleryImage Gallery = await _unitOfWork.GetRepository<GalleryImage>().GetAsync(x => x.Id == id && x.IsActive == true);
            var map = _mapper.Map<GalleryViewModel>(Gallery);
            return map;
        }

        public async Task CreateGalleryAsync(GalleryAddViewModel GalleryAddVM)
        {
            GalleryImage Gallery = new GalleryImage
            {
                Id = GalleryAddVM.Id,
                ImageUrl = GalleryAddVM.ImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "gallery"))
            };
            await _unitOfWork.GetRepository<GalleryImage>().AddAsync(Gallery);
            await _unitOfWork.SaveAsync();
        }

        public GalleryUpdateViewModel UpdateGalleryById(Guid id)
        {
            var Gallery = _unitOfWork.GetRepository<GalleryImage>().GetByGuidAsync(id);
            GalleryUpdateViewModel GalleryUpdateVM = new GalleryUpdateViewModel
            {
                Id = id
            };
            return GalleryUpdateVM;
        }

        public async Task UpdateGalleryAsync(GalleryUpdateViewModel GalleryUpVM)
        {
            GalleryImage Gallery = await _unitOfWork.GetRepository<GalleryImage>().GetAsync(x => x.IsActive && x.Id == GalleryUpVM.Id);
            if (GalleryUpVM.ImageFile is not null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "gallery", Gallery.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                Gallery.ImageUrl = GalleryUpVM.ImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "gallery"));
            }

            await _unitOfWork.GetRepository<GalleryImage>().UpdateAsync(Gallery);
            await _unitOfWork.SaveAsync();
        }

        public async Task SafeDeleteGalleryAsync(Guid GalleryId)
        {
            GalleryImage Gallery = await _unitOfWork.GetRepository<GalleryImage>().GetByGuidAsync(GalleryId);
            Gallery.IsActive = false;
            await _unitOfWork.GetRepository<GalleryImage>().UpdateAsync(Gallery);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            GalleryImage Gallery = await _unitOfWork.GetRepository<GalleryImage>().GetByGuidAsync(id);
            string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "gallery", Gallery.ImageUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            await _unitOfWork.GetRepository<GalleryImage>().DeleteAsync(Gallery);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverGalleryAsync(Guid id)
        {
            GalleryImage Gallery = await _unitOfWork.GetRepository<GalleryImage>().GetByGuidAsync(id);
            Gallery.IsActive = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<GalleryViewModel>> GetAllPassiveGallerys()
        {
            var Gallerys = await _unitOfWork.GetRepository<GalleryImage>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<GalleryViewModel>>(Gallerys);
            return map;
        }
    }
}
