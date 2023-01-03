using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class TrinhDoHoSoModel
    {
        public TrinhDoModel TrinhDo { get; set; }
        public int TrinhDoId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }
    }
}
