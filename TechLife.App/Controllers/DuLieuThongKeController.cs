using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model.HoatDongKinhDoanh;
using TechLife.Model.TongHop;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class DuLieuThongKeController : BaseController
    {
        private readonly IHoatDongKinhDoanhService _hoatDongKinhDoanhService;
        private readonly IDanhMucDuLieuThongKeService _danhMucDuLieuThongKeService;
        private readonly ITongHopService _tongHopService;
        private readonly IQuocTichService _quocTichService;
        private readonly ILogger<DuLieuThongKeController> _logger;

        public DuLieuThongKeController(IUserService userService
            , IHoatDongKinhDoanhService hoatDongKinhDoanhService
            , IDanhMucDuLieuThongKeService danhMucDuLieuThongKeService
            , ITongHopService tongHopService
            , IQuocTichService quocTichService
            , IConfiguration configuration
            , ILogger<DuLieuThongKeController> logger
            , ITrackingService trackingService = null)
            : base(userService, configuration, trackingService)
        {
            _hoatDongKinhDoanhService = hoatDongKinhDoanhService;
            _danhMucDuLieuThongKeService = danhMucDuLieuThongKeService;
            _tongHopService = tongHopService;
            _quocTichService = quocTichService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult HoatDongKinhDoanh()
        {
            try
            {
                ViewData["Title"] = "Hoạt động kinh doanh";
                ViewData["Title_parent"] = "Dữ liệu thống kê";

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo hoạt động kinh doanh");
                return View(pageError404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> HoatDongKinhDoanhList(HoatDongKinhDoanhFormRequest request)
        {
            try
            {
                ViewData["Title"] = "Hoạt động kinh doanh";
                request.PageIndex = !string.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex;
                request.PageSize = !string.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize;

                var months = Enumerable.Range(1, 12).ToList();

                int minYear = await _hoatDongKinhDoanhService.MinYear();

                request.Thang = (request.Thang < 1 || request.Thang > 12) ? DateTime.Now.Month : request.Thang;
                request.Nam = (request.Nam < minYear - 5 || request.Nam > DateTime.Now.Year) ? DateTime.Now.Year : request.Nam;

                ViewBag.MonthOptions = months.Select(x => new SelectListItem
                {
                    Text = $"Tháng {x}",
                    Value = x.ToString(),
                    Selected = request.Thang == x
                });

                ViewBag.Thang = request.Thang;
                ViewBag.Nam = request.Nam;

                List<int> years = new();

                for (int i = minYear - 5; i <= DateTime.Now.Year; i++)
                {
                    years.Add(i);
                }

                ViewBag.YearOptions = years.Select(x => new SelectListItem
                {
                    Text = $"Năm {x}",
                    Value = x.ToString(),
                    Selected = request.Nam == x
                });

                var data = await _hoatDongKinhDoanhService.GetPaging(request);

                return PartialView(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem danh sách hoạt động kinh doanh");
                return PartialView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImportHoatDongKinhDoanh(int thang, int nam)
        {
            try
            {
                var months = Enumerable.Range(1, 12).ToList();

                thang = thang == 0 ? DateTime.Now.Month : thang;
                nam = nam == 0 ? DateTime.Now.Year : nam;

                ViewBag.MonthOptions = months.Select(x => new SelectListItem
                {
                    Text = $"Tháng {x}",
                    Value = x.ToString(),
                    Selected = thang == x
                });

                int minYear = await _hoatDongKinhDoanhService.MinYear();

                List<int> years = new();

                for (int i = minYear - 5; i <= DateTime.Now.Year; i++)
                {
                    years.Add(i);
                }

                ViewBag.YearOptions = years.Select(x => new SelectListItem
                {
                    Text = $"Năm {x}",
                    Value = x.ToString(),
                    Selected = nam == x
                });

                return PartialView();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportHoatDongKinhDoanh([FromForm] ImportHoatDongKinhDoanhRequest request)
        {
            try
            {
                if (request.File == null)
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "Vui lòng chọn file" });
                }

                var allowedExtensions = new List<string> { ".xls", ".xlsx" };

                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "File không hợp lệ. Chỉ cho phép file Excel." });
                }

                var stream = request.File.OpenReadStream();

                var fileImport = new List<ImporFileVm>();

                var listDanhMuc = await _danhMucDuLieuThongKeService.GetAll();

                using var workbook = new XLWorkbook(stream);

                IXLWorksheet worksheet = workbook.Worksheet(1);
                var lastCol = worksheet.LastColumnUsed().ColumnNumber();
                var lastRow = worksheet.LastRowUsed().RowNumber();

                for (int i = 9; i < listDanhMuc.Count + 9; i++)
                {
                    var item = new ImporFileVm();

                    item.ChinhThucThangTruoc = decimal.TryParse(worksheet.Cell(i, 5).Value.ToString().Trim(), out _) ? worksheet.Cell(i, 5).Value.ToString().Trim() : "0";
                    item.UocThangHienTai = decimal.TryParse(worksheet.Cell(i, 6).Value.ToString().Trim(), out _) ? worksheet.Cell(i, 6).Value.ToString().Trim() : "0";
                    item.LuyKeTuDauNam = decimal.TryParse(worksheet.Cell(i, 7).Value.ToString().Trim(), out _) ? worksheet.Cell(i, 7).Value.ToString().Trim() : "0";
                    item.DuTinhUocThangSau = decimal.TryParse(worksheet.Cell(i, 8).Value.ToString().Trim(), out _) ? worksheet.Cell(i, 8).Value.ToString().Trim() : "0";

                    fileImport.Add(item);
                }

                var result = await _hoatDongKinhDoanhService.Import(fileImport, request.Month, request.Year);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Import file thất bại");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuaHoatDongKinhDoanh(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var data = await _hoatDongKinhDoanhService.GetById(Id);

                if (data == null) return BadRequest("Dữ liệu không tồn tại");

                var model = new HoatDongKinhDoanhUpdateRequest
                {
                    Id = id,
                    ChinhThucThangTruoc = Functions.ConvertDecimalVND(data.ChinhThucThangTruoc),
                    UocThangHienTai = Functions.ConvertDecimalVND(data.UocThangHienTai),
                    LuyKeTuDauNam = Functions.ConvertDecimalVND(data.LuyKeTuDauNam),
                    DuTinhUocThangSau = Functions.ConvertDecimalVND(data.DuTinhUocThangSau),
                    Thang = data.Thang,
                    Nam = data.Nam
                };

                return PartialView(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem cập nhật báo cáo hoạt động kinh doanh");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaHoatDongKinhDoanh(HoatDongKinhDoanhUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return Ok(new Result<string>() { IsSuccessed = false, Message = "Vui lòng nhập đầy đủ thông tin" });

                request.ChinhThucThangTruoc = Regex.Replace(request.ChinhThucThangTruoc.Trim(), "[,.]", "");
                request.UocThangHienTai = Regex.Replace(request.UocThangHienTai.Trim(), "[,.]", "");
                request.LuyKeTuDauNam = Regex.Replace(request.LuyKeTuDauNam.Trim(), "[,.]", "");
                request.DuTinhUocThangSau = Regex.Replace(request.DuTinhUocThangSau.Trim(), "[,.]", "");

                if (!decimal.TryParse(request.ChinhThucThangTruoc, out _)
                || !decimal.TryParse(request.UocThangHienTai, out _)
                || !decimal.TryParse(request.LuyKeTuDauNam, out _)
                || !decimal.TryParse(request.DuTinhUocThangSau, out _))
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "Vui lòng kiểm tra lại thông tin" });
                }

                var result = await _hoatDongKinhDoanhService.Update(request);

                await Tracking(result.Message);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi cập nhật hoạt động kinh doanh");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public IActionResult XoaHoatDongKinhDoanh(int thang, int nam)
        {
            var model = new HoatDongKinhDoanhDeleteRequest
            {
                Thang = thang,
                Nam = nam
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaHoatDongKinhDoanh(HoatDongKinhDoanhDeleteRequest request)
        {
            try
            {
                var result = await _hoatDongKinhDoanhService.Delete(request);
                await Tracking(result.Message);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xóa");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public IActionResult TongHop()
        {
            ViewData["Title"] = "Tổng hợp";
            ViewData["Title_parent"] = "Dữ liệu thống kê";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TongHopList(TongHopFormRequest request)
        {
            try
            {
                request.Nam = request.Nam == 0 ? DateTime.Now.Year : request.Nam;

                var months = Enumerable.Range(1, 12).ToList();

                ViewBag.MonthOptions = months.Select(x => new SelectListItem
                {
                    Text = $"Tháng {x}",
                    Value = x.ToString(),
                    Selected = x == request.Thang
                });

                int minYear = await _hoatDongKinhDoanhService.MinYear();

                List<int> years = new();

                for (int i = minYear - 5; i <= DateTime.Now.Year; i++)
                {
                    years.Add(i);
                }

                ViewBag.YearOptions = years.Select(x => new SelectListItem
                {
                    Text = $"Năm {x}",
                    Value = x.ToString(),
                    Selected = x == request.Nam
                });

                ViewData["Title"] = $"Tổng hợp thị trường lưu trú và lượt khách các tháng năm {request.Nam}";
                request.PageIndex = !string.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex;
                request.PageSize = !string.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize;

                ViewBag.Thang = request.Thang;
                ViewBag.Nam = request.Nam;

                var result = await _tongHopService.GetPaging(request);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem danh sách tổng hợp thị trường");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImportTongHop(int thang, int nam)
        {
            try
            {
                var months = Enumerable.Range(1, 12).ToList();

                nam = nam == 0 ? DateTime.Now.Year : nam;

                ViewBag.MonthOptions = months.Select(x => new SelectListItem
                {
                    Text = $"Tháng {x}",
                    Value = x.ToString(),
                    Selected = thang != 0 && x == thang
                });

                int minYear = await _hoatDongKinhDoanhService.MinYear();

                List<int> years = new();

                for (int i = minYear - 5; i <= DateTime.Now.Year; i++)
                {
                    years.Add(i);
                }

                ViewBag.YearOptions = years.Select(x => new SelectListItem
                {
                    Text = $"Năm {x}",
                    Value = x.ToString(),
                    Selected = nam == x
                });

                return PartialView();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportTongHop([FromForm] ImportTongHopRequest request)
        {
            try
            {
                if (request.File == null)
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "Vui lòng chọn file" });
                }

                var allowedExtensions = new List<string> { ".xls", ".xlsx" };

                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "File không hợp lệ. Chỉ cho phép file Excel." });
                }

                var stream = request.File.OpenReadStream();

                var fileImport = new TongHopImportRequest();

                fileImport.Thang = request.Month;
                fileImport.Nam = request.Year;
                fileImport.Items = new();

                using var workbook = new XLWorkbook(stream);

                IXLWorksheet worksheet = workbook.Worksheet(1);
                var lastCol = worksheet.LastColumnUsed().ColumnNumber();
                var lastRow = worksheet.LastRowUsed().RowNumber();

                for (int i = 2; i <= lastRow - 1; i++)
                {
                    var item = new TongHopImportVm();

                    item.TenQuocTich = worksheet.Cell(i, 2).Value.ToString().Trim();

                    item.SoLieu = worksheet.Cell(i, 3).Value.ToString().Trim();
                    item.SoLieu = Regex.Replace(item.SoLieu, "[,.]", "");
                    item.SoLieu = decimal.TryParse(item.SoLieu, out _) ? item.SoLieu : "0";

                    item.CongDon = worksheet.Cell(i, 4).Value.ToString().Trim();
                    item.CongDon = Regex.Replace(item.CongDon, "[,.]", "");
                    item.CongDon = decimal.TryParse(item.CongDon, out _) ? item.CongDon : "0";

                    item.ThiPhan = worksheet.Cell(i, 5).Value.ToString().Trim();
                    item.ThiPhan = Regex.Replace(item.ThiPhan, "[,%]", "");
                    item.ThiPhan = decimal.TryParse(item.ThiPhan, out _) ? item.ThiPhan : "0";

                    fileImport.Items.Add(item);
                }

                var result = await _tongHopService.Import(fileImport);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Import file thất bại");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuaTongHop(int thang, int nam)
        {
            try
            {
                var listQuocTich = await _quocTichService.GetAll();

                ViewBag.QuocTichOptions = listQuocTich.Select(x => new SelectListItem
                {
                    Text = x.TenQuocTich,
                    Value = HashUtil.EncodeID(x.Id.ToString())
                });

                var model = new TongHopUpdateRequest
                {
                    CongDon = "",
                    SoLieu = "",
                    ThiPhan = 0,
                    Month = thang,
                    Year = nam
                };

                return PartialView(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaTongHop(TongHopUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return Ok(new Result<bool>() { IsSuccessed = false, Message = "Vui lòng nhập đầy đủ thông tin" });

                request.SoLieu = Regex.Replace(request.SoLieu.Trim(), "[,.]", "");
                request.CongDon = Regex.Replace(request.CongDon.Trim(), "[,.]", "");

                if (!decimal.TryParse(request.SoLieu, out _) || !decimal.TryParse(request.CongDon, out _) || request.ThiPhan < 0 || request.ThiPhan > 100)
                {
                    return Ok(new Result<bool>() { IsSuccessed = false, Message = "Vui lòng kiểm tra lại thông tin" });
                }

                var result = await _tongHopService.Update(request);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi cập nhật");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public IActionResult XoaTongHop(int thang, int nam)
        {
            var model = new TongHopDeleteRequest()
            {
                Thang = thang,
                Nam = nam
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaTongHop(TongHopDeleteRequest request)
        {
            try
            {
                var result = await _tongHopService.Delete(request);
                await Tracking(result.Message);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xóa tổng hợp thị trường");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }
    }
}
