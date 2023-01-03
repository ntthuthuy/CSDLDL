using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class TourConfigruation : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tours");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CongTyLuHanhId).IsRequired();
            builder.Property(x => x.TenChuyenDi).IsRequired(); 
            builder.Property(x => x.Gia).IsRequired(); 
            builder.Property(x => x.SoNgay).IsRequired(); 
            builder.Property(x => x.LoaiId).IsRequired(); 
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
