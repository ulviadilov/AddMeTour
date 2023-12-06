﻿using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities
{
    public class TourImage : EntityBase
    {
        public Guid TourId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPoster { get; set; }
        public Tour Tour { get; set; }
    }
}
