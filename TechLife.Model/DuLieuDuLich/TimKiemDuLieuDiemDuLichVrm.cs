using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class TimKiemDuLieuDiemDuLichVrm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int LoaiHinhId { get; set; }
        public string TenDanhMuc { get; set; }
   
        public DateTime? ThoiDiemBatDauKinhDoanh { get; set; }
        public string SoGiayPhep { get; set; }
        public decimal DienTichMatBang { get; set; }
    }
}
