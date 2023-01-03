using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;
namespace TechLife.Data.Configurations
{
    public class HuongDanVienLoaiHinhConfigruation : IEntityTypeConfiguration<HuongDanVienLoaiHinh>
    {
        public void Configure(EntityTypeBuilder<HuongDanVienLoaiHinh> builder)
        {
            builder.ToTable("HuongDanVienLoaiHinh");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.HuongDanVienId).IsRequired();
            builder.Property(x => x.LoaiHinhId).IsRequired();

        }
    }
}
