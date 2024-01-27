using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.ViewModels.Gallery;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Gallery
{
    public class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            CreateMap<GalleryViewModel, GalleryImage>().ReverseMap();
            CreateMap<GalleryAddViewModel, GalleryImage>().ReverseMap();
            CreateMap<GalleryUpdateViewModel, GalleryImage>().ReverseMap();
        }
    }
}
