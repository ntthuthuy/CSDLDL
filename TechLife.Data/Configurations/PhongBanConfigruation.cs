using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class PhongBanConfigruation : IEntityTypeConfiguration<PhongBan>
    {
        public void Configure(EntityTypeBuilder<PhongBan> builder)
        {
            builder.ToTable("PhongBan");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Ten).IsRequired();
            builder.Property(x => x.MaDinhDanh).IsRequired();
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
