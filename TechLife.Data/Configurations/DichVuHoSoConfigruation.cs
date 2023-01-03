using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class DichVuHoSoConfigruation : IEntityTypeConfiguration<DichVuHoSo>
    {
        public void Configure(EntityTypeBuilder<DichVuHoSo> builder)
        {

            builder.ToTable("DichVuHoSo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.DichVu).WithMany(x => x.DSDichVuHoSo).HasForeignKey(x => x.DichVuId);


        }
    }
}
