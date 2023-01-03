using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class TrinhDoHoSo
    {
        public int Id { get; set; }
        public int TrinhDoId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }
        public TrinhDo TrinhDo { get; set; }
    }
}
