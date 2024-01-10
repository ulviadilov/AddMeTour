using AddMeTour.Entity.Entities.User;
using AddMeTour.Service.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddMeTour.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Developer")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        UserName = "cavidgerayli",
        //        Fullname = "Cavid Gerayli"
        //    };

        //    var result = await _userManager.CreateAsync(admin, "oPF85Uvwtde7g7z");
        //    return Ok(result);
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    await _roleManager.CreateAsync(role1);

        //    return Ok("Created");
        //}

        //public async Task<IActionResult> AddRole()
        //{
        //    AppUser user = await _userManager.FindByNameAsync("cavidgerayli");

        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");
        //    return Ok("Role added");
        //}


    }


}
