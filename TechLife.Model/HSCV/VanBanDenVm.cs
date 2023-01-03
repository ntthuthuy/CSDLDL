using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HSCV
{
    public class VanBanDenVm
    {
        public bool TrangThaiKetQua { get; set; }
        public string ThongBaoLoi { get; set; }
        public ThongTinTrangDuLieu ThongTinTrangDuLieu { get; set; }
        public List<VanBanDen> DSVanBanDen { get; set; }
    }
    public class ThongTinTrangDuLieu
    {
        public int TongSoTrang { get; set; }
        public int TongSoBangGhi { get; set; }
        public int TrangHienTai { get; set; }
        public int SoBangGhiTraVe { get; set; }
    }
    public class VanBanDen
    {
        public string ID { get; set; }
        public string TenLoaiVanBan { get; set; }
        public string TenLinhVuc { get; set; }
        public string SoKyHieu { get; set; }
        public string TrichYeu { get; set; }
        public string TenCoQuanGui { get; set; }
        public DateTime NgayPhatHanh { get; set; }
    }
    
}
