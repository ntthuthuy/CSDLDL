using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.TinTuc
{
    public class TinTucUpdateRequest
    {
        public string Id { get; set; }

        [Display(Name = "Thuộc chuyên mục")]
        public int ChuyenMucId { get; set; }

        [Display(Name = "Chuyên mục khác")]
        public List<int> listChuyenMucId { get; set; }

        [Display(Name = "Thuộc hồ sơ")]
        public int HoSoId { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string NgonNguId { get; set; }

        [Display(Name = "Tiêu đề bài viết")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        public string TieuDe { get; set; }

        [Display(Name = "Đường dẫn thân thiện")]
        public string URL { get; set; }

        public string AnhDaiDien { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public IFormFile Image { get; set; }
        [Display(Name = "Mô tả ảnh đại diện")]
        public string MoTaAnh { get; set; }

        [Display(Name = "Tóm tắt nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        public string MoTa { get; set; }
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết")]
        public string NoiDung { get; set; }
        [Display(Name = "Nguồn tin")]
        public string NguonTin { get; set; }
        [Display(Name = "Tác giả")]
        public string TacGia { get; set; }
        [Display(Name = "Tác quyền")]
        public string TacQuyen { get; set; }
        [Display(Name = "Nhãn")]
        public string Tag { get; set; }
        [Display(Name = "Nguồn ngôn ngữ")]
        public string NguonNgonNguId { get; set; }
        [Display(Name = "Tin bài được chia sẻ")]
        public bool IsTinBaiChiaSe { get; set; }
        [Display(Name = "Tin tiêu điểm")]
        public bool IsTinTieuDiem { get; set; }
        [Display(Name = "Tin khuyến mãi")]
        public bool IsTinKhuyenMai { get; set; }
        [Display(Name = "Tin lễ hội")]
        public bool IsTinLeHoi { get; set; }
        [Display(Name = "Ngày diễn ra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayDienRa { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayKetThuc { get; set; }
    }
}
