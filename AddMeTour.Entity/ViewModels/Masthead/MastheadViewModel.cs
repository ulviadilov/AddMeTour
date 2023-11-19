using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Masthead
{
    public class MastheadViewModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string TitleYellow { get; set; }
        public string TitleWhite { get; set; }
        public string Description { get; set; }
        public string BigImageUrl { get; set; }
        public string SmallImageUrl1 { get; set; }
        public string SmallImageUrl2 { get; set; }
    }
}
