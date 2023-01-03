using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;
namespace TechLife.Data.Configurations
{
    public class ThietBiConfigruation : IEntityTypeConfiguration<ThietBi>
    {
        public void Configure(EntityTypeBuilder<ThietBi> builder)
        {
            builder.ToTable("ThietBi");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.MaThietBi).IsRequired();
            builder.Property(x => x.NgayCaiDat).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);

        }
    }
}