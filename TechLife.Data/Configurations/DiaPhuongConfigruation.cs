using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class DiaPhuongConfigruation : IEntityTypeConfiguration<DiaPhuong>
    {
        public void Configure(EntityTypeBuilder<DiaPhuong> builder)
        {
            builder.ToTable("DiaPhuong");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TenDiaPhuong).IsRequired();
            builder.Property(x => x.ParentId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.MoTa);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
