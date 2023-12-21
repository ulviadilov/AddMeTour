using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class Category : EntityBase
    {
        public string CategoryName { get; set; }
        public ICollection<TourCategory>? TourCategories { get; set; }
    }
}
