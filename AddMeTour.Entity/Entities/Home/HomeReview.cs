using AddMeTour.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.Home
{
    public class HomeReview : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string UserCountry { get; set; }
        public string AvatarUrl { get; set; }
    }
}
