using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.HoatDongKinhDoanh
{
    public class ImportRequest
    {
        [Display(Name = "Tháng")]
        [Range(1, 12, ErrorMessage = "Tháng không hợp lệ")]
        public int Month { get; set; }

        [Display(Name = "File")]
        [Required(ErrorMessage = "Vui lòng chọn file")]
        public IFormFile File { get; set; }
    }
}
