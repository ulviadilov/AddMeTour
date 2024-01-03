using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour
{
    public class TourUpdateViewModel
    {
        public Guid TourId { get; set; }
        public bool IsActive { get; set; }
        public string TourName { get; set; }
        public string Overview { get; set; }
        public decimal Price { get; set; }
        public byte GroupSize { get; set; }
        public byte Duration { get; set; }
        public string DepartureDetails { get; set; }
        public List<Guid>? ImageIds { get; set; }
        [NotMapped]
        public List<IFormFile>? ImageFiles { get; set; }
        [NotMapped]
        public IFormFile? PosterImageFile { get; set; }
        public List<Guid>? LanguageIds { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public List<Guid>? CountryIds { get; set; }
        public List<Guid>? InclusionIds { get; set; }
        public List<Guid>? ExclusionIds { get; set; }
    }
}
