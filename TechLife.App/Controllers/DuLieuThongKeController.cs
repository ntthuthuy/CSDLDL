using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model.DanhMucDuLieuThongKe;
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
        public async Task<IActionResult> DanhMucDuLieuThongKe(DanhMucDuLieuThongKeFormRequets requets)
        {
            try
            {
                ViewData["Title"] = "Danh mục";
                ViewData["Title_parent"] = "Dữ liệu thống kê";

                var data = await _danhMucDuLieuThongKeService.GetPaging(requets);

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem danh mục");
                return View(pageError404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ThemDanhMucDuLieuThongKe()
        {
            var options = await _danhMucDuLieuThongKeService.GetHierarchy();
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

                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });
                }

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Thêm danh mục không thành công");
                return View(request);
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

                var options = hierarchy
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = HashUtil.EncodeID(x.Id.ToString()),
                        Selected = x.Id == (data.ParentId == null ? 0 : data.ParentId),
                        Disabled = x.Id == data.Id
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

                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Cập nhật thành công",
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi sửa danh mục");
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> HoatDongKinhDoanh(HoatDongKinhDoanhFormRequest requets)
        {
            try
            {
                ViewData["Title"] = "Hoạt động kinh doanh";
                ViewData["Title_parent"] = "Dữ liệu thống kê";

                var data = await _hoatDongKinhDoanhService.GetPaging(requets);

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem báo cáo hoạt động kinh doanh");
                return View(pageError404);
            }
        }
    }
}
