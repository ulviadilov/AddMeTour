using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Entity.ViewModels.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Home
{
    public class SearchByCountryViewModel
    {
        public List<TourViewModel> Tours { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public CountryViewModel Country { get; set; }
        public List<CountryViewModel> Countries { get; set; }
    }
}
