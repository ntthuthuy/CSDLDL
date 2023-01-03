using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class HuongDanVienConfigruation : IEntityTypeConfiguration<HuongDanVien>
    {
        public void Configure(EntityTypeBuilder<HuongDanVien> builder)
        {
            builder.ToTable("HuongDanVien");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HoVaTen).IsRequired();
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
