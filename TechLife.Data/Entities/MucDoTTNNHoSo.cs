using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class MucDoTTNNHoSo
    {
        public int Id { get; set; }
        public int MucDoId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public int DonViTinhId { get; set; }

        public MucDoThongThaoNgoaiNgu MucDoTTNN { get; set; }
    }
}
