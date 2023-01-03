using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DanhGia
    {
        public int Id { get; set; }
        public string Loai { get; set; }
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public int SoSao { get; set; }
        public int HoSoId { get; set; }
        public string GhiChu { get; set; }
    }
}
