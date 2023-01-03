using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechLife.Common.Enums;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class FileUploadConfigruation : IEntityTypeConfiguration<FileUpload>
    {
        public void Configure(EntityTypeBuilder<FileUpload> builder)
        {
            builder.ToTable("FileUploads");

            builder.HasKey(x => x.FileId);

            builder.Property(x => x.FileId).UseIdentityColumn();

            builder.Property(x => x.Id).IsRequired();


            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileUrl).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.IsStatus).HasDefaultValue(true);

        }
    }
}
