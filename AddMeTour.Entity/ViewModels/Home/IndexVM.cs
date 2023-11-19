using AddMeTour.Entity.Entities;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.ViewModels.Masthead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Home
{
    public class IndexVM
    {
        public List<FeatureViewModel> Features { get; set; }
        public MastheadViewModel Masthead { get; set; }
    }
}
