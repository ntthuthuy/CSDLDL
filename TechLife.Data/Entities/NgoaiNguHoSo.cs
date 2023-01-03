using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class NgoaiNguHoSo
    {
        public int Id { get; set; }
        public int NgoaiNguId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }

        public NgoaiNgu NgoaiNgu { get; set; }
    }
}
