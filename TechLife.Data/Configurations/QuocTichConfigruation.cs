using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class QuocTichConfigruation : IEntityTypeConfiguration<QuocTich>
    {
        public void Configure(EntityTypeBuilder<QuocTich> builder)
        {
            builder.ToTable("QuocTich");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TenQuocTich).IsRequired();
            builder.Property(x => x.MoTa);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
