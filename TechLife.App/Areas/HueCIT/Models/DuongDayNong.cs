using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;

namespace TechLife.App.Areas.HueCIT.Models
{
    public class DuongDayNong
    {
        public int ID { get; set; }
        [Required]
        public string DonViTiepNhan { get; set; }
        [Required]
        public int NhomDonVi { get; set; }
        [Required]
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    public class DuongDayNongTrinhDien
    {
        public int ID { get; set; }
        public string DonViTiepNhan { get; set; }
        public int Nhom { get; set; }
        public NhomDuongDayNong NhomDonVi { get; set; }
        public string TenNhom { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    public class DuongDayNongRequest
    {
        public int NhomDonVi { get; set; }
        public int DonVi { get; set; }
    }

    public class DuongDayNongSearch
    {
        public int ID { get; set; }
        public string DonViTiepNhan { get; set; }
    }
}
