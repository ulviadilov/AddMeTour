using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Features
{
    public class FeatureAddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile imageFile { get; set; }
    }
}
