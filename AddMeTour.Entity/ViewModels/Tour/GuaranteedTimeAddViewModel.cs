using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour
{
    public class GuaranteedTimeAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TourId { get; set; }
        public DateTime? TourStartTime { get; set; }
        public DateTime? TourEndTime { get; set; }
        public byte MaxPeopleCount { get; set; }
        public byte ReservedPeopleCount { get; set; }
        public double Price { get; set; }
    }
}
