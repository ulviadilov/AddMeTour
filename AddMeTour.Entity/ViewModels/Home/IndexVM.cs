using AddMeTour.Entity.Entities;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.ViewModels.HomeReviews;
using AddMeTour.Entity.ViewModels.Masthead;
using AddMeTour.Entity.ViewModels.Rating;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Home
{
    public class IndexVM
    {
        public List<FeatureViewModel> Features { get; set; }
        public MastheadViewModel Masthead { get; set; }
        public RatingViewModel Rating { get; set; }
        public List<HomeReviewViewModel> homeReviews { get; set; }
        public List<TourViewModel> Tours { get; set; }
        public List<Country> Countries { get; set; }
    }
}
