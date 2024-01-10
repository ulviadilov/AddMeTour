using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Service.Helpers.Pagination;

namespace AddMeTour.Web.ViewModels
{
    public class SearchByCountryViewModel
    {
        public PaginatedList<TourViewModel> Tours { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public CountryViewModel Country { get; set; }
        public List<CountryViewModel> Countries { get; set; }
    }
}
