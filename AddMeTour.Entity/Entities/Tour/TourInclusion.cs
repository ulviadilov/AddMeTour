using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class TourInclusion: EntityBase
    {
        public Guid TourId { get; set; }
        public Guid InclusionId { get; set; }
        public Tour? Tour { get; set; }
        public Inclusion? Inclusion { get; set; }
    }
}
