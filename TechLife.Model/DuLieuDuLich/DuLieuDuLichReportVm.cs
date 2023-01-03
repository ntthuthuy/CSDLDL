using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
    public class DuLieuDuLichTheoDiaBanReportVm
    {
        public int HuyenId { get; set; }
        public string HuyenName { get; set; }
        public int TongSoCSLT { get; set; }
        public int TongSoKhachSan { get; set; }
        public int TongSoNhaNghi { get; set; }
        public int TongSoNhaKhach { get; set; }
        public int TongSoCanHo { get; set; }
        public int TongSoCSLTKhac { get; set; }

        public int TongSoNhaHang { get; set; }
        public int TongSoCTLH { get; set; }
        public int TongSoCSMS { get; set; }
        public int TongSoDiemDuLich { get; set; }
    }
}
