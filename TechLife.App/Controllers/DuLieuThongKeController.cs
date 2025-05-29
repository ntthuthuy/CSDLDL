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
using TechLife.Model.DanhMucDuLieuThongKe;
using TechLife.Model.HoatDongKinhDoanh;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class DuLieuThongKeController : BaseController
    {
        private readonly IHoatDongKinhDoanhService _hoatDongKinhDoanhService;
        private readonly IDanhMucDuLieuThongKeService _danhMucDuLieuThongKeService;
        private readonly ILogger<DuLieuThongKeController> _logger;

        public DuLieuThongKeController(IUserService userService
            , IHoatDongKinhDoanhService hoatDongKinhDoanhService
            , IDanhMucDuLieuThongKeService danhMucDuLieuThongKeService
            , IConfiguration configuration
            , ILogger<DuLieuThongKeController> logger
            , ITrackingService trackingService = null)
            : base(userService, configuration, trackingService)
        {
            _hoatDongKinhDoanhService = hoatDongKinhDoanhService;
            _danhMucDuLieuThongKeService = danhMucDuLieuThongKeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult DanhMucDuLieuThongKe()
        {
            try
            {
                ViewData["Title"] = "Danh mục";
                ViewData["Title_parent"] = "Dữ liệu thống kê";

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem danh mục");
                return View(pageError404);
            }
        }

        public async Task<IActionResult> DanhMucDuLieuThongKeList(DanhMucDuLieuThongKeFormRequets request)
        {
            ViewData["Title"] = "Danh mục";
            request.PageIndex = !string.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex;
            request.PageSize = !string.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize;

            var data = await _danhMucDuLieuThongKeService.GetPaging(request);

            return PartialView(data);
        }

        [HttpGet]
        public async Task<IActionResult> ThemDanhMucDuLieuThongKe()
        {
            var options = await _danhMucDuLieuThongKeService.GetHierarchy();

            foreach (var item in options)
            {
                item.Name = (item.Code != null ? item.Code + ". " : "") + item.Name;

                if (item.Level > 0)
                {
                    string space = "";

                    for (int i = 0; i < item.Level; i++) space += "- ";

                    item.Name = space + item.Name;
                }
            }

            ViewBag.Options = options.Select(x => new SelectListItem { Text = x.Name, Value = HashUtil.EncodeID(x.Id.ToString()) });
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ThemDanhMucDuLieuThongKe(DanhMucDuLieuThongKeCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Result<bool>() { IsSuccessed = false, Message = "Vui lòng nhập đầy đủ thông tin!" });
                }

                var result = await _danhMucDuLieuThongKeService.Create(request);
                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Thêm danh mục không thành công");
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Đã có lỗi xảy ra" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuaDanhMucDuLieuThongKe(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var data = await _danhMucDuLieuThongKeService.GetById(Id);

                var model = new DanhMucDuLieuThongKeUpdateRequest
                {
                    Id = HashUtil.EncodeID(data.Id.ToString()),
                    Code = data.Code,
                    Name = data.Name,
                    ParentId = data.ParentId != null ? HashUtil.EncodeID(data.ParentId.ToString()) : null,
                };

                var hierarchy = await _danhMucDuLieuThongKeService.GetHierarchy();

                foreach (var item in hierarchy)
                {
                    item.Name = (item.Code != null ? item.Code + ". " : "") + item.Name;

                    if (item.Level > 0)
                    {
                        string space = "";

                        for (int i = 0; i < item.Level; i++) space += "- ";

                        item.Name = space + item.Name;
                    }
                }

                var childrents = hierarchy.Where(x => x.Parents.Split(',').Select(int.Parse).Contains(Id)).ToList();

                var options = hierarchy
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = HashUtil.EncodeID(x.Id.ToString()),
                        Selected = x.Id == (data.ParentId == null ? 0 : data.ParentId),
                        Disabled = childrents.Contains(x)
                    });

                ViewBag.Options = options;

                return PartialView(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi sửa danh mục");
                return View(pageError404);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SuaDanhMucDuLieuThongKe(DanhMucDuLieuThongKeUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Result<string>() { IsSuccessed = false, Message = "Vui lòng nhập đầy đủ thông tin" });
                }

                var result = await _danhMucDuLieuThongKeService.Update(request);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi sửa danh mục");
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Đã có lỗi xảy ra" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(string id, int value)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var result = await _danhMucDuLieuThongKeService.UpdateOrder(Id, value);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi cập nhật vị trí");
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Đã có lỗi xảy ra" });
            }
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

                ViewBag.Month = request.Thang;

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
        public async Task<IActionResult> ImportHoatDongKinhDoanh()
        {
            var months = Enumerable.Range(1, 12).ToList();

            ViewBag.MonthOptions = months.Select(x => new SelectListItem
            {
                Text = $"Tháng {x}",
                Value = x.ToString(),
                Selected = DateTime.Now.Month == x
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
                Selected = DateTime.Now.Year == x
            });

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ImportHoatDongKinhDoanh([FromForm] ImportRequest request)
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

                using var workbook = new XLWorkbook(stream);

                IXLWorksheet worksheet = workbook.Worksheet(1);
                var lastCol = worksheet.LastColumnUsed().ColumnNumber();
                var lastRow = worksheet.LastRowUsed().RowNumber();

                for (int i = 9; i <= 42; i++)
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
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Import file thất bại" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuaHoatDongKinhDoanh(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var data = await _hoatDongKinhDoanhService.GetById(Id);

                if (data == null) return BadRequest(new Result<string>() { IsSuccessed = false, Message = "Dữ liệu không tồn tại" });

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
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Đã có lỗi xảy ra" });
            }
        }

        [HttpPost]
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
                return Ok(new Result<string>() { IsSuccessed = false, Message = "Đã có lỗi xảy ra" });
            }
        }
    }
}
