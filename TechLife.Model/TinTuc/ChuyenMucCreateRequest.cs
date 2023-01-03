using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.TinTuc
{
    public class ChuyenMucCreateRequest
    {
        [Display(Name = "Tên chuyên mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên chuyên mục")]
        public string Ten { set; get; }

        [Display(Name = "Mô tả")]
        public string MoTa { set; get; }

        [Display(Name = "Tiêu đề")]
        public string TieuDe { set; get; }

        [Display(Name = "Từ khóa")]
        public string TuKhoa { set; get; }
        public string UrlImage { set; get; }

        [Display(Name = "Ảnh đại diện")]
        public IFormFile Image { get; set; }

        [Display(Name = "Thuộc chuyên mục")]
        public int ParentId { get; set; }

        [Display(Name = "Hiển thị trên Menu")]
        public bool IsHienThiMenu { get; set; }
        public int UserId { get; set; }
        public string NgonNguId { get; set; }
        [Display(Name = "Icon hiển thị trên mobile")]
        public string IconMoblie { get; set; }
        [Display(Name = "Icon hiển thị trên cổng")]
        public string IconWeb { get; set; }
    }
}