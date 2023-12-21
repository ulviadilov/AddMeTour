using AddMeTour.Entity.Entities.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Data.Mappings
{
    public class FeatureMap : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(250);
        }
    }
}
