using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class MucDoThongThaoNgoaiNguConfiguration : IEntityTypeConfiguration<MucDoThongThaoNgoaiNgu>
    {
        public void Configure(EntityTypeBuilder<MucDoThongThaoNgoaiNgu> builder)
        {
            builder.ToTable("MucDoThongThaoNgoaiNgu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.MucDo).IsRequired();
            builder.Property(x => x.MoTa);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
