using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class HoSoVanBan
    {
        public int Id { get; set; }
        public int HosoId { get; set; }
        public int GiayPhepId { get; set; }
        public string TenGoi { get; set; }
        public string MaSo { get; set; }
        public string Loai { get; set; }
        public string NoiCap { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsStatus{ get; set; }
        public bool IsDelete { get; set; }
    }
}
