using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechLife.Model.NhaCungCap
{
    public class NhaCungCapCreateRequets
    {
        [Display(Name = "Tên đơn vị")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Mã số đơn vị")]
        public string MaSoDoanhNghiep { get; set; }
        [Display(Name = "Ngày cấp phép hoạt động")]
        [DataType(DataType.Date)]
        public DateTime? NgayDangKy { get; set; }
        [Display(Name = "Tên người đại diện")]
        public string TenNguoiDaiDien { get; set; }
        [Display(Name = "Chức vụ người đại diện")]
        public string ChucVu { get; set; }
        [Display(Name = "Số điện thoại doanh nghiệp")]
        public string SDTDoanhNghiep { get; set; }
        [Display(Name = "Số điện thoại người đại diện")]
        public string SDTNguoiDaiDien { get; set; }
        [Display(Name = "Email người đại diện")]
        public string EmailNguoiDaiDien { get; set; }
        [Display(Name = "Email doanh nghiệp")]
        public string EmailDoanhNghiep { get; set; }
    }
}
