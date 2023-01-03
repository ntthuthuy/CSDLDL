using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class GiayPhepConfigruation : IEntityTypeConfiguration<GiayPhep>
    {
        public void Configure(EntityTypeBuilder<GiayPhep> builder)
        {

            builder.ToTable("GiayPhep");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
