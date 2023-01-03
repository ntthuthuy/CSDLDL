using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
   public class LoaiGiuongPhong
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int SoGiuong { get; set; }
        public int LuuTruId { get; set; }
        public decimal GiaPhong { get; set; }
        public decimal GiaGiuongPhu { get; set; }
        public bool IsDelete { get; set; }
    }
}
