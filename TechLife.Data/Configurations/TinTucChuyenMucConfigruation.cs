using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class TinTucChuyenMucConfigruation : IEntityTypeConfiguration<TinTucChuyenMuc>
    {
        public void Configure(EntityTypeBuilder<TinTucChuyenMuc> builder)
        {
            builder.ToTable("TinTucChuyenMuc");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TinTucId).IsRequired();
            builder.Property(x => x.ChuyenMucId).IsRequired();
       
        }
    }
}
