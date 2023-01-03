using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class HanhTrinh
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int NoiDenId { get; set; }
        public int Ngay { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }

        public int ThoiGian { get; set; }

        public string Mota { get; set; }
        public bool IsStatus { get; set; }
        public Tour Tour { get; set; }
        public HoSo HoSo { get; set; }
    }
}
