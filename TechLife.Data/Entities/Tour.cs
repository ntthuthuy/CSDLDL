using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Tour
    {
        public int Id { get; set; }
        public int LoaiId { get; set; }
        public int CongTyLuHanhId { get; set; }
        public int SoNgay { get; set; }
        public string TenChuyenDi { get; set; }
        public string MoTaChuyenDi { get; set; }
        public string MaTour { get; set; }
        public string KhoiHanhTu { get; set; }
        public string LichTrinh { get; set; }
        public int HinhThucId { get; set; }
        public decimal Gia { get; set; }
        public bool IsHangNgay { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        public List<HanhTrinh> DSHanhTrinh { get; set; }
    }
}
