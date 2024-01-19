using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Destination
{
    public class DestinationViewModel
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
