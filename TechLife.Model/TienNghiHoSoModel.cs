using System;
using System.Collections.Generic;
using System.Text;
using TechLife.Model.TienNghi;

namespace TechLife.Model
{
    public class TienNghiHoSoModel
    {
        public TienNghiVm TienNghi { get; set; }
        public int TienNghiId { get; set; }
        public int HoSoId { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public bool IsPhuPhi { get; set; }
        public bool IsSuDung { get; set; }
    }
}
