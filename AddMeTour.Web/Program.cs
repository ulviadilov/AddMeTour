using AddMeTour.Data.Context;
using AddMeTour.Service.Extensions;
using AddMeTour.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using AddMeTour.Entity.Entities.User;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using System.Reflection;


namespace AddMeTour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.LoadDataLayerExtension(builder.Configuration);
            builder.Services.LoadServiceLayerExtension();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddControllers().AddFluentValidation(opt =>
            {
                opt.ImplicitlyValidateChildProperties = true;
                opt.ImplicitlyValidateRootCollectionElements = true;
                opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            var app = builder.Build();

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseStatusCodePagesWithReExecute("/Error/NotFound", "?code={0}");
            app.UseAuthentication();
            app.UseAuthorization();

            
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}