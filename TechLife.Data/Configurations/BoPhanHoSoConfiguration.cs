using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class BoPhanHoSoConfiguration : IEntityTypeConfiguration<BoPhanHoSo>
    {
        public void Configure(EntityTypeBuilder<BoPhanHoSo> builder)
        {
            builder.ToTable("BoPhanHoSo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.HasOne(x => x.BoPhan).WithMany(x => x.DSBoPhanHoSo).HasForeignKey(x => x.BoPhanId);
        }
    }
}
