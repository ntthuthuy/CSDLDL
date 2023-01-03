using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class TienNghiModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tiện nghi")]
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int DonViTinhId { get; set; }
        public int[] LinhVucId { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
