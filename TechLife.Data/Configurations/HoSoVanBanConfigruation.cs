using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class HoSoVanBanConfigruation : IEntityTypeConfiguration<HoSoVanBan>
    {
        public void Configure(EntityTypeBuilder<HoSoVanBan> builder)
        {
            builder.ToTable("HoSoVanBan");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.HosoId).IsRequired();
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
