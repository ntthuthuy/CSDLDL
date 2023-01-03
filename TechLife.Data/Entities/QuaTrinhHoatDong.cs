using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class QuaTrinhHoatDong
    {
        public int Id { get; set; }
        public int HDVId { get; set; }
        public string HoatDong { get; set; }
        public string ThoiGian { get; set; }
        public string KetQua { get; set; }
        public HuongDanVien HuongDanVien { get; set; }
    }
}
