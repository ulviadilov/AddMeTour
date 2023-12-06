using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities
{
    public class TourCategory 
    {
        public Guid Id { get; set; }
        public Guid? TourId { get; set; }
        public Guid? CategoryId { get; set; }
        public Tour? Tour { get; set; }
        public Category? Category { get; set; }
    }
}
