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
    public class MastheadMap : IEntityTypeConfiguration<Masthead>
    {
        public void Configure(EntityTypeBuilder<Masthead> builder)
        {
            builder.Property(x => x.TitleYellow).HasMaxLength(100);
            builder.Property(x => x.TitleWhite).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(150);
        }
    }
}
