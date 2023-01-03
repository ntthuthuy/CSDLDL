using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace TechLife.Model.User
{
    public class UserSocialRequest
    {
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Avata")]
        public string AvataUrl { get; set; }
    }
}