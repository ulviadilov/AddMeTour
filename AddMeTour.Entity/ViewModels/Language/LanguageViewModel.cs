using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tour.Languages
{
    public class LanguageViewModel
    {
        public Guid Id { get; set; }
        public string LanguageName { get; set; }
        public bool IsActive { get; set; }
    }
}
