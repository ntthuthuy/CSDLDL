using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class VeDichVuHoSo
    {
        public int Id { get; set; }
        public int HosoId { get; set; }
        public string TenVe { get; set; }
        public decimal GiaVe { get; set; }
        public string MoTa { get; set; }
    }
}
