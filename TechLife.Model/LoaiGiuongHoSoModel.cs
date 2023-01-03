using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class LoaiGiuongHoSoModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int SoPhong { get; set; }
        public int DienTich { get; set; }
        public string TenHienThi { get; set; }
        public int SoNguoiLon { get; set; }
        public int SoTreEm { get; set; }
        public decimal GiaPhong { get; set; }
    }
}
