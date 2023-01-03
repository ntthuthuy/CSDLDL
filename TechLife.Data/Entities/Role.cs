using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public List<RoleGroup> RoleGroups { get; set; }
    }
}
