using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Phong
    {
        public int Id { get; set; }
        public int SoNguoiLon { get; set; }
        public int SoTreEm { get; set; }
        public int LoaiPhongId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
