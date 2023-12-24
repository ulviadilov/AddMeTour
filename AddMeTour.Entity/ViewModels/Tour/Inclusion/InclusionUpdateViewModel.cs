using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Inclusion
{
    public class InclusionUpdateViewModel
    {
        public Guid Id { get; set; }
        public string InclusionString { get; set; }
        public bool IsActive { get; set; }
    }
}
