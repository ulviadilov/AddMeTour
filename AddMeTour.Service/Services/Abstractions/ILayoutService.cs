using AddMeTour.Entity.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ILayoutService
    {
        Task<AppUser> GetUser();
    }
}
