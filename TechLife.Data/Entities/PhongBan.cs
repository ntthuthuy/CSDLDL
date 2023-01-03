using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class PhongBan
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MaDinhDanh { get; set; }
        public string SoDienThoai { get; set; }
        public bool IsDelete { get; set; }
    }
}
