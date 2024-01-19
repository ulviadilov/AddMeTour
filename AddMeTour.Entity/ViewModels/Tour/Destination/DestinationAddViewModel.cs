using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Destination
{
    public class DestinationAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
