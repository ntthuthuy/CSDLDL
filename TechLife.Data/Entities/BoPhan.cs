using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class BoPhan
    {
        public int Id { get; set; }
        public string TenBoPhan { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int ViTri { get; set; }
        public string LinhVucId { get; set; }
        public List<BoPhanHoSo> DSBoPhanHoSo { get; set; }
    }
}
