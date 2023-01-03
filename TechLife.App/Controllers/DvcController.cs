using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Model.VanBanDen;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class DvcController : BaseController
    {
        private readonly IHscvApiClient _hscvApiClient;
        public DvcController(IUserService userService
         , IConfiguration configuration
            , IHscvApiClient hscvApiClient)
         : base(userService, configuration)
        {
            _hscvApiClient = hscvApiClient;
        }
        public IActionResult Tracuu()
        {
            return View();
        }
        public async Task<IActionResult> Vanbanden()
        {
            var request = new VanBanDenRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                NgayBanHanh = !String.IsNullOrEmpty(Request.Query["ngay"]) ? Request.Query["ngay"].ToString() : "",
            };

            var data = await _hscvApiClient.DSVanBanDen(SystemConstants.AppSettings.UniqueCode, request);

            return View(data);
        }
    }
}
