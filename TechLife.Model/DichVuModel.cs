using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class DichVuModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        public string TenDichVu { get; set; }
        public int LoaiId { get; set; }
        public int SucChua { get; set; }
        public string DVT { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
