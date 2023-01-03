using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLife.Data.Entities;
using System;
namespace TechLife.Data.Configurations
{
    public class OrderConfigruation : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.MaThietBi).IsRequired();
            builder.Property(x => x.DichVuId).IsRequired();
            builder.Property(x => x.LoaiDinhVu).IsRequired();
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.Property(x => x.NgayTao).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);

        }
    }
}