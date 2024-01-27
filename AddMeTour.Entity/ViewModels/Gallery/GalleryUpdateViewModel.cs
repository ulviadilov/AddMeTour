﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Gallery
{
    public class GalleryUpdateViewModel
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
