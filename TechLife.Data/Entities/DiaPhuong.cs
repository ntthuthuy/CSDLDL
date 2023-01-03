using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DiaPhuong
    {
        public int Id { get; set; }
        public string TenDiaPhuong { get; set; }
        public int ParentId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        // HueCIT
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
