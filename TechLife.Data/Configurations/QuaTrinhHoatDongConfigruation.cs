using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class QuaTrinhHoatDongConfigruation : IEntityTypeConfiguration<QuaTrinhHoatDong>
    {
        public void Configure(EntityTypeBuilder<QuaTrinhHoatDong> builder)
        {
            builder.ToTable("QuaTrinhHoatDong");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HDVId).IsRequired();

            builder.HasOne(x => x.HuongDanVien).WithMany(x => x.DSQuaTrinhHoatDong).HasForeignKey(x => x.HDVId);
        }
    }
}
