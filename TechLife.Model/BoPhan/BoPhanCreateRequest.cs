using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.BoPhan
{
    public class BoPhanCreateRequest
    {

        [Required(ErrorMessage = "Vui lòng nhập tên bộ phận")]
        [Display(Name = "Tên bộ phận")]
        public string TenBoPhan { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Lĩnh vực áp dụng")]
        public List<int> LinhVucId { get; set; }
    }
}
