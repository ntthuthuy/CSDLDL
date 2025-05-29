using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.HoatDongKinhDoanh
{
    public class HoatDongKinhDoanhUpdateRequest
    {
        public string Id { get; set; }

        [Display(Name = "Mã")]
        public string Code { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string DVT { get; set; }

        [Required]
        public string ChinhThucThangTruoc { get; set; }

        [Required]
        public string UocThangHienTai { get; set; }

        [Required]
        public string LuyKeTuDauNam { get; set; }

        [Required]
        public string DuTinhUocThangSau { get; set; }
        public string ParentId { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
