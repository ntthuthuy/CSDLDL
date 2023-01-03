using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DiemVeSinhDongBo
    {
        public int id { get; set; }
        public string tendoituong { get; set; }
        public string vitri { get; set; }
        public string donvi { get; set; }
        public string hientrang { get; set; }
        public string ghichu { get; set; }
        public string kd { get; set; }
        public string vd { get; set; }
        public string anh { get; set; }
    }

    public class DanhSachDiemVeSinhDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<DiemVeSinhDongBo> data { get; set; }
        public int totalrow { get; set; }
    }

}
