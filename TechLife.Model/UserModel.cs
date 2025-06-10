using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; } = false;

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime Dob { get; set; }

        [Display(Name = "Hòm thư")]
        public string Email { get; set; }
        [Display(Name = "Loại tài khoản")]
        public int TypeId { get; set; } = 0;

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string AvataUrl { get; set; }
        public int GroupId { get; set; } = 0;

        public IList<string> Roles { get; set; }

        [Display(Name = "Căn cước công dân")]
        [Required(ErrorMessage = "Vui lòng nhập căn cước công dân")]
        public string CanCuocCongDan { get; set; }
    }

}
