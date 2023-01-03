using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
  
    public class HoSoConfigruation : IEntityTypeConfiguration<HoSo>
    {
        public void Configure(EntityTypeBuilder<HoSo> builder)
        {
            builder.ToTable("HoSo");

            builder.HasKey(x => x.Id);
          
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Ten).IsRequired();

            builder.Property(x => x.QuanHuyenId).IsRequired();
            builder.Property(x => x.TinhThanhId).IsRequired();
            builder.Property(x => x.LoaiHinhId).IsRequired();

            builder.Property(x => x.LinhVucKinhDoanhId).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);

        }
    }
}
