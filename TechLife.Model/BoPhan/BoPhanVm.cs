using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.BoPhan
{
    public class BoPhanVm
    {
        public int Id { get; set; }
        public string TenBoPhan { get; set; }
        public string MoTa { get; set; }
        public int ViTri { get; set; }
        public string LinhVucId { get; set; }
    }
}
