using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class GuaranteedTime : EntityBase
    {
        public Guid TourId { get; set; }
        public DateTime? TourStartTime { get; set; }
        public DateTime? TourEndTime { get; set; }
        public byte MaxPeopleCount { get; set; }
        public byte ReservedPeopleCount { get; set; }
        public double Price { get; set; }
        public Tour? Tour { get; set; }
    }
}
