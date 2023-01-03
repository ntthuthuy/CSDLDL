
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class HanhTrinhChuyenDiConfigruation : IEntityTypeConfiguration<HanhTrinhChuyenDi>
    {
        public void Configure(EntityTypeBuilder<HanhTrinhChuyenDi> builder)
        {
            builder.ToTable("HanhTrinhChuyenDi");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

        }
    }
}
