using AddMeTour.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Data.Mappings
{
    public class RatingMap : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder) 
        {
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Title1).HasMaxLength(100);
            builder.Property(x => x.Title2).HasMaxLength(100);
        }
    }
}
