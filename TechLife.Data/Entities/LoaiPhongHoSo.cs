using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class LoaiPhongHoSo
    {
        public int Id { get; set; }
        public string TenHienThi { get; set; }
        public int LoaiPhongId { get; set; }
        public int HoSoId { get; set; }
        public int LoaiGiuongId { get; set; }
        public int SoPhong { get; set; }
        public int DienTich { get; set; }
        public int SoNguoiLon { get; set; }
        public int SoTreEm { get; set; }
        public decimal GiaPhong { get; set; }

        public LoaiPhong LoaiPhong { get; set; }
        public LoaiGiuong LoaiGiuong { get; set; }
    }
}
