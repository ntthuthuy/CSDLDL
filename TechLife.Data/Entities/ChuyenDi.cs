using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class ChuyenDi
    {
        public int Id { get; set; }
        public string TenChuyenDi { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MaThietBi { get; set; }
        public string UserName { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }

        public int SoNgay { get; set; }
        public int SoNguoi { get; set; }
        public int TourId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

    }
}