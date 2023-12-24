using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Exclusion
{
    public class ExclusionViewModel
    {
        public Guid Id { get; set; }
        public string ExclusionString { get; set; }
        public bool IsActive { get; set; }
    }
}
