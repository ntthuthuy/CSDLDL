﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model
{
    public class LoaiGiuongModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại giường")]
        public string Ten { get; set; }
   
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
