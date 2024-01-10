using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities.User
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
