using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{

    public class TrackingConfiguration : IEntityTypeConfiguration<Tracking>
    {
        public void Configure(EntityTypeBuilder<Tracking> builder)
        {
            builder.ToTable("Trackings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Action).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Time).HasDefaultValue(DateTime.Now);
        }
    }
}
