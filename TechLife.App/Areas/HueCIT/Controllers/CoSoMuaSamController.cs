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
    public class CoSoMuaSamController : BaseController
    {
        private readonly IHoSoScheduleRepository _hoSoScheduleRepository;
        public CoSoMuaSamController(IUserService userService, 
                                    IConfiguration configuration,
                                    IHoSoScheduleRepository hoSoScheduleRepository,
                                    ITrackingService trackingService = null) : base(userService, configuration, trackingService)
        {
            _hoSoScheduleRepository = hoSoScheduleRepository;
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _hoSoScheduleRepository.GetDataCoSoMuaSam();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ cơ sở mua sắm thành công" });
                return Redirect("/HoSo/CoSoMuaSam");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HoSo/CoSoMuaSam");
            }
        }
    }
}
