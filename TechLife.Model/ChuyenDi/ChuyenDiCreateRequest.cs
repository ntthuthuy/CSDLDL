using System;
using System.Collections.Generic;
namespace TechLife.Model.ChuyenDi
{
    public class ChuyenDiCreateRequest
    {
        public string TenChuyenDi { get; set; }
        public string MaThietBi { get; set; }
        public string UserName { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public int SoNgay { get; set; }
        public int SoNguoi { get; set; }

        public int TourId { get; set; }
        public List<HanhTrinhChuyenDiCreateRequets> DSHanhTrinh { get; set; }
    }
}