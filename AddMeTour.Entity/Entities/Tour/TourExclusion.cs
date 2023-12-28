using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class TourExclusion : EntityBase
    {
        public Guid TourId { get; set; }
        public Guid ExclusionId { get; set; }
        public Tour? Tour { get; set; }
        public Exclusion? Exclusion { get; set; }
    }
}
