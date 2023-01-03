using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class TrinhDoHoSoConfigruation : IEntityTypeConfiguration<TrinhDoHoSo>
    {
        public void Configure(EntityTypeBuilder<TrinhDoHoSo> builder)
        {

            builder.ToTable("TrinhDoHoSo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.TrinhDo).WithMany(x => x.DSTrinhDoHoSo).HasForeignKey(x => x.TrinhDoId);

        }
    }
}
