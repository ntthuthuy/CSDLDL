using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class NganhDaoTaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên ngành đào tạo")]
        public string TenNganhDaoTao { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
