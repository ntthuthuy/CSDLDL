using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class TinTucConfigruation : IEntityTypeConfiguration<TinTuc>
    {
        public void Configure(EntityTypeBuilder<TinTuc> builder)
        {
            builder.ToTable("TinTuc");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TieuDe).IsRequired();
            builder.Property(x => x.MoTa).IsRequired();
            builder.Property(x => x.NoiDung).IsRequired();
            builder.Property(x => x.NgonNguId).IsRequired();
            builder.Property(x => x.AnhDaiDien).IsRequired();

            builder.Property(x => x.LuotXem).HasDefaultValue(0);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
