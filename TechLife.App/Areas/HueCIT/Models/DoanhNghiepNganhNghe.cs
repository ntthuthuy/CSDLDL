using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiepNganhNghe
    {
        public int Id { get; set; }
        public string TenNganhNghe { get; set; }
        public int? NguonDongBo { get; set; }
        public string? DongBoID { get; set; }
    }

    public class DoanhNghiepNganhNgheTrinhDien
    {
        public int Id { get; set; }
        public string DongBoID { get; set; }
        public string TenNganhNghe { get; set; }
        public int NguonDongBo { get; set; }
    }
    public class DoanhNghiepNganhNgheDongBo
    {
        public string maNganhNghe { get; set; }
        public string tenNganhNghe { get; set; }
    }
    public class DanhSachDoanhNghiepNganhNgheDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public List<DoanhNghiepNganhNgheDongBo> data { get; set; }
        public string optionValues { get; set; }
    }
}
