using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class TimKiemDuLieuLuHanhVrm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LoaiHinhId { get; set; }
        public string DuongPho { get; set; }
        public string SoDienThoai { get; set; }
        public decimal DienTichMatbang { get; set; }
        public DateTime? ThoiDiembatDauKinhDoanh { get; set; }
        public string Keyword { get; set; }
        public string SoLuongLaoDong { get; set; }
        public string TenDanhMuc { get; set; }

    }

}
