using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class DichVu
    {
        public int Id { get; set; }
        public string TenDichVu { get; set; }
        public int LoaiId { get; set; }
        public int SucChua { get; set; }
        public string DVT { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }

        public List<DichVuHoSo> DSDichVuHoSo { get; set; }
    }
}
