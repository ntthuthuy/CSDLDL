using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class LoaiAmThucDiaDiemAnUongConfigruation : IEntityTypeConfiguration<LoaiAmThucDiaDiemAnUong>
    {
        public void Configure(EntityTypeBuilder<LoaiAmThucDiaDiemAnUong> builder)
        {
            builder.ToTable("DL_DiaDiemAnUong_LoaiAmThuc");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TenLoaiAmThuc).IsRequired();
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
