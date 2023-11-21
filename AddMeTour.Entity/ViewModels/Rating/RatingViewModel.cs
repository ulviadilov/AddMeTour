using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Rating
{
    public class RatingViewModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public decimal OverallRating { get; set; }
        public int TotalGuestCount { get; set; }
    }
}
