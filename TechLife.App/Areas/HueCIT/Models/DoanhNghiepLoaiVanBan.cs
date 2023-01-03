using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiepLoaiVanBan
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DoanhNghiepLoaiVanBanTrinhDien
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DoanhNghiepLoaiVanBanDongBo
    {
        public int loaiGiayPhepID { get; set; }
        public string loaiGiayPhep { get; set; }
    }
    public class DanhSachDoanhNghiepLoaiVanBanDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public List<DoanhNghiepLoaiVanBanDongBo> data { get; set; }
        public string optionValues { get; set; }
    }
}
