using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class LoaiPhong
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LuuTruId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public List<LoaiPhongHoSo> DSLoaiPhongHoSo { get; set; }

    }
}
