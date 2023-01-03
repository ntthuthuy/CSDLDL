using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
   
    public class BoPhanLinhVucConfiguration : IEntityTypeConfiguration<BoPhanLinhVuc>
    {
        public void Configure(EntityTypeBuilder<BoPhanLinhVuc> builder)
        {
            builder.ToTable("BoPhanLinhVuc");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
