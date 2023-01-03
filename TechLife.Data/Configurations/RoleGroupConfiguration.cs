using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup>
    {
        public void Configure(EntityTypeBuilder<RoleGroup> builder)
        {
          

            builder.HasKey(t => new { t.GroupId, t.RoleId });

            builder.ToTable("RoleGroups");

            builder.HasOne(t => t.Role).WithMany(pc => pc.RoleGroups)
                .HasForeignKey(pc => pc.RoleId);

        }
    }
}
