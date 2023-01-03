using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class HoSoThanhTra
    {
        public int Id { get; set; }
        public int HoSoId { get; set; }
        public string TruongDoan { get; set; }
        public string NoiDung { get; set; }
        public string KetLuan { get; set; }
        public int KetQua { get; set; }
        public DateTime ThoiGian { get; set; }
        public int UserId { get; set; }
        public DateTime NgayTao { get; set; } 
        public bool IsDelete { get; set; }
    }
}
