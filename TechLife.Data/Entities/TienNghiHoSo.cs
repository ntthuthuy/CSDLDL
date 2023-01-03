using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class TienNghiHoSo
    {
        public int Id { get; set; }
        public int TienNghiId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public bool IsPhuPhi { get; set; }
        public bool IsSuDung { get; set; }
        public TienNghi TienNghi { get; set; }
    }
}
