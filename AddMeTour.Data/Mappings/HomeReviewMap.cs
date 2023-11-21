using AddMeTour.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Data.Mappings
{
    public class HomeReviewMap : IEntityTypeConfiguration<HomeReview>
    {
        public void Configure(EntityTypeBuilder<HomeReview> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(75);
            builder.Property(x => x.Username).HasMaxLength(50);
            builder.Property(x => x.UserCountry).HasMaxLength(50);
        }
    }
}
