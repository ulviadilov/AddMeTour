using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Tour
{
    public class Language : EntityBase
    {
        public string LanguageName { get; set; }
        public ICollection<TourLanguage>? TourLanguages { get; set; }
    }
}
