using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class LoaiAmThucDiaDiemAnUong
    {
        public int Id { get; set; }
        public string TenLoaiAmThuc { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
