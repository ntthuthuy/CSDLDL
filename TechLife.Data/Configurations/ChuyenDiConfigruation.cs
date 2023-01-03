using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;
namespace TechLife.Data.Configurations
{

     public class ChuyenDiConfigruation : IEntityTypeConfiguration<ChuyenDi>
    {
        public void Configure(EntityTypeBuilder<ChuyenDi> builder)
        {
            builder.ToTable("ChuyenDi");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.MaThietBi).IsRequired();
            
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}