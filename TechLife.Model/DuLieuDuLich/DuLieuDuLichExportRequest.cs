using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class DuLieuDuLichExportRequest
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string Loai { get; set; }
        public string SoDienThoai { get; set; }
        public string SoQuyetDinh { get; set; }
        public string Email { get; set; }
        public string SoGiayPhep { get; set; }
        public string HoTenNguoiDaiDien { get; set; }
        public string ChucVuNguoiDaiDien { get; set; }
        public string SDTNguoiDaiDien { get; set; }
        public string HangSao { get; set; }

    }
}
