using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Data.Entities;

namespace TechLife.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleId,
                Name = "root",
                NormalizedName = "root",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "root",
                NormalizedUserName = "root",
                Email = "nhonlq.hue@gmail.com",
                NormalizedEmail = "nhonlq.hue@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc@123"),
                SecurityStamp = string.Empty,
                FirstName = "Quản trị",
                LastName = "hệ thống",
                FullName = "Quản trị hệ thống",
                Dob = new DateTime(1994, 09, 22)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<NgonNgu>().HasData(
               new NgonNgu() { Id = "vi", Ten = "Tiếng Việt", IsDefault = true },
               new NgonNgu() { Id = "en", Ten = "English", IsDefault = false },
               new NgonNgu() { Id = "ja", Ten = "Japan", IsDefault = false });

        }
    }
}
