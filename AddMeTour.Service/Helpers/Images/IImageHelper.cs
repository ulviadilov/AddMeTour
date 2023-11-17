using AddMeTour.Entity.Enums;
using AddMeTour.Entity.ViewModels.Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadViewModel> Upload(string name, IFormFile imageFile,ImageType imageType, string folderName = null);
        void Delete(string name);
    }
}
