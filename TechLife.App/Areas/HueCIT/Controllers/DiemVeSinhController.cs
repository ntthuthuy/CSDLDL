using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Service;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class DiemVeSinhController : BaseController
    {
		private readonly IDiemVeSinhScheduleRepository _diemVeSinhScheduleRepository;
		public DiemVeSinhController(IUserService userService, 
									IConfiguration configuration,
                                    IDiemVeSinhScheduleRepository diemVeSinhScheduleRepository,
                                    ITrackingService trackingService = null) : base(userService, configuration, trackingService)
		{
			_diemVeSinhScheduleRepository = diemVeSinhScheduleRepository;
		}

		public async Task<IActionResult> DongBo()
        {
			try
			{
				await _diemVeSinhScheduleRepository.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ điểm vệ sinh công cộng thành công" });
                return Redirect("/Hoso/Vesinhcongcong/Index");
            }
			catch (Exception ex)
			{
				TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
				return Redirect("/Hoso/Vesinhcongcong/Index");
			}
        }
    }
}
