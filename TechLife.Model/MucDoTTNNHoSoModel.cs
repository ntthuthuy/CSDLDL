using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class MucDoTTNNHoSoModel
    {
        public MucDoThongThaoNgoaiNguModel MucDoThongThao { get; set; }
        public int MucDoId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }
    }
}
