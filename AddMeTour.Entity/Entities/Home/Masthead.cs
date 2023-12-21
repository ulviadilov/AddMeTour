using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Home
{
    public class Masthead : EntityBase
    {
        public string TitleYellow { get; set; }
        public string TitleWhite { get; set; }
        public string Description { get; set; }
        public string? BigImageUrl { get; set; }
        public string? SmallImageUrl1 { get; set; }
        public string? SmallImageUrl2 { get; set; }
    }
}
