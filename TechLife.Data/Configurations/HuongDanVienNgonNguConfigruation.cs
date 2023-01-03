using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class HuongDanVienNgonNguConfigruation : IEntityTypeConfiguration<HuongDanVienNgonNgu>
    {
        public void Configure(EntityTypeBuilder<HuongDanVienNgonNgu> builder)
        {
            builder.ToTable("HuongDanVienNgonNgu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HuongDanVienId).IsRequired();
            builder.Property(x => x.NgonNguId).IsRequired();

        }
    }
}
