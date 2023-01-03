using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using System.ComponentModel.DataAnnotations;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class ChuDeSuKien
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Chủ đề sự kiện")]
        public string ChuDe { get; set; }
    }
}
