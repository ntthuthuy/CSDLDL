using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLife.Data.Entities
{
    public class LoaiGiuong
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
        public List<LoaiPhongHoSo> DSLoaiPhongHoSo { get; set; }
    }
}
