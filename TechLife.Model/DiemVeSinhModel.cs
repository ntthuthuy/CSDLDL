using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class DiemVeSinhModel
    {
        public int Id { get; set; }
        public string ViTri { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        // HueCIT
        public int DiemVeSinhID { get; set; }
        public string DonVi { get; set; }
        public string HienTrang { get; set; }
        public string GhiChu { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
