using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Inclusion
{
    public class InclusionAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string InclusionString { get; set; }
        public bool IsActive { get; set; }
    }
}
