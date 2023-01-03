using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using System.ComponentModel.DataAnnotations;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class LoaiAmThuc
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại")]
        public string TenLoai { get; set; }
        public int? DongBoID { get; set; }
        public int? NguonDongBo { get; set; }
        public bool IsDelete { get; set; }
    }
}
