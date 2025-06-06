using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.GiayPhepChungChi
{
    public class GiayPhepCreateRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên loại giấy phép, chứng chỉ")]
        [Display(Name = "Tên loại giấy phép, chứng chỉ")]
        public string Ten { get; set; }

        [Display(Name = "Lĩnh vực sử dụng")]
        public List<int> LoaiHinhId { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string NgonNguId { get; set; }
    }
}
