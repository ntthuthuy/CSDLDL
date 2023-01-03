using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.TienNghi
{
    public class TienNghiCreateRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tiện nghi")]
        [Display(Name = "Tên tiện nghi")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Đơn vị tính")]
        public int DonViTinhId { get; set; }
        [Display(Name = "Lĩnh vực áp dụng")]
        public int[] LinhVucId { get; set; }
    }
}
