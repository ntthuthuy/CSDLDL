using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.TongHop
{
    public class TongHopCreateRequest
    {
        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Vui lòng chọn quốc tịch")]
        public string QuocTichId { get; set; }

        [Display(Name = "Tháng")]
        [Range(1, 12, ErrorMessage = "Tháng không hợp lệ")]
        public int Month { get; set; }

        [Display(Name = "Năm")]
        [Range(1, 9999, ErrorMessage = "Năm không hợp lệ")]
        public int Year { get; set; }

        [Display(Name = "Số liệu")]
        [Required(ErrorMessage = "Vui lòng nhập số liệu")]
        public string SoLieu { get; set; }
    }
}
