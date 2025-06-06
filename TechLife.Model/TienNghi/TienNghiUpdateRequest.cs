using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.TienNghi
{
    public class TienNghiUpdateRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tiện nghi")]
        [Display(Name = "Tên tiện nghi")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Đơn vị tính")]
        public int DonViTinhId { get; set; }
        [Display(Name = "Lĩnh vực áp dụng")]
        public int[] LinhVucId { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string NgonNguId { get; set; }
    }
}
