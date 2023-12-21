using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{ 
    public class Country : EntityBase
    {
        public string CountryName { get; set; }
        public ICollection<TourCountry>? TourCountries { get; set; }
    }
}
