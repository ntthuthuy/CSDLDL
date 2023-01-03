using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.Tracking
{
    public class TrackingCreateRequets
    {
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }
    }
}
