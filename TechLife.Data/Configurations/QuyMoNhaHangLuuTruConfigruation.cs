using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class QuyMoNhaHangLuuTruConfigruation : IEntityTypeConfiguration<QuyMoNhaHangLuuTru>
    {
        public void Configure(EntityTypeBuilder<QuyMoNhaHangLuuTru> builder)
        {
            builder.ToTable("QuyMoNhaHangLuuTru");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.DienTich).HasDefaultValue(0);
            builder.Property(x => x.SoGhe).HasDefaultValue(0);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
