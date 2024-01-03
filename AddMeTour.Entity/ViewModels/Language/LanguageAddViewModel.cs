using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.AutoMapper.Tour.Languages
{
    public class LanguageAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LanguageName { get; set; }
        public bool IsActive { get; set; }
    }
}
