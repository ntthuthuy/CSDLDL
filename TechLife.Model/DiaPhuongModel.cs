using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class DiaPhuongModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên địa phương")]
        public string TenDiaPhuong { get; set; }
        public int ParentId { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
