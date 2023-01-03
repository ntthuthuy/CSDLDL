using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.HueCIT
{
    public class LoaiHinhDB
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int LinhVucId { get; set; }
        public int? NguonDongBo { get; set; }
        public int? DongBoID { get; set; }
    }
}
