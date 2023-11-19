using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Masthead
{
    public class MastheadUpdateViewModel
    {
        public Guid Id { get; set; }
        public string TitleYellow { get; set; }
        public string TitleWhite { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile? BigImageFile { get; set; }
        [NotMapped]
        public IFormFile? SmallImageFile1 { get; set; }
        [NotMapped]
        public IFormFile? SmallImageFile2 { get; set; }
    }
}
