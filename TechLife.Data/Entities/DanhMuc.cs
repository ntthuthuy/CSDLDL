using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LoaiId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        // HueCIT
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
}
