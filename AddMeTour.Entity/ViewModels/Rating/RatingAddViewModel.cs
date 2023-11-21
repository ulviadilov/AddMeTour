using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Rating
{
    public class RatingAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActice { get; set; } = true;
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description { get; set; }
        public decimal OverallRating { get; set; }
        public int TotalGuestCount { get; set; }
    }
}
