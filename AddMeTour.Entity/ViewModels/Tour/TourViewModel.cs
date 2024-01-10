using AddMeTour.Entity.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour
{
    public class TourViewModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string TourName { get; set; }
        public string Overview { get; set; }
        public decimal Price { get; set; }
        public byte GroupSize { get; set; }
        public byte Duration { get; set; }
        public double Rating { get; set; }
        public string DepartureDetails { get; set; }
        public string PosterImageUrl { get; set; }
        public DateTime CreateTime { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public ICollection<TourCountry>? TourCountries { get; set; }
        public ICollection<TourCategory>? TourCategories { get; set; }
        public ICollection<TourLanguage>? TourLanguages { get; set; }
        public ICollection<TourInclusion>? TourInclusions { get; set; }
        public ICollection<TourExclusion>? TourExclusions { get; set; }
    }
}
