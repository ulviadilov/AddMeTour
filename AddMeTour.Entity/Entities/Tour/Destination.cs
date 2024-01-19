using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public  class Destination : EntityBase
    {
        public Guid TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Tour? Tour { get; set; }
    }
}
