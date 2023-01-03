using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
   
    public class NgonNguConfigruation : IEntityTypeConfiguration<NgonNgu>
    {
        public void Configure(EntityTypeBuilder<NgonNgu> builder)
        {
            builder.ToTable("NgonNgu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Ten).IsRequired();
        }
    }
}
