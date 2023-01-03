using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class BinhLuan
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string NoiDung { get; set; }
        public string Type { get; set; }
        public int HoSoId { get; set; }
        public DateTime NgayBinhLuan { get; set; }
        public string AvataUrl { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
