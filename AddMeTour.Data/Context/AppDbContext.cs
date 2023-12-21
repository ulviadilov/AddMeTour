using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.Entities.Tour;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Masthead> Mastheads { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<HomeReview> HomeReviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Exclusion> Exclusions { get; set; }
        public DbSet<Inclusion> Inclusions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourCountry> TourCountries { get; set; }
        public DbSet<TourExclusion> TourExclusions { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<TourInclusion> TourInclusions { get; set; }
        public DbSet<TourLanguage> TourLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
