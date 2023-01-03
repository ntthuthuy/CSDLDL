using System;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class ThuVienMedia
    {
        public Guid ID { get; set; }
        public string TenLeHoi { get; set; }
        public int Loai { get; set; }
        public int Cap { get; set; }
        public string NoiDung { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
        public string DiaDiem { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
