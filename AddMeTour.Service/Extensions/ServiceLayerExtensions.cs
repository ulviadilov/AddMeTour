using AddMeTour.Data.Context;
using AddMeTour.Data.Repositories.Abstractions;
using AddMeTour.Data.Repositories.Concretes;
using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Data.UnitOfWorks.Concretes;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstraction;
using AddMeTour.Service.Services.Abstractions;
using AddMeTour.Service.Services.Concrete;
using AddMeTour.Service.Services.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IMastheadService, MastheadService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IHomeReviewService, HomeReviewService>();
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
