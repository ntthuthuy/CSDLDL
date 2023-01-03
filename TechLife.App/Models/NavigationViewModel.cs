using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TechLife.Model;

namespace TechLife.App.Models
{
    public class NavigationViewModel
    {
        public List<SelectListItem> Languages { get; set; }

        public string CurrentLanguageId { get; set; }

        public string ReturnUrl { set; get; }
        public UserModel UserInfo { set; get; }
    }
}