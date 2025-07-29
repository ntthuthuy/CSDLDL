using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model.ThongKeSoLieu;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    public class ThongKeSoLieuController : ControllerBase
    {
        private readonly IDanhMucDuLieuThongKeService _danhMucDuLieuThongKeService;
        private readonly IHoatDongKinhDoanhService _hoatDongKinhDoanhService;
        private readonly ITongHopService _tongHopService;
        private readonly ILogger<ThongKeSoLieuController> _logger;

        public ThongKeSoLieuController(IDanhMucDuLieuThongKeService danhMucDuLieuThongKeService
            , IHoatDongKinhDoanhService hoatDongKinhDoanhService
            , ITongHopService tongHopService
            , ILogger<ThongKeSoLieuController> logger)
        {
            _danhMucDuLieuThongKeService = danhMucDuLieuThongKeService;
            _hoatDongKinhDoanhService = hoatDongKinhDoanhService;
            _tongHopService = tongHopService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Hello word");
        }

        [HttpGet("GetSoLieu")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSoLieu([FromQuery] int thang, int nam)
        {
            try
            {
                var danhmuc = await _danhMucDuLieuThongKeService.GetHierarchy();

                var result = new ThongKeSoLieuVm() { ListHoatDongKinhDoanh = new(), Top10 = new() };

                for (int i = 0; i < 5; i++)
                {
                    result.ListHoatDongKinhDoanh.Add(new());
                }

                var request = new HoatDongKinhDoanhFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                };

                var dataHoatDongKinhDoanh = await _hoatDongKinhDoanhService.GetPaging(request);

                foreach (var item in dataHoatDongKinhDoanh.Items)
                {
                    if (item.DanhMucId == 1)
                        result.ListHoatDongKinhDoanh[0] = item;
                    else if (item.DanhMucId == 9)
                        result.ListHoatDongKinhDoanh[1] = item;
                    else if (item.DanhMucId == 23)
                        result.ListHoatDongKinhDoanh[2] = item;
                    else if (item.DanhMucId == 25 || item.DanhMucId == 28)
                    {
                        var t = result.ListHoatDongKinhDoanh[3];
                        t.Name = item.Name;
                        t.ChinhThucThangTruoc += item.ChinhThucThangTruoc;
                        t.UocThangHienTai += item.UocThangHienTai;
                        t.DuTinhUocThangSau += item.DuTinhUocThangSau;
                        t.Thang = thang;
                        t.Nam = nam;
                    }
                    else if (item.DanhMucId == 3)
                        result.ListHoatDongKinhDoanh[4] = item;
                }

                var dataTongHop = await _tongHopService.GetPaging(new TongHopFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                });

                result.Top10 = dataTongHop.Items.Select(x => x.TenQuocTich).Take(10).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo {0}", Request.GetFullUrl());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
