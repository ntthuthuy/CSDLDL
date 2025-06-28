using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TechLife.Common.Extension;
using TechLife.Model;

namespace TechLife.App.Models
{
    public class NavigationViewModel
    {
        public List<SelectListItem> Languages { get; set; }

        public string CurrentLanguageId { get; set; }

        public string ReturnUrl { set; get; }
        public UserLoginVm UserInfo { set; get; }
    }
}