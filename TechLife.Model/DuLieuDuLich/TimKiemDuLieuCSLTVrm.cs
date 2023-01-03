using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class TimKiemDuLieuCSLTVrm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LoaiHinhId { get; set; }
        public string DuongPho { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? ThoiDiemKinhDoanh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public decimal DienTichMatBang { get; set; }
        public string HoTenNguoiDaiDien { get; set; }
        public string ChucVuNguoiDaiDien { get; set; }
        public string SoDienThoaiNguoiDaiDien { get; set; }
        public DateTime? NgayCongNhanDatChuan { get; set; }
        public int HangSao { get; set; }
        public int TienNghiId { get; set; }
        //public LoaiHinhModel LoaiHinh { get; set; }
    }
}
