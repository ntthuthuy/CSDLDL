using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
 
    public class TinhChatLaoDongConfiguartion : IEntityTypeConfiguration<TinhChatLaoDong>
    {
        public void Configure(EntityTypeBuilder<TinhChatLaoDong> builder)
        {
            builder.ToTable("TinhChatLaoDong");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Ten).IsRequired();
            builder.Property(x => x.MoTa);
            builder.Property(x => x.IsStatus).HasDefaultValue(true);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
        }
    }
}
