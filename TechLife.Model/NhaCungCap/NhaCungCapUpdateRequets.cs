using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.NhaCungCap
{
    public class NhaCungCapUpdateRequets
    {
        public int Id { get; set; }
        
        [Display(Name = "Tên nhà cung cấp")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Mã số")]
        public string MaSoDoanhNghiep { get; set; }
        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.Date)]
        public DateTime? NgayDangKy { get; set; }
        [Display(Name = "Tên người đại diện")]
        public string TenNguoiDaiDien { get; set; }
        [Display(Name = "Chức vụ người đại diện")]
        public string ChucVu { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SDTDoanhNghiep { get; set; }
        [Display(Name = "Số điện thoại người đại diện")]
        public string SDTNguoiDaiDien { get; set; }
        [Display(Name = "Email người đại diện")]
        public string EmailNguoiDaiDien { get; set; }
        [Display(Name = "Email")]
        public string EmailDoanhNghiep { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
