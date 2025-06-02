using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.TongHop
{
    public class ImportTongHopRequest
    {
        [Display(Name = "Tháng")]
        [Range(1, 12, ErrorMessage = "Tháng không hợp lệ")]
        public int Month { get; set; }

        [Display(Name = "Năm")]
        [Range(1, 9999, ErrorMessage = "Năm không hợp lệ")]
        public int Year { get; set; }

        [Display(Name = "File")]
        [Required(ErrorMessage = "Vui lòng chọn file")]
        public IFormFile File { get; set; }
    }
}
