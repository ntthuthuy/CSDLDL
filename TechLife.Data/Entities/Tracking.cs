using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Tracking
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        //public string FullName { get; set; }
        public string Action { get; set; }
    }
}
