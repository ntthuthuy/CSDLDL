using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.DuLieuDuLich
{
   public class TimKiemDuLieuThanhTraVrm
    {
        public int Id { get; set; }
        public string TruongDoan { get; set; }
        public string NoiDung { get; set; }
        public string KetQua { get; set; }
        public DateTime? ThoiGian { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
