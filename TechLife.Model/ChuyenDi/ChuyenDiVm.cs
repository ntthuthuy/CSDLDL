using System;
using System.Collections.Generic;
using System.Text;
namespace TechLife.Model.ChuyenDi
{
    public class ChuyenDiVm
    {
        public int Id { get; set; }
        public string MaChuyenDi { get; set; }
        public string TenChuyenDi { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public string DonGia { get; set; }
        public string Avata { get; set; }
        public int SoNgay { get; set; }
        public int SoNguoi { get; set; }

    }
}