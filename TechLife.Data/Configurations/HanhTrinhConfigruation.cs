using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class HanhTrinhConfigruation : IEntityTypeConfiguration<HanhTrinh>
    {
        public void Configure(EntityTypeBuilder<HanhTrinh> builder)
        {
            builder.ToTable("HanhTrinh");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TourId).IsRequired();

            builder.Property(x => x.Ngay).IsRequired();
            builder.Property(x => x.Gio).IsRequired();
            builder.Property(x => x.Phut).IsRequired();
            builder.Property(x => x.NoiDenId).IsRequired();
            builder.Property(x => x.ThoiGian).IsRequired();
            builder.Property(x => x.IsStatus).HasDefaultValue(true);

            builder.HasOne(x => x.Tour).WithMany(x => x.DSHanhTrinh).HasForeignKey(x => x.TourId);
        }
    }
}
