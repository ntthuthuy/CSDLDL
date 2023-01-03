using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class BoPhanHoSo
    {
        public int Id { get; set; }
        public int BoPhanId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }

        public BoPhan BoPhan { get; set; }
    }
}
