using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class Tour : EntityBase
    {
        public string TourName { get; set; }
        public string Overview { get; set; }
        public decimal Price { get; set; }
        public byte GroupSize { get; set; }
        public byte Duration { get; set; }
        public double Rating { get; set; }
        public int Order { get; set; }
        public bool IsGuaranteed { get; set; }
        public bool IsBest { get; set; }
        public string? PosterImageUrl { get; set; }
        public DateTime CreateTime { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public ICollection<TourCountry>? TourCountries { get; set; }
        public ICollection<TourCategory>? TourCategories { get; set; }
        public ICollection<TourLanguage>? TourLanguages { get; set; }
        public ICollection<TourInclusion>? TourInclusions { get; set; }
        public ICollection<TourExclusion>? TourExclusions { get; set; }
        public ICollection<Destination>? Destinations { get; set; }
        public ICollection<GuaranteedTime>? GuaranteedTimes { get; set; }
    }
}
