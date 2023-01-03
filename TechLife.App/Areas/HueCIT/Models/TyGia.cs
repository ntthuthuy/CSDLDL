using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class TyGia
    {
        public int ID { get; set; }
        [Required]
        public string TenNgoaiTe { get; set; }
        [Required]
        public string KyHieu { get; set; }
        [Required]
        public DateTime Ngay { get; set; }
        public decimal GiaMua { get; set; }
        public decimal GiaBan { get; set; }
    }

    public class TyGiaRequest
    {
        public string Ngay { get; set; }
    }
}
