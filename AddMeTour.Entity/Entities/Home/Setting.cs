using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Home
{
    public class Setting : EntityBase
    {
        public string? Value { get; set; }
        public string? Key { get; set; }
    }
}
