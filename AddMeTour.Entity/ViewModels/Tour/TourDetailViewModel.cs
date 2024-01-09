using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Entity.ViewModels.Tour.Exclusion;
using AddMeTour.Entity.ViewModels.Tour.Inclusion;
using AddMeTour.Service.AutoMapper.Tour.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour
{
    public class TourDetailViewModel
    {
        public TourViewModel Tour { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
        public List<TourViewModel> Tours { get; set; }
        public List<InclusionViewModel> Inclusions { get; set; }
        public List<ExclusionViewModel> Exclusions { get; set; }
    }
}
