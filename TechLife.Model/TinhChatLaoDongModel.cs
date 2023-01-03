using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class TinhChatLaoDongModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tính chất lao động")]
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
