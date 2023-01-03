using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{

    public class HoSoThanhTraConfigruation : IEntityTypeConfiguration<HoSoThanhTra>
    {
        public void Configure(EntityTypeBuilder<HoSoThanhTra> builder)
        {
            builder.ToTable("HoSoThanhTra");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HoSoId).IsRequired();
            builder.Property(x => x.ThoiGian).IsRequired();

            builder.Property(x => x.NgayTao).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
