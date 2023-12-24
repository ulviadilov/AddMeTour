using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Exclusion
{
    public class ExclusionAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ExcluisonString { get; set; }
        public bool IsActive { get; set; }
    }
}
