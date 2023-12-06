using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities
{
    public class TourLanguage
    {
        public Guid TourId { get; set; }
        public Guid LanguageId { get; set; }
        public Tour? Tour { get; set; }
        public Language? Language { get; set; }
    }
}
