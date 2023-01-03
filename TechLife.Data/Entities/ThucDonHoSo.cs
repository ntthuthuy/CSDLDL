using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class ThucDonHoSo
    {
        public int Id { get; set; }
        public int HosoId { get; set; }
        public string TenThucDon { get; set; }
        public decimal DonGia { get; set; }
        public string MoTa { get; set; }
    }
}
