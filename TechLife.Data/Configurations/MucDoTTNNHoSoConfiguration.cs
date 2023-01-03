using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class MucDoTTNNHoSoConfiguration : IEntityTypeConfiguration<MucDoTTNNHoSo>
    {
        public void Configure(EntityTypeBuilder<MucDoTTNNHoSo> builder)
        {
            builder.ToTable("MucDoTTNNHoSo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.MucDoTTNN).WithMany(x => x.DSMucDoTTNNHoSo).HasForeignKey(x => x.MucDoId);

        }
    }
}
