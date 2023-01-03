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
    public class VcgtController : BaseController
    {
        private readonly IHoSoScheduleRepository _hoSoScheduleRepository;
        public VcgtController(IUserService userService
                             ,IHoSoScheduleRepository hoSoScheduleRepository
                             ,IConfiguration configuration
                             ,ITrackingService trackingService = null) : base(userService, configuration, trackingService)
        {
            _hoSoScheduleRepository = hoSoScheduleRepository;   
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _hoSoScheduleRepository.GetDataVcgt();
                
                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ dịch vụ vui chơi giải trí thành công" });
                return Redirect("/HoSo/Vcgt");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HoSo/Vcgt");
            }
        }
    }
}
