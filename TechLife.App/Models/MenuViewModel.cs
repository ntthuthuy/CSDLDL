using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Model;

namespace TechLife.App.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int GroupId { get; set; } = 0;
        public int Level { get; set; } = 0;
      
    }
}
