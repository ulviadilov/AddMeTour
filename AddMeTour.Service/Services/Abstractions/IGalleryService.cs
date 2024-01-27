using AddMeTour.Entity.ViewModels.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface IGalleryService
    {
        Task<List<GalleryViewModel>> GetAllGallerysNonDeletedAsync();
        Task<GalleryViewModel> GetGalleryByGuidNonDeletedAsync(Guid id);
        Task CreateGalleryAsync(GalleryAddViewModel GalleryAddVM);
        GalleryUpdateViewModel UpdateGalleryById(Guid id);
        Task UpdateGalleryAsync(GalleryUpdateViewModel GalleryUpVM);
        Task SafeDeleteGalleryAsync(Guid GalleryId);
        Task HardDeleteAsync(Guid id);
        Task RecoverGalleryAsync(Guid id);
        Task<List<GalleryViewModel>> GetAllPassiveGallerys();
    }
}
