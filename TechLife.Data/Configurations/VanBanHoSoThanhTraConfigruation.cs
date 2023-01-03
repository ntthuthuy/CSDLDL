using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class VanBanHoSoThanhTraConfigruation : IEntityTypeConfiguration<VanBanHoSoThanhTra>
    {
        public void Configure(EntityTypeBuilder<VanBanHoSoThanhTra> builder)
        {
            builder.ToTable("VanBanHoSoThanhTra");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.HoSoThanhTraId).IsRequired();
        }
    }
}
