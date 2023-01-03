using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class QuocTichModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên quốc tịch")]
        public string TenQuocTich { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
