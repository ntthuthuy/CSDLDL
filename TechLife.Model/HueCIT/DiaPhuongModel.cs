using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.HueCIT
{
    public class DiaPhuongModelDongBo
    {
        public int Id { get; set; }
        public string TenDiaPhuong { get; set; }
        public int ParentId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
