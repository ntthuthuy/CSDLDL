using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Data.Entities;
using TechLife.Model.HoatDongKinhDoanh;
using TechLife.Model.ThongKeSoLieu;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    public class ThongKeSoLieuController : ControllerBase
    {
        private readonly IDanhMucDuLieuThongKeService _danhMucDuLieuThongKeService;
        private readonly IHoatDongKinhDoanhService _hoatDongKinhDoanhService;
        private readonly ITongHopService _tongHopService;
        private readonly IThongKeService _thongKeService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly ILogger<ThongKeSoLieuController> _logger;

        public ThongKeSoLieuController(IDanhMucDuLieuThongKeService danhMucDuLieuThongKeService
            , IHoatDongKinhDoanhService hoatDongKinhDoanhService
            , ITongHopService tongHopService
            , IThongKeService thongKeService
            , IDuLieuDuLichService duLieuDuLichService
            , ILogger<ThongKeSoLieuController> logger)
        {
            _danhMucDuLieuThongKeService = danhMucDuLieuThongKeService;
            _hoatDongKinhDoanhService = hoatDongKinhDoanhService;
            _tongHopService = tongHopService;
            _thongKeService = thongKeService;
            _duLieuDuLichService = duLieuDuLichService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Hello world");
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
                        Nam = nam,
                        IsBool =true
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
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "2. Khách do các cơ sở lưu trú phục vụ (1)",
                        DVT = dataHoatDongKinhDoanh.Items[16].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[16].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[16].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[16].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[16].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                    new()
                    {
                        Name = "Trong đó, Khách quốc tế",
                        DVT = dataHoatDongKinhDoanh.Items[17].DVT,
                        ChinhThucThangTruoc = dataHoatDongKinhDoanh.Items[17].ChinhThucThangTruoc,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[17].UocThangHienTai,
                        LuyKeTuDauNam = dataHoatDongKinhDoanh.Items[17].LuyKeTuDauNam,
                        DuTinhUocThangSau = dataHoatDongKinhDoanh.Items[17].DuTinhUocThangSau,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
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
                        Nam = nam,
                        IsBool =true
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

                result.Top10 = dataTongHop.Items.LastOrDefault().SoLieu.Values.Sum() == 0m
                    ? new()
                    : dataTongHop.Items.Where(x => x.SoLieu.Values.Sum() > 0 && x.QuocTichId != 0).Select(x => x.TenQuocTich).Take(10).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo {0}", Request.GetFullUrl());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetThongKeDoanhThu")]
        [AllowAnonymous]
        public async Task<IActionResult> GetThongKeDoanhThu([FromQuery] int thang, int nam)
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
                        DVT = "Lượt",
                        ChinhThucThangTruoc = thang!=1?  dataHoatDongKinhDoanh.Items[0].ChinhThucThangTruoc:null,
                        UocThangHienTai =dataHoatDongKinhDoanh.Items[0].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[0].LuyKeTuDauNam:null,
                        DuTinhUocThangSau =thang==5? dataHoatDongKinhDoanh.Items[0].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                    new()
                    {
                        Name = "Khách quốc tế",
                        DVT = dataHoatDongKinhDoanh.Items[1].DVT,
                        ChinhThucThangTruoc =thang!=1? dataHoatDongKinhDoanh.Items[1].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[1].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[1].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[1].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "Khách nội địa",
                        DVT = dataHoatDongKinhDoanh.Items[3].DVT,
                        ChinhThucThangTruoc =thang!=1? dataHoatDongKinhDoanh.Items[3].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[3].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[3].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[3].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "2. Khách do các cơ sở lưu trú phục vụ",
                         DVT = "Lượt",
                        ChinhThucThangTruoc =thang!=1?  dataHoatDongKinhDoanh.Items[16].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[16].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[16].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[16].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                    new()
                    {
                        Name = "Khách quốc tế",
                        DVT = dataHoatDongKinhDoanh.Items[17].DVT,
                        ChinhThucThangTruoc = thang!=1? dataHoatDongKinhDoanh.Items[17].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[17].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[17].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[17].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                      new()
                    {
                        Name = "Khách nội địa",
                        DVT = dataHoatDongKinhDoanh.Items[18].DVT,
                        ChinhThucThangTruoc =thang!=1?  dataHoatDongKinhDoanh.Items[18].ChinhThucThangTruoc :null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[18].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[18].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[18].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "3. Ngày khách lưu trú",
                        DVT = "Ngày",
                        ChinhThucThangTruoc =thang!=1?  dataHoatDongKinhDoanh.Items[19].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[19].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[19].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[19].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                    new()
                    {
                        Name = "Khách quốc tế",
                        DVT = dataHoatDongKinhDoanh.Items[20].DVT,
                        ChinhThucThangTruoc =thang!=1? dataHoatDongKinhDoanh.Items[20].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[20].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[20].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[20].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                      new()
                    {
                        Name = "Khách nội địa",
                        DVT = dataHoatDongKinhDoanh.Items[21].DVT,
                        ChinhThucThangTruoc =thang!=1?  dataHoatDongKinhDoanh.Items[21].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[21].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[21].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[21].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "4. Tổng thu từ du lịch",
                        DVT = "Nghìn đồng",
                        ChinhThucThangTruoc = thang!=1? dataHoatDongKinhDoanh.Items[32].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[32].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[32].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[32].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                    new()
                    {
                        Name = "Doanh thu từ lữ hành",
                        DVT = dataHoatDongKinhDoanh.Items[25].DVT,
                        ChinhThucThangTruoc = thang!=1? dataHoatDongKinhDoanh.Items[25].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[25].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[25].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[25].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                     new()
                    {
                        Name = "Doanh thu từ cơ sở lưu trú",
                        DVT = dataHoatDongKinhDoanh.Items[26].DVT,
                        ChinhThucThangTruoc = thang!=1? dataHoatDongKinhDoanh.Items[26].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[26].UocThangHienTai,
                        LuyKeTuDauNam = thang!=1?dataHoatDongKinhDoanh.Items[26].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[26].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =false
                    },
                    new()
                    {
                        Name = "5. Công suất sử dụng buồng",
                        DVT = "%",
                        ChinhThucThangTruoc = thang!=1? dataHoatDongKinhDoanh.Items[34].ChinhThucThangTruoc:null,
                        UocThangHienTai = dataHoatDongKinhDoanh.Items[34].UocThangHienTai,
                        LuyKeTuDauNam =thang!=1? dataHoatDongKinhDoanh.Items[34].LuyKeTuDauNam:null,
                        DuTinhUocThangSau = thang==5?dataHoatDongKinhDoanh.Items[34].DuTinhUocThangSau:null,
                        Thang = thang,
                        Nam = nam,
                        IsBool =true
                    },
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo {0}", Request.GetFullUrl());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetThongKeThiTruong")]
        [AllowAnonymous]
        public async Task<IActionResult> GetThongKeThiTruong([FromQuery] int thang, int nam)
        {
            try
            {

                var dataTongHop = await _tongHopService.GetPaging(new TongHopFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                });


                return Ok(dataTongHop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo {0}", Request.GetFullUrl());
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetTongHopDuLieuDuLich")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTongHopDuLieuDuLich(int thang, int nam)
        {
            try
            {
                if (thang > 12)
                {
                    return StatusCode(500, "Tháng/năm không hợp lệ!");
                }

                thang = thang == 0 ? DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1 : thang;
                nam = nam == 0 ? DateTime.Now.Year : nam;

                if (DateTime.Now.Month == 1)
                    nam = nam - 1;

                var result = new
                {
                    MoTa = "Tính đến tháng " + (thang) + " năm " + nam,
                    CoSoLuuTruTheoLoaiHinh = (await _duLieuDuLichService.LuuTruTheoLoaiHinh()),
                    CoSoLuuTruTheoDiaBan = (await _duLieuDuLichService.LuuTruTheoDiaBan()),
                    KhachSanTheoHangSao = (await _duLieuDuLichService.KhachSanTheoHangSao()),
                    CongTyLuHanhTheoLoaiHinh = (await _duLieuDuLichService.LuHanhTheoLoaiHinh()),
                    DiemDuLichTheoLoaiHinh = (await _duLieuDuLichService.DiemDuLichTheoLoaiHinh()),
                    HuongDanVienTheoLoaiThe = (await _duLieuDuLichService.HDVTheoLoaiThe()),
                    HuongDanVienTheoNgonNgu = (await _duLieuDuLichService.HDVTheoNgonNgu()),

                    SoPhongTheoLoaiHinh = (await _duLieuDuLichService.SoPhongTheoLoaiHinh()),
                    SoPhongTheoDiaBan = (await _duLieuDuLichService.SoPhongTheoDiaBan()),
                    SoGiuongTheoLoaiHinh = (await _duLieuDuLichService.SoGiuongTheoLoaiHinh()),
                    SoGiuongTheoDiaBan = (await _duLieuDuLichService.SoGiuongTheoDiaBan()),

                    TongSoDiSanVanHoa = (await _thongKeService.DiSanVanHoa()),
                    TongSoKhuVuiChoi = (await _thongKeService.KhuVuiChoi()),

                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo {0}", Request.GetFullUrl());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetTongHopThongKeDoanhThu")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTongHopThongKeDoanhThu(int thang, int nam)
        {
            try
            {
                if (thang > 12)
                {
                    return StatusCode(500, "Tháng/năm không hợp lệ!");
                }

                thang = thang == 0 ? DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1 : thang;
                nam = nam == 0 ? DateTime.Now.Year : nam;

                if (DateTime.Now.Month == 1)
                    nam = nam - 1;

                var danhmuc = await _danhMucDuLieuThongKeService.GetHierarchy();

                var request = new HoatDongKinhDoanhFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                };

                var dataHoatDongKinhDoanh = await _hoatDongKinhDoanhService.GetPaging(request);

                var dataTongHop = await _tongHopService.GetPaging(new TongHopFormRequest
                {
                    Nam = nam,
                    Thang = thang,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                });

                var luongKhachTrongNam = await _tongHopService.GetPaging(new TongHopFormRequest
                {
                    Nam = nam,
                    Thang = 0,
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Search = ""
                });

                var result = new
                {
                    MoTa = "Tính đến tháng " + (thang) + " năm " + nam,
                    TongLuotKhachQuocTe = dataHoatDongKinhDoanh.Items[1].LuyKeTuDauNam,
                    TongLuotKhachNoiDia = dataHoatDongKinhDoanh.Items[3].LuyKeTuDauNam,
                    TyLeKhachQuocTeTrenNoiDia = dataHoatDongKinhDoanh.Items[3].LuyKeTuDauNam == 0m ? 0 : Math.Round((decimal)(dataHoatDongKinhDoanh.Items[1].LuyKeTuDauNam / dataHoatDongKinhDoanh.Items[3].LuyKeTuDauNam) * 100, 2),
                    Top10QuocGia = dataTongHop.Items.LastOrDefault().SoLieu.Values.Sum() == 0m
                    ? new()
                    : dataTongHop.Items.Where(x => x.SoLieu.Values.Sum() > 0 && x.QuocTichId != 0).Select(x => new { x.TenQuocTich, x.SoLieu, x.MoTa }).Take(10).ToList(),

                    LuotKhachTheoThang = luongKhachTrongNam.Items.Select(x => new { x.TenQuocTich, x.SoLieu, x.MoTa }).ToList(),
                    CongSuatPhongTrungBinh = dataHoatDongKinhDoanh.Items[34].LuyKeTuDauNam,

                    TongDoanhThu = dataHoatDongKinhDoanh.Items[32].LuyKeTuDauNam,
                    DoanhThuTuKhachQuocTe = dataHoatDongKinhDoanh.Items[23].LuyKeTuDauNam,
                    DoanhThuTuKhachNoiDia = dataHoatDongKinhDoanh.Items[32].LuyKeTuDauNam - dataHoatDongKinhDoanh.Items[23].LuyKeTuDauNam,

                };
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
