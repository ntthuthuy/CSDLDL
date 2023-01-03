using System;

namespace TechLife.Model.Tracking
{
    public class TrackingVm
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }
    }
}