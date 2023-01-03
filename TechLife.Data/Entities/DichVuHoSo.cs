using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DichVuHoSo
    {
        public int Id { get; set; }
        public int DichVuId { get; set; }
        public int HoSoId { get; set; }
        public int QuyMo { get; set; }
        public int DonViTinhId { get; set; }
        public DichVu DichVu { get; set; }
    }
}
