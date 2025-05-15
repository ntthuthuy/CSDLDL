using Microsoft.AspNetCore.Identity;
using System;

namespace TechLife.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string AvataUrl { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsStatus { get; set; }
        public DateTime Dob { get; set; }
        public string CanCuocCongDan { get; set; }
    }
}
