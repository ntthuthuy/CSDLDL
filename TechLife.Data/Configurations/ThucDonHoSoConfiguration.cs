using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{

    public class ThucDonHoSoConfiguration : IEntityTypeConfiguration<ThucDonHoSo>
    {
        public void Configure(EntityTypeBuilder<ThucDonHoSo> builder)
        {
            builder.ToTable("ThucDonHoSo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
