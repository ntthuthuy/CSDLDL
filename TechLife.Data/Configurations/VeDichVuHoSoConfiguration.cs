using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{

    public class VeDichVuHoSoConfiguration : IEntityTypeConfiguration<VeDichVuHoSo>
    {
        public void Configure(EntityTypeBuilder<VeDichVuHoSo> builder)
        {
            builder.ToTable("VeDichVuHoSo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
