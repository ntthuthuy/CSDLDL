using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class VanBanHoSoThanhTra
    {
        public int Id { get; set; }
        public int HoSoThanhTraId { get; set; }
        public string SoHieu { get; set; }
        public string TenVanBan { get; set; }
        public DateTime NgayKy { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
