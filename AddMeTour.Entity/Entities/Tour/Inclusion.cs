using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class Inclusion : EntityBase
    {
        public string? InclusionString { get; set; }
        public ICollection<TourInclusion>? TourInclusions { get; set; }
    }
}
