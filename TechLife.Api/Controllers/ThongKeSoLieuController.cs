using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model.HoatDongKinhDoanh;
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

                var request = new HoatDongKinhDoanhFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                };

                var dataHoatDongKinhDoanh = await _hoatDongKinhDoanhService.GetPaging(request);

                result.ListHoatDongKinhDoanh = new List<HoatDongKinhDoanhVm>
                {
                    new()
                    {
                        Name = "1. Khách du lịch",
                        DVT = dataHoatDongKinhDoanh.Items[0].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[0].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[0].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[0].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[0].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam
                    },
                    new()
                    {
                        Name = "Trong đó, Khách quốc tế",
                        DVT = dataHoatDongKinhDoanh.Items[1].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[1].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[1].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[1].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[1].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam
                    },
                    new()
                    {
                        Name = "2. Khách do các cơ sở lưu trú phục vụ",
                        DVT = dataHoatDongKinhDoanh.Items[15].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[15].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[15].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[15].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[15].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam
                    },
                    new()
                    {
                        Name = "Trong đó, Khách quốc tế",
                        DVT = "",
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[17].ChinhThucThangTruoc + dataHoatDongKinhDoanh.Items[20].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[17].UocThangHienTai + dataHoatDongKinhDoanh.Items[20].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[17].LuyKeTuDauNam + dataHoatDongKinhDoanh.Items[20].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[17].DuTinhUocThangSau + dataHoatDongKinhDoanh.Items[20].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam
                    },
                    new()
                    {
                        Name = "3. Tổng thu từ du lịch",
                        DVT = dataHoatDongKinhDoanh.Items[32].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[32].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[32].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[32].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[32].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam
                    },
                };

                var dataTongHop = await _tongHopService.GetPaging(new TongHopFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                });

                result.Top10 = dataTongHop.Items.LastOrDefault().SoLieu.Values.Sum() == 0m ? new() : dataTongHop.Items.Select(x => x.TenQuocTich).Take(10).ToList();

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
