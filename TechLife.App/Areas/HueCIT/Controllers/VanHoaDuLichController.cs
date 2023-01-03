using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class VanHoaDuLichController : BaseController
    {
        private readonly int _pageSize = 1;
        public VanHoaDuLichController(IUserService userService, IConfiguration configuration)
            : base(userService, configuration)
        {
            
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Văn hóa - Du lịch";

            return View();
        }
    }
}