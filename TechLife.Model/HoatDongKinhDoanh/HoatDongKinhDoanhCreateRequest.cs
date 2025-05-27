using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.HoatDongKinhDoanh
{
    public class HoatDongKinhDoanhCreateRequest
    {
        [Display(Name = "Mã")]
        public string Code { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string DVT { get; set; }

        public string ChinhThucThangTruoc { get; set; }
        public string UocThangHienTai { get; set; }
        public string LuyKeTuDauNam { get; set; }
        public string DuTinhUocThangSau { get; set; }
        public string ParentId { get; set; }
    }
}
