using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class QuyMoNhaHangLuuTru
    {
        public int Id { get; set; }
        public int HoSoId { get; set; }
        public string TenGoi { get; set; }
        public int DienTich { get; set; }
        public int SoGhe { get; set; }
        public int IsDelete { get; set; }
    }
}
