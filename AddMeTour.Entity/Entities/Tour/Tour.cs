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
        public string DepartureDetails { get; set; }
        public DateTime CreateTime { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public ICollection<TourCountry>? TourCountries { get; set; }
        public ICollection<TourCategory>? TourCategories { get; set; }
    }
}
