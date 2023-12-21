using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Category
{
    public class CategoryAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public string CategoryName { get; set; }
    }
}
