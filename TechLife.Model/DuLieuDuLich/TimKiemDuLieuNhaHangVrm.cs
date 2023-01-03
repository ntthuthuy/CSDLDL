using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class TimKiemDuLieuNhaHangVrm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string DuongPho { get; set; }
        public int LoaiId { get; set; }
        public string SoDienThoai { get; set; }
        public string TenDichVu { get; set; }         
        public DateTime? ThoiDiemBatDauKinhDoanh { get; set; } 
        public string Keyword { get; set; }
        public DateTime? NgayCVDatChuan { get; set; }
        public decimal DienTichMatBang { get; set; }
    }
}
