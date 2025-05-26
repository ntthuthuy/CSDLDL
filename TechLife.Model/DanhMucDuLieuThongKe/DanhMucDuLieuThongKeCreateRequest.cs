using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.DanhMucDuLieuThongKe
{
    public class DanhMucDuLieuThongKeCreateRequest
    {
        [Display(Name = "Mã")]
        public string Code { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string DVT { get; set; }

        public string ParentId { get; set; }
    }
}
