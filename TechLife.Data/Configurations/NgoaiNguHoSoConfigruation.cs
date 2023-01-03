using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class NgoaiNguHoSoConfigruation : IEntityTypeConfiguration<NgoaiNguHoSo>
    {
        public void Configure(EntityTypeBuilder<NgoaiNguHoSo> builder)
        {

            builder.ToTable("NgoaiNguHoSo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.NgoaiNgu).WithMany(x => x.DSNgoaiNguHoSo).HasForeignKey(x => x.NgoaiNguId);
        }
    }
}
