using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class PhanAnhHienTruongLinhVuc
    {
        public int Id { get; set; }
        public string LinhVuc { get; set; }
        public bool IsEnable { get; set; }
    }

    public class ChuyenMucPhanAnhHienTruong
    {
        public int ChuyenMucID { get; set; }
        public string TenChuyenMuc { get; set; }
    }

    public class DanhSachChuyenMucPhanAnhHienTruong
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<ChuyenMucPhanAnhHienTruong> data { get; set; }
    }

    public class LinhVucPhanAnhHienTruongModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên lĩnh vực phán ánh hiện trường")]
        public string LinhVuc { get; set; }
    }
}
