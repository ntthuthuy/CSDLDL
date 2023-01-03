using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model;

namespace TechLife.Common
{
    public class RoleAssignRequest
    {
        public object Id { get; set; }
        public int GroupId { get; set; }
        public List<Guid> RoleId { get; set; }
        public List<RoleModel> UserRoles { get; set; } = new List<RoleModel>();
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> GroupRoles { get; set; } = new List<SelectListItem>();
    }
}
