using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Tour.Country
{
    public record  CountryAddViewModel(
        Guid Id ,
        string CountryName, 
        bool IsActive);
}
