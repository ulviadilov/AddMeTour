using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Home
{
    public class SearchByCategoryViewModel
    {
        public List<TourViewModel> Tours { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CountryViewModel> Countries { get; set; }
    }
}
