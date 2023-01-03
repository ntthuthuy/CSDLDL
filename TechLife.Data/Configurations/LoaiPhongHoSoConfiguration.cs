using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class LoaiPhongHoSoConfiguration : IEntityTypeConfiguration<LoaiPhongHoSo>
    {
        public void Configure(EntityTypeBuilder<LoaiPhongHoSo> builder)
        {
            builder.ToTable("LoaiPhongHoSo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.LoaiGiuong).WithMany(x => x.DSLoaiPhongHoSo).HasForeignKey(x => x.LoaiGiuongId);
            builder.HasOne(x => x.LoaiPhong).WithMany(x => x.DSLoaiPhongHoSo).HasForeignKey(x => x.LoaiPhongId);

        }
    }
}
