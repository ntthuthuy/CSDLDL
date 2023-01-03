using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiepTrangThai
    {
        public int Id { get; set; }
        public string TrangThai { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
        public string TenClassCSS { get; set; }
    }

    public class DoanhNghiepTrangThaiTrinhDien
    {
		public int Id { get; set; }
		public string TrangThai { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }
	public class DoanhNghiepTrangThaiDongBo
    {
		public int trangThaiID { get; set; }
		public string tenTrangThai { get; set; }
	}
    public class DanhSachDoanhNghiepTrangThaiDongBo
	{
        public int code { get; set; }
		public string message { get; set; }
		public string description { get; set; }
		public List<DoanhNghiepTrangThaiDongBo> data { get; set; }
		public string optionValues { get; set; }
    }
}
