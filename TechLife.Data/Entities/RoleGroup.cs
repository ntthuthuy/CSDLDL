using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class RoleGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
