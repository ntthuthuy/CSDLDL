using System;
using System.Collections.Generic;
using System.Text;
namespace TechLife.Data.Entities
{
    public class HanhTrinhChuyenDi
    {
        public int Id { get; set; }
        public int ChuyenDiId { get; set; }

        public int DiaDiemId { get; set; }
        public string MoTa { get; set; }
        public int Ngay { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }
    }
}