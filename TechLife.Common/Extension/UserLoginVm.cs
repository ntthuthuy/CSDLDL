using System;
using System.ComponentModel.DataAnnotations;
using TechLife.Common.Enums;

namespace TechLife.Common.Extension
{
    public class UserLoginVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string IdToken { get; set; }
        public string AvartarUrl { get; set; }
        public LoginType LoginType { get; set; }

    }
}
