using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Setting
{
    public class SettingViewModel
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public string? Key { get; set; }
    }
}
