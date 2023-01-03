using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDeleted { get; set; }
    }
}
