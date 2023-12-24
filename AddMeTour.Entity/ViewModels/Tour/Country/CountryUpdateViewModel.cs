using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Country
{
    public class CountryUpdateViewModel
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }

}
