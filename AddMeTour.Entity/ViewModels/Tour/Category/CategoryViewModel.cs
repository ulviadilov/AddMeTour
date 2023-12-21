using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
    }
}
