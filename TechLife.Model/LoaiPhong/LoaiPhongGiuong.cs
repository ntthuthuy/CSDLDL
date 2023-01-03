using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.LoaiPhong
{
    public class LoaiPhongGiuong
    {
        public int Id { get; set; }
        public string TenGoi { get; set; }
        public int SoGiuong { get; set; }
        public string GiaPhong { get; set; }
        public string GiaGiuong { get; set; }
        public bool IsDelete { get; set; }
    }
}
