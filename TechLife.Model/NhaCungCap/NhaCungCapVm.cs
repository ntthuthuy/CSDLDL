using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.NhaCungCap
{
    public class NhaCungCapVm
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string MaSoDoanhNghiep { get; set; }
     
        public DateTime? NgayDangKy { get; set; }
        public string TenNguoiDaiDien { get; set; }
        public string ChucVu { get; set; }
        public string SDTDoanhNghiep { get; set; }
        public string SDTNguoiDaiDien { get; set; }
        public string EmailNguoiDaiDien { get; set; }
        public string EmailDoanhNghiep { get; set; }
        public bool IsStatus { get; set; }
    }
}
