using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class TourCategory : EntityBase
    {
        public Guid? TourId { get; set; }
        public Guid CategoryId { get; set; }
        public Tour? Tour { get; set; }
        public Category? Category { get; set; }
    }
}
