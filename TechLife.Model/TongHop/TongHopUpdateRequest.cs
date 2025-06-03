using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.TongHop
{
    public class TongHopUpdateRequest
    {
        public string Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Vui lòng chọn quốc tịch")]
        public string QuocTichId { get; set; }

        [Display(Name = "Số liệu")]
        [Required(ErrorMessage = "Vui lòng nhập số liệu")]
        public string SoLieu { get; set; }

        [Display(Name = "Cộng dồn")]
        [Required(ErrorMessage = "Vui lòng nhập số cộng dồn")]
        public string CongDon { get; set; }

        [Display(Name = "Thị phần", Prompt = "0 đến 100")]
        [Range(0.0, 100.0, ErrorMessage = "Giá trị không hợp lệ")]
        public decimal ThiPhan { get; set; }
    }
}
