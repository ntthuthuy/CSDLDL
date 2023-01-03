using System.Collections.Generic;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DoanhNghiepLoaiHinh
    {
        public int Id { get; set; }
        public string LoaiHinh { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DoanhNghiepLoaiHinhTrinhDien
    {
        public int Id { get; set; }
        public string LoaiHinh { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
    }

    public class DoanhNghiepLoaiHinhDongBo
    {
        public int loaiHinhID { get; set; }
        public string tenLoaiHinh { get; set; }
    }
    public class DanhSachDoanhNghiepLoaiHinhDongBo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public List<DoanhNghiepLoaiHinhDongBo> data { get; set; }
        public string optionValues { get; set; }
    }
}
