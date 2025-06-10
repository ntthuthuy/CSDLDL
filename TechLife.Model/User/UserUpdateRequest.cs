using System;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model.User
{
    public class UserUpdateRequest
    {
        public string Id { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime Dob { get; set; }

        [Display(Name = "Hòm thư")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Căn cước công dân")]
        [Required(ErrorMessage = "Vui lòng nhập căn cước công dân")]
        public string CanCuocCongDan { get; set; }

        //public int GroupId { get; set; } = 0;
    }
}