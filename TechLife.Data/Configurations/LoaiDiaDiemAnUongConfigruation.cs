using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{

    public class LoaiDiaDiemAnUongConfigruation : IEntityTypeConfiguration<LoaiDiaDiemAnUong>
    {
        public void Configure(EntityTypeBuilder<LoaiDiaDiemAnUong> builder)
        {
            builder.ToTable("LoaiDiaDiemAnUong");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TenLoai).IsRequired();
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
           
        }
    }
}
