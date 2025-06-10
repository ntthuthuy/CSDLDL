using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using TechLife.Model.BoPhan;
using TechLife.Model.DanhMucDuLieuThongKe;
using TechLife.Model.GiayPhepChungChi;
using TechLife.Model.TienNghi;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.App.Controllers
{
    [Authorize(Roles = "root")]
    public class DanhmucController : BaseController
    {
        private readonly IGiayPhepService _giayPhepService;
        private readonly ITienNghiService _tienNghiService;
        private readonly IBoPhanService _boPhanService;
        private readonly ILoaiPhongService _loaiPhongService;
        private readonly IDanhMucDongBoRepository _danhMucDongBoRepository;
        private readonly IDanhMucDongBoService _danhMucDongBoService;
        private readonly ILoaiDichVuDongBoService _loaiDichVuDongBoService;
        private readonly IQuocTichService _quocTichService;
        private readonly IDanhMucScheduleRepository _danhMucScheduleRepository;
        private readonly IDanhMucDuLieuThongKeService _danhMucDuLieuThongKeService;
        private readonly IDanhMucService _danhMucService;
        private readonly INgonNguService _ngonNguService;
        private readonly IDichVuService _dichVuService;
        private readonly ILoaiHinhService _loaiHinhService;
        private readonly ILoaiDichVuService _loaiDichVuService;
        private readonly INgoaiNguService _ngoaiNguService;
        private readonly ITrinhDoService _trinhDoService;
        private readonly ILoaiHinhLaoDongService _loaiHinhLaoDongService;
        private readonly IMucDoThongThaoNgoaiNguService _mucDoThongThaoNgoaiNguService;
        private readonly IDonViTinhService _donViTinhService;
        private readonly ILogger<DanhmucController> _logger;
        private const string SERVICE_ID_NHA_HANG = "7NL8tqxUups3P0nh9xI2+w==";
        private const string SERVICE_ID_LU_HANH = "1uDmGjtxCbkEyePdF1lYVQ==";
        private const string SERVICE_ID_DIEM_DU_LICH = "wMhgvtnwerKMAUlFfzNE4w==";
        private const string SERVICE_ID_CO_SO_MUA_SAM = "yhX5adxP6hVfMT6JEPZsxg==";
        private const string SERVICE_ID_KHU_VUI_CHOI = "ukzX6qDQElFQ8mpP3E1bPw==";
        private const string SERVICE_ID_THE_THAO = "CYe596Eym68xxw07d+nVOQ==";
        private const string SERVICE_ID_CSSK = "ZpiYQL99yu80qkCJOdEuHQ==";
        private const string SERVICE_ID_VAN_CHUYEN = "3RyYOuKCRjnstethwapQKw==";
        private const int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;

        public DanhmucController(IUserService userService
            , IConfiguration configuration
            , IDiaPhuongApiClient diaPhuongApiClient
            , IDichVuApiClient dichVuApiClient
            , IDonViTinhApiClient donViTinhApiClient
            , ILoaiHinhApiClient loaiHinhApiClient
            , INgoaiNguApiClient ngoaiNguApiClient
            , ITrinhDoApiClient trinhDoApiClient
            , IBoPhanApiClient boPhanApiClient
            , ILoaiPhongApiClient loaiPhongApiClient
            , IMucDoThongThaoNgoaiNguApiClient mucDoThongThaoNgoaiNguApiClient
            , ITienNghiApiClient tienNghiApiClient
            , IHuongDanVienApiClient huongDanVienApiClient
            , IDiemVeSinhApiClient diemVeSinhApiClient
            , ILoaiGiuongApiClient loaiGiuongApiClient
            , IQuocTichApiClient quocTichApiClient
            , ILoaiDichVuApiClient loaiDichVuApiClient
            , IDanhMucApiClient danhMucApiClient
            , ILoaiHinhLaoDongApiClient loaiHinhLaoDongApiClient
            , ITinhChatLaoDongApiClient tinhChatLaoDongApiClient
            , IDuLieuDuLichApiClient csdlDuLichApiClient
            , IDiaPhuongService diaPhuongService
            , IFileUploadService fileUploadService
            , INhaCungCapService nhaCungCapService
            , ITrackingService trackingService
            , IGiayPhepService giayPhepService
            , ITienNghiService tienNghiService
            , ILoaiPhongService loaiPhongService
            , IBoPhanService boPhanService
            , IDanhMucDongBoRepository danhMucDongBoRepository
            , IDanhMucDongBoService danhMucDongBoService
            , ILoaiDichVuDongBoService loaiDichVuDongBoService
            , IQuocTichService quocTichService
            , IDanhMucScheduleRepository danhMucScheduleRepository
            , IDanhMucDuLieuThongKeService danhMucDuLieuThongKeService
            , IDanhMucService danhMucService
            , INgonNguService ngonNguService
            , IDichVuService dichVuService
            , ILoaiHinhService loaiHinhService
            , ILoaiDichVuService loaiDichVuService
            , INgoaiNguService ngoaiNguService
            , ITrinhDoService trinhDoService
            , ILoaiHinhLaoDongService loaiHinhLaoDongService
            , IMucDoThongThaoNgoaiNguService mucDoThongThaoNgoaiNguService
            , IDonViTinhService donViTinhService
            , ILogger<DanhmucController> logger)
            : base(userService, diaPhuongApiClient
                  , donViTinhApiClient, loaiHinhApiClient
                  , dichVuApiClient, ngoaiNguApiClient
                  , trinhDoApiClient, boPhanApiClient
                  , loaiPhongApiClient, mucDoThongThaoNgoaiNguApiClient
                  , tienNghiApiClient, huongDanVienApiClient
                  , diemVeSinhApiClient, loaiGiuongApiClient
                  , csdlDuLichApiClient
                  , quocTichApiClient
                  , loaiDichVuApiClient
                  , danhMucApiClient
                  , loaiHinhLaoDongApiClient
                  , tinhChatLaoDongApiClient
                  , diaPhuongService
                  , configuration, fileUploadService, nhaCungCapService, trackingService)
        {
            _giayPhepService = giayPhepService;
            _tienNghiService = tienNghiService;
            _boPhanService = boPhanService;
            _loaiPhongService = loaiPhongService;
            _danhMucDongBoRepository = danhMucDongBoRepository;
            _danhMucDongBoService = danhMucDongBoService;
            _loaiDichVuDongBoService = loaiDichVuDongBoService;
            _quocTichService = quocTichService;
            _danhMucScheduleRepository = danhMucScheduleRepository;
            _danhMucDuLieuThongKeService = danhMucDuLieuThongKeService;
            _danhMucService = danhMucService;
            _ngonNguService = ngonNguService;
            _dichVuService = dichVuService;
            _loaiHinhService = loaiHinhService;
            _loaiDichVuService = loaiDichVuService;
            _ngoaiNguService = ngoaiNguService;
            _trinhDoService = trinhDoService;
            _loaiHinhLaoDongService = loaiHinhLaoDongService;
            _mucDoThongThaoNgoaiNguService = mucDoThongThaoNgoaiNguService;
            _donViTinhService = donViTinhService;
            _logger = logger;
        }

        #region ĐỊA PHƯƠNG
        public async Task<IActionResult> Diaphuong()
        {

            ViewData["Title"] = "Địa phương";
            ViewData["Title_parent"] = "Danh mục";
            var data = await _diaPhuongService.GetAll();

            listDiaPhuong = new List<DiaPhuongModel>();
            listDiaPhuong = ListDiaPhuong(data);
            return View(listDiaPhuong);
        }
        List<DiaPhuongModel> ListDiaPhuong(List<DiaPhuongModel> list, int seletedId = 0, int parentId = 0, int level = 0)
        {
            var diaphuong = list.Where(v => v.ParentId == parentId);
            foreach (var x in diaphuong)
            {
                string space = "";
                for (int i = 0; i < level; i++)
                {
                    space += "- ";
                }
                x.TenDiaPhuong = space + x.TenDiaPhuong;
                listDiaPhuong.Add(new DiaPhuongModel()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    IsDelete = x.IsDelete,
                    IsStatus = x.IsStatus,
                    MoTa = x.MoTa,
                    TenDiaPhuong = x.TenDiaPhuong
                });

                var list_chird = list.Where(v => v.ParentId == x.Id);
                if (list_chird.Count() > 0)
                {
                    int level_next = level + 1;
                    ListDiaPhuong(list, seletedId, x.Id, level_next);
                }
            }
            return listDiaPhuong;
        }

        [HttpGet]
        public async Task<IActionResult> Themdiaphuong()
        {

            ViewData["Title"] = "Thêm địa phương";
            ViewData["Title_parent"] = "Danh mục";

            await OptionDiaPhuong();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdiaphuong(DiaPhuongModel request, string type_submit)
        {

            ViewData["Title"] = "Thêm địa phương";
            ViewData["Title_parent"] = "Danh mục";



            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                await OptionDiaPhuong();

                return View(request);
            }

            var result = await _diaPhuongApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                if (type_submit == "save")
                    return Redirect("/Danhmuc/Diaphuong/");
                else
                {
                    await OptionDiaPhuong();

                    request = new DiaPhuongModel()
                    {
                        ParentId = request.ParentId
                    };

                    return View(request);
                }
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });

            await OptionDiaPhuong();

            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suadiaphuong(string id = "")
        {
            ViewData["Title"] = "Sửa địa phương";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _diaPhuongApiClient.GetById(Id);

            await OptionDiaPhuong();

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadiaphuong(string Id, DiaPhuongModel request)
        {

            ViewData["Title"] = "Sửa địa phương";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                await OptionDiaPhuong();

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _diaPhuongApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Diaphuong");
            }

            await OptionDiaPhuong();

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadiaphuong(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _diaPhuongApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Bophan");
        }
        #endregion

        #region QUỐC TỊCH
        public IActionResult Quoctich()
        {

            ViewData["Title"] = "Quốc tịch";
            ViewData["Title_parent"] = "Danh mục";

            return View();
        }

        public async Task<IActionResult> Quoctichlist(GetPagingFormRequest request)
        {
            ViewData["Title"] = "Quốc tịch";

            request.PageIndex = !string.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex;
            request.PageSize = !string.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize;

            var data = await _quocTichService.GetPaging(request);

            return PartialView(data);
        }

        [HttpGet]
        public IActionResult Themquoctich()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themquoctich(QuocTichModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResult<bool>() { IsSuccessed = false, Message = "Vui lòng nhập đầy đủ thông tin" });
                }
                var result = await _quocTichService.Create(request);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi thêm mới quốc tịch");
                return BadRequest("Đã có lỗi xảy ra");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Suaquoctich(string id = "")
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _quocTichService.GetById(Id);
            return PartialView(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suaquoctich(string Id, QuocTichModel request)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiErrorResult<bool>("Vui lòng nhập đầy đủ thông tin"));
                }

                int id = Convert.ToInt32(HashUtil.DecodeID(Id));

                var result = await _quocTichService.Update(id, request);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã có lỗi xảy ra");
                return Ok(new ApiErrorResult<bool>("Cập nhật không thành công"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Xoaquoctich(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var data = await _quocTichService.GetById(Id);

            if (data == null) return BadRequest(new ApiErrorResult<bool>("Dữ liệu không tồn tại"));

            var formRequest = new FormRequest
            {
                Id = id,
                Title = "Cảnh báo",
                Caption = $"Bạn chắc chắn muốn xóa quốc tịch {data.TenQuocTich}",
                Url = "/Danhmuc/Xoaquoctich",
                UrlBack = "/Danhmuc/Quoctichlist",
                IsLoadPage = false
            };
            return PartialView("_ModalConfirm", formRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaquoctich(FormRequest request)
        {
            try
            {
                int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));

                var result = await _quocTichService.Delete(id);

                await Tracking(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Xóa không thành công");
                return Ok(new ApiErrorResult<bool>("Đã có lỗi xảy ra"));
            }
        }
        #endregion

        #region BỘ PHẬN
        public async Task<IActionResult> Bophan()
        {

            ViewData["Title"] = "Bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            int linhvuc = !String.IsNullOrEmpty(Request.Query["linhvuc"]) ? Convert.ToInt32(Request.Query["linhvuc"]) : 0;

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            await OptionLinhVucKinhDoanh(linhvuc);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            var data = await _boPhanService.GetAll(linhvuc, ngonNguId);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Thembophan()
        {

            ViewData["Title"] = "Thêm bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Thembophan(BoPhanCreateRequest request)
        {

            ViewData["Title"] = "Thêm bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _boPhanService.Create(request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });

            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suabophan(string id = "")
        {

            ViewData["Title"] = "Sửa bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _boPhanService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            await OptionLinhVucKinhDoanh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });

            var model = new BoPhanUpdateRequest()
            {
                Id = data.Id,
                LinhVucId = !String.IsNullOrEmpty(data.LinhVucId) ? Array.ConvertAll(data.LinhVucId.Split(','), int.Parse) : null,
                MoTa = data.MoTa,
                TenBoPhan = data.TenBoPhan,
                NgonNguId = data.NgonNguId
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suabophan(string Id, BoPhanUpdateRequest request)
        {

            ViewData["Title"] = "Bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == request.NgonNguId
            });

            request.Id = Convert.ToInt32(HashUtil.DecodeID(Id));

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _boPhanService.Update(request.Id, request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });

            if (!result.IsSuccessed)
                return View(request);
            return RedirectToAction("Bophan", new { NgonNgu = request.NgonNguId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoabophan(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _boPhanService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Bophan");
        }

        [HttpPost]
        public async Task<IActionResult> Capnhat_vitri_bophan(int id, int value)
        {

            var result = await _boPhanService.UpdateViTri(id, value);

            return Ok(result);
        }

        #endregion

        #region DỊCH VỤ
        public async Task<IActionResult> Dichvu()
        {

            ViewData["Title"] = "Dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _dichVuService.GetAll(ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themdichvu()
        {

            ViewData["Title"] = "Thêm dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdichvu(DichVuModel request)
        {
            ViewData["Title"] = "Thêm dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _dichVuService.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return RedirectToAction("Dichvu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suadichvu(string id = "")
        {
            ViewData["Title"] = "Sửa dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _dichVuService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadichvu(string Id, DichVuModel request)
        {

            ViewData["Title"] = "Sửa dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _dichVuService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return RedirectToAction("Dichvu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadichvu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _dichVuService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Dichvu");
        }
        #endregion

        #region LOẠI DỊCH VỤ
        public async Task<IActionResult> Loaidichvu()
        {

            ViewData["Title"] = "Loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _loaiDichVuService.GetAll(ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themloaidichvu()
        {
            ViewData["Title"] = "Thêm loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = SystemConstants.DefaultLanguage == x.Id
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themloaidichvu(LoaiDichVuModel request)
        {
            ViewData["Title"] = "Thêm loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _loaiDichVuService.Create(request);
            if (result.IsSuccessed)
            {
                // HueCIT
                // Thêm mới danh mục cơ sở mua sắm lên database số hóa
                var success = await _danhMucDongBoRepository.AddOrEditCoSoMuaSam(new DanhMucCoSoMuaSamFormData
                {
                    serviceid = SERVICE_ID_CO_SO_MUA_SAM,
                    cosoid = "0",
                    idloaihinh = result.ResultObj.Id.ToString(),
                    tenloaihinh = result.ResultObj.TenLoai
                });

                // Nếu != null: cập nhật [DongBoID] database HueCIT
                if (success > 0)
                {
                    TechLife.Model.HueCIT.LoaiDichVu loai = new TechLife.Model.HueCIT.LoaiDichVu
                    {
                        TenLoai = result.ResultObj.TenLoai,
                        MoTa = result.ResultObj.MoTa,
                        DongBoID = success,
                        NguonDongBo = NGUON_DONG_BO,
                    };

                    // Cập nhật [DongBoID] của DataBase HueCIT = Id của DataBase Số Hóa
                    var res = await _loaiDichVuDongBoService.Update(result.ResultObj.Id, loai);
                    if (res != null)
                    {
                        TempData.AddAlert(new Result<string>()
                        {
                            IsSuccessed = true,
                            Message = "Thêm mới thành công",
                        });
                        return Redirect("/Danhmuc/Loaidichvu");
                    }
                }
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = false,
                Message = "Thêm mới thất bại"
            });

            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Sualoaidichvu(string id = "")
        {
            ViewData["Title"] = "Sửa loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiDichVuService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sualoaidichvu(string Id, LoaiDichVuModel request)
        {

            ViewData["Title"] = "Sửa loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiDichVuService.Update(id, request);
            if (result.IsSuccessed)
            {
                // HueCIT
                // Cập nhật danh mục cơ sở mua sắm lên database số hóa
                var success = await _danhMucDongBoRepository.AddOrEditCoSoMuaSam(new DanhMucCoSoMuaSamFormData
                {
                    serviceid = SERVICE_ID_CO_SO_MUA_SAM,
                    cosoid = request.DongBoID.ToString(),
                    idloaihinh = id.ToString(),
                    tenloaihinh = request.TenLoai
                });

                if (success > 0)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Cập nhật thành công",
                    });
                    return Redirect("/Danhmuc/Loaidichvu");
                }
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = false,
                Message = "Cập nhật thất bại"
            });

            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaloaidichvu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiDichVuService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Loaidichvu");
        }
        #endregion

        #region LOẠI HÌNH KINH DOANH
        public async Task<IActionResult> Loaihinhkinhdoanh()
        {

            ViewData["Title"] = "Loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _loaiHinhService.GetAll(ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themloaihinhkinhdoanh()
        {
            ViewData["Title"] = "Thêm loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themloaihinhkinhdoanh(LoaiHinhModel request)
        {
            ViewData["Title"] = "Thêm loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _loaiHinhService.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return RedirectToAction("Loaihinhkinhdoanh", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Sualoaihinhkinhdoanh(string id = "")
        {
            ViewData["Title"] = "Sửa loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiHinhService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sualoaihinhkinhdoanh(string Id, LoaiHinhModel request)
        {
            ViewData["Title"] = "Sửa loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return RedirectToAction("Loaihinhkinhdoanh", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaloaihinhkinhdoanh(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Loaihinhkinhdoanh");
        }
        #endregion

        #region LOẠI PHÒNG
        public async Task<IActionResult> Loaiphong()
        {

            ViewData["Title"] = "Loại phòng";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _loaiPhongService.GetAll(0);

            return View(data);
        }
        [HttpGet]
        public IActionResult Themloaiphong()
        {
            ViewData["Title"] = "Thêm loại phòng";
            ViewData["Title_parent"] = "Danh mục";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themloaiphong(LoaiPhongModel request)
        {

            ViewData["Title"] = "Thêm loại phòng";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }
            var result = await _loaiPhongApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return Redirect("/Danhmuc/Loaiphong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Sualoaiphong(string id = "")
        {
            ViewData["Title"] = "Sửa loại phòng";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiPhongApiClient.GetById(Id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sualoaiphong(string Id, LoaiPhongModel request)
        {

            ViewData["Title"] = "Sửa loại phòng";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiPhongApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Loaiphong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaloaiphong(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiPhongApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Loaiphong");
        }
        #endregion

        #region NGOẠI NGỮ
        public async Task<IActionResult> Ngoaingu()
        {

            ViewData["Title"] = "Ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _ngoaiNguService.GetAll(ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themngoaingu()
        {
            ViewData["Title"] = "Thêm ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themngoaingu(NgoaiNguModel request)
        {

            ViewData["Title"] = "Thêm ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _ngoaiNguService.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return RedirectToAction("NgoaiNgu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suangoaingu(string id = "")
        {
            ViewData["Title"] = "Sửa ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _ngoaiNguService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suangoaingu(string Id, NgoaiNguModel request)
        {
            ViewData["Title"] = "Sửa ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _ngoaiNguService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return RedirectToAction("NgoaiNgu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoangoaingu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _ngoaiNguService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Ngoaingu");
        }
        #endregion

        #region MỨC ĐỘ THÔNG THẠO NGOẠI NGỮ
        public async Task<IActionResult> Dothanhthaongonngu()
        {
            ViewData["Title"] = "Mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;
            var data = await _mucDoThongThaoNgoaiNguService.GetAll(ngonNguId);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themmucdothongthaongoaingu()
        {

            ViewData["Title"] = "Thêm mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmucdothongthaongoaingu(MucDoThongThaoNgoaiNguModel request)
        {

            ViewData["Title"] = "Thêm mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _mucDoThongThaoNgoaiNguService.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return RedirectToAction("Dothanhthaongonngu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suamucdothongthaongoaingu(string id = "")
        {
            ViewData["Title"] = "Sửa mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _mucDoThongThaoNgoaiNguService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suamucdothongthaongoaingu(string Id, MucDoThongThaoNgoaiNguModel request)
        {

            ViewData["Title"] = "Sửa mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _mucDoThongThaoNgoaiNguService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return RedirectToAction("Dothanhthaongonngu", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoamucdothongthaongoaingu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _mucDoThongThaoNgoaiNguService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Dothanhthaongonngu");
        }
        #endregion

        #region LOẠI HÌNH LAO ĐỘNG
        public async Task<IActionResult> Loaihinhlaodong()
        {

            ViewData["Title"] = "Loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";
            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;
            var data = await _loaiHinhLaoDongService.GetAll(ngonNguId);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themloaihinhlaodong()
        {

            ViewData["Title"] = "Thêm loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themloaihinhlaodong(LoaiHinhLaoDongModel request)
        {

            ViewData["Title"] = "Thêm loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _loaiHinhLaoDongService.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return RedirectToAction("Loaihinhlaodong", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Sualoaihinhlaodong(string id = "")
        {
            ViewData["Title"] = "Sửa loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiHinhLaoDongService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sualoaihinhlaodong(string Id, LoaiHinhLaoDongModel request)
        {

            ViewData["Title"] = "Sửa loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhLaoDongService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return RedirectToAction("Loaihinhlaodong", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaloaihinhlaodong(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhLaoDongService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Loaihinhlaodong");
        }
        #endregion

        #region TÍNH CHẤT LAO ĐỘNG
        public async Task<IActionResult> Tinhchatlaodong()
        {
            ViewData["Title"] = "Tính chất lao động";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _tinhChatLaoDongApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themtinhchatlaodong()
        {
            ViewData["Title"] = "Thêm tính chất lao động";
            ViewData["Title_parent"] = "Danh mục";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themtinhchatlaodong(TinhChatLaoDongModel request)
        {

            ViewData["Title"] = "Thêm tính chất lao động";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _tinhChatLaoDongApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return Redirect("/Danhmuc/Diaphuong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suatinhchatlaodong(string id = "")
        {
            ViewData["Title"] = "Sửa tính chất lao động";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _tinhChatLaoDongApiClient.GetById(Id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suatinhchatlaodong(string Id, TinhChatLaoDongModel request)
        {

            ViewData["Title"] = "Sửa tính chất lao động";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _tinhChatLaoDongApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Tinhchatlaodong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoatinhchatlaodong(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _tinhChatLaoDongApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Tinhchatlaodong");
        }
        #endregion

        #region TRÌNH ĐỘ
        public async Task<IActionResult> Trinhdo()
        {

            ViewData["Title"] = "Trình độ";
            ViewData["Title_parent"] = "Danh mục";
            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;
            var data = await _trinhDoService.GetAll(ngonNguId);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themtrinhdo()
        {

            ViewData["Title"] = "Thêm trình độ";
            ViewData["Title_parent"] = "Danh mục";
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themtrinhdo(TrinhDoModel request)
        {

            ViewData["Title"] = "Thêm trình độ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _trinhDoService.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return RedirectToAction("Trinhdo", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suatrinhdo(string id = "")
        {
            ViewData["Title"] = "Sửa trình độ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _trinhDoService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suatrinhdo(string Id, TrinhDoModel request)
        {

            ViewData["Title"] = "Sửa trình độ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));
            var result = await _trinhDoService.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return RedirectToAction("Trinhdo", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoatrinhdo(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _trinhDoService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Trinhdo");
        }
        #endregion

        public IActionResult Dichvuphong()
        {
            return View();
        }

        #region Danh mục
        public async Task<IActionResult> Cosoluutru()
        {

            ViewData["Title"] = "Cơ sở lưu trú";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);

            return View(data);
        }
        public async Task<IActionResult> Nhahang()
        {

            ViewData["Title"] = "Nhà hàng";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.NhaHang);

            return View(data);
        }
        public async Task<IActionResult> Congtyluhanh()
        {

            ViewData["Title"] = "Công ty lữ hành";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.LuHanh, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        public async Task<IActionResult> Vanchuyen()
        {

            ViewData["Title"] = "Công ty vận chuyển";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.VanChuyen, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }

        public async Task<IActionResult> Diemdulich()
        {

            ViewData["Title"] = "Điểm du lịch";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["ngonNgu"]) ? Request.Query["ngonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.DiemDuLich, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = ngonNguId == x.Id
            });

            return View(data);
        }
        public async Task<IActionResult> Khudulich()
        {

            ViewData["Title"] = "Khu du lịch";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.KhuDuLich, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        public async Task<IActionResult> Khuvcgt()
        {

            ViewData["Title"] = "Khu vui chơi giải trí";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.KhuVuiChoi, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        public async Task<IActionResult> Cssk()
        {

            ViewData["Title"] = "Dịch vụ chăm sóc sức khỏe";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.CSSK, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        public async Task<IActionResult> Thethao()
        {

            ViewData["Title"] = "Dịch vụ thể dục, thể thao";
            ViewData["Title_parent"] = "Danh mục";

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.TheThao, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        public async Task<IActionResult> Cosomuasam()
        {

            ViewData["Title"] = "Cơ sở mua sắm";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.MuaSam);

            return View(data);
        }
        public async Task<IActionResult> Huongdanvien()
        {

            ViewData["Title"] = "Hướng dẫn viên";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.HDV);

            return View(data);
        }
        public async Task<IActionResult> Vesinhcongcong()
        {

            ViewData["Title"] = "Vệ sinh công cộng";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucService.GetAll((int)LinhVucKinhDoanh.VSCC);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themdanhmuc(string act)
        {

            ViewData["Title"] = "Thêm mới danh mục";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.Action = act;

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdanhmuc(string act, DanhMucModel request)
        {

            ViewData["Title"] = "Thêm mới danh mục";
            ViewData["Title_parent"] = "Danh mục";
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            // HueCIT
            // Lĩnh vực kinh doanh
            // ServiceId của số hóa cấp
            switch (act)
            {
                case "cosoluutru":
                    request.LoaiId = (int)LinhVucKinhDoanh.CoSoLuuTru;
                    break;
                case "nhahang":
                    request.LoaiId = (int)LinhVucKinhDoanh.NhaHang;
                    request.ServiceId = SERVICE_ID_NHA_HANG;
                    break;
                case "congtyluhanh":
                    request.LoaiId = (int)LinhVucKinhDoanh.LuHanh;
                    request.ServiceId = SERVICE_ID_LU_HANH;
                    break;
                case "diemdulich":
                    request.LoaiId = (int)LinhVucKinhDoanh.DiemDuLich;
                    request.ServiceId = SERVICE_ID_DIEM_DU_LICH;
                    break;
                case "huongdanvien":
                    request.LoaiId = (int)LinhVucKinhDoanh.HDV;
                    break;
                case "cosomuasam":
                    request.LoaiId = (int)LinhVucKinhDoanh.MuaSam;
                    request.ServiceId = SERVICE_ID_CO_SO_MUA_SAM;
                    break;
                case "khudulich":
                    request.LoaiId = (int)LinhVucKinhDoanh.KhuDuLich;
                    break;
                case "Khuvcgt":
                    request.LoaiId = (int)LinhVucKinhDoanh.KhuVuiChoi;
                    request.ServiceId = SERVICE_ID_KHU_VUI_CHOI;
                    break;
                case "Thethao":
                    request.LoaiId = (int)LinhVucKinhDoanh.TheThao;
                    request.ServiceId = SERVICE_ID_THE_THAO;
                    break;
                case "Cssk":
                    request.LoaiId = (int)LinhVucKinhDoanh.CSSK;
                    request.ServiceId = SERVICE_ID_CSSK;
                    break;
                case "Vanchuyen":
                    request.LoaiId = (int)LinhVucKinhDoanh.VanChuyen;
                    request.ServiceId = SERVICE_ID_VAN_CHUYEN;
                    break;
                default:
                    request.LoaiId = (int)LinhVucKinhDoanh.VSCC;
                    break;
            }

            // HueCIT
            // True: Thêm mới tại database HueCIT & Số hóa
            // False: Thêm mới tại database
            if (act == "nhahang" || act == "congtyluhanh" || act == "diemdulich" || act == "cosomuasam" || act == "Khuvcgt" || act == "Thethao" || act == "Cssk" || act == "Vanchuyen")
            {
                // Thêm mới tại database HueCIT
                var result = await _danhMucService.Create(request);

                if (result.IsSuccessed)
                {
                    // Thêm mới vào database số hóa
                    int success = -1;

                    if (act == "nhahang" || act == "Vanchuyen" || act == "Khuvcgt" || act == "Thethao" || act == "Cssk")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                        {
                            serviceid = request.ServiceId,
                            eformid = "0",
                            idloaihinh = result.ResultObj.Id.ToString(),
                            tenloaihinh = result.ResultObj.Ten
                        });
                    }
                    else if (act == "congtyluhanh")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditLuHanh(new DanhMucLuHanhFormData
                        {
                            serviceid = request.ServiceId,
                            phanloaiid = "0",
                            idloaihinh = result.ResultObj.Id.ToString(),
                            tenloaihinh = result.ResultObj.Ten
                        });
                    }
                    else if (act == "diemdulich")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditDiemDuLich(new DanhMucDiemDuLichFormData
                        {
                            serviceid = request.ServiceId,
                            diemdlid = "0",
                            idloaihinh = result.ResultObj.Id.ToString(),
                            tenloaihinh = result.ResultObj.Ten
                        });
                    }

                    // Kiểm tra thêm mới database số hóa có thành công
                    if (success > 0)
                    {
                        // Cập nhập [DongBoID] database HueCIT
                        await _danhMucDongBoService.UpdateDongBoID(result.ResultObj.Id, success);

                        TempData.AddAlert(new Result<string>()
                        {
                            IsSuccessed = result.IsSuccessed,
                            Message = "Thêm mới thành công",
                        });

                        return Redirect("/Danhmuc/" + act);
                    }
                }
            }
            else
            {
                // Thêm mới tại database HueCIT
                var result = await _danhMucService.Create(request);
                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });

                    return Redirect("/Danhmuc/" + act);
                }
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = false,
                Message = "Thêm mới thất bại"
            });

            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Suadanhmuc(string act, string id = "")
        {
            ViewData["Title"] = "Sửa danh mục";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _danhMucService.GetById(Id);
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            ViewBag.Action = act;
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadanhmuc(string act, string Id, DanhMucModel request)
        {
            ViewData["Title"] = "Sửa danh mục";
            ViewData["Title_parent"] = "Danh mục";
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            // HueCIT
            // True: Thêm mới tại database HueCIT & Số hóa
            // False: Thêm mới tại database
            if (act == "nhahang" || act == "congtyluhanh" || act == "diemdulich" || act == "cosomuasam" || act == "Khuvcgt" || act == "Thethao" || act == "Cssk" || act == "Vanchuyen")
            {
                // Cập nhật danh mục database HueCIT
                var result = await _danhMucService.Update(id, request);
                if (result.IsSuccessed)
                {
                    // HueCIT
                    // Update database số hóa
                    switch (act)
                    {
                        case "congtyluhanh":
                            request.ServiceId = SERVICE_ID_LU_HANH;
                            break;
                        case "diemdulich":
                            request.ServiceId = SERVICE_ID_DIEM_DU_LICH;
                            break;
                        case "cosomuasam":
                            request.ServiceId = SERVICE_ID_CO_SO_MUA_SAM;
                            break;
                        case "Khuvcgt":
                            request.ServiceId = SERVICE_ID_KHU_VUI_CHOI;
                            break;
                        case "Thethao":
                            request.ServiceId = SERVICE_ID_THE_THAO;
                            break;
                        case "Cssk":
                            request.ServiceId = SERVICE_ID_CSSK;
                            break;
                        case "Vanchuyen":
                            request.ServiceId = SERVICE_ID_VAN_CHUYEN;
                            break;
                        default:
                            break;
                    }


                    int success = -1;
                    if (act == "Vanchuyen" || act == "Khuvcgt" || act == "Thethao" || act == "Cssk")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                        {
                            serviceid = request.ServiceId,
                            eformid = request.DongBoID.ToString(),
                            idloaihinh = id.ToString(),
                            tenloaihinh = request.Ten
                        });
                    }
                    else if (act == "congtyluhanh")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditLuHanh(new DanhMucLuHanhFormData
                        {
                            serviceid = request.ServiceId,
                            phanloaiid = request.DongBoID.ToString(),
                            idloaihinh = id.ToString(),
                            tenloaihinh = request.Ten
                        });
                    }
                    else if (act == "diemdulich")
                    {
                        success = await _danhMucDongBoRepository.AddOrEditDiemDuLich(new DanhMucDiemDuLichFormData
                        {
                            serviceid = request.ServiceId,
                            diemdlid = request.DongBoID.ToString(),
                            idloaihinh = id.ToString(),
                            tenloaihinh = request.Ten
                        });
                    }

                    if (success > 0)
                    {
                        TempData.AddAlert(new Result<string>()
                        {
                            IsSuccessed = result.IsSuccessed,
                            Message = "Cập nhật thành công",
                        });

                        return Redirect("/DanhMuc/" + act);
                    }
                }
            }
            else
            {
                // Cập nhật danh mục database HueCIT
                var result = await _danhMucService.Update(id, request);
                if (result != null)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Cập nhật thành công",
                    });

                    return Redirect("/DanhMuc/" + act);
                }
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = false,
                Message = "Cập nhật thất bại"
            });

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadanhmuc(string Id, string act)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _danhMucService.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });
                return Redirect("/Danhmuc/" + act);
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/" + act);
        }
        #endregion

        public async Task<IActionResult> Tiennghi()
        {

            ViewData["Title"] = "Tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            int linhvuc = !String.IsNullOrEmpty(Request.Query["linhvuc"]) ? Convert.ToInt32(Request.Query["linhvuc"]) : 0;
            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            await OptionLinhVucKinhDoanh(linhvuc);
            var data = await _tienNghiService.GetAll(linhvuc, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themtiennghi()
        {

            ViewData["Title"] = "Thêm tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themtiennghi(TienNghiCreateRequest request)
        {

            ViewData["Title"] = "Thêm tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _tienNghiService.Create(request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            if (!result.IsSuccessed)
                return View(request);

            return RedirectToAction("Themtiennghi");
        }

        [HttpGet]
        public async Task<IActionResult> Suatiennghi(string id = "")
        {

            ViewData["Title"] = "Sửa tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _tienNghiService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });

            var model = new TienNghiUpdateRequest()
            {
                DonViTinhId = data.DonViTinhId,
                Id = data.Id,
                LinhVucId = !String.IsNullOrEmpty(data.LinhVucId) ? Array.ConvertAll(data.LinhVucId.Split(','), int.Parse) : null,
                MoTa = data.MoTa,
                Ten = data.Ten,
                NgonNguId = data.NgonNguId,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suatiennghi(string Id, TienNghiUpdateRequest request)
        {

            ViewData["Title"] = "Sửa tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            request.Id = Convert.ToInt32(HashUtil.DecodeID(Id));

            ModelState.Remove("Id");

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }
            var result = await _tienNghiService.Update(request.Id, request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return View(request);

        }
        [HttpPost]
        public async Task<IActionResult> Capnhat_vitri_tiennghi(int id, int value)
        {

            var result = await _tienNghiService.UpdateViTri(id, value);

            return Ok(result);
        }

        public async Task<IActionResult> Loaigiuong()
        {

            ViewData["Title"] = "Loại giường";
            ViewData["Title_parent"] = "Danh mục";


            var data = await _loaiGiuongApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themloaigiuong()
        {

            ViewData["Title"] = "Thêm loại giường";
            ViewData["Title_parent"] = "Danh mục";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themloaigiuong(LoaiGiuongModel request)
        {

            ViewData["Title"] = "Thêm loại giường";
            ViewData["Title_parent"] = "Danh mục";

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _loaiGiuongApiClient.Create(request);

            if (result != null)
            {
                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });

                    return Redirect("/Danhmuc/Loaigiuong");
                }
                else
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = result.Message
                    });
                }
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã có lỗi xãy ra trong quá trình xử lý"
                });
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Sualoaigiuong(string id = "")
        {

            ViewData["Title"] = "Sửa loại giường";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _loaiGiuongApiClient.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sualoaigiuong(string Id, LoaiGiuongModel request)
        {

            ViewData["Title"] = "Sửa loại giường";
            ViewData["Title_parent"] = "Danh mục";

            request.Id = Convert.ToInt32(HashUtil.DecodeID(Id));

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _loaiGiuongApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Loaigiuong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return View(request);
        }

        public async Task<IActionResult> Donvitinh()
        {

            ViewData["Title"] = "Đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";
            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            var data = await _donViTinhService.GetAll(ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themdvt()
        {

            ViewData["Title"] = "Thêm đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdvt(DonViTinhModel request)
        {

            ViewData["Title"] = "Thêm đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _donViTinhService.Create(request);

            if (result != null)
            {
                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });

                    return RedirectToAction("Donvitinh", new { NgonNgu = request.NgonNguId });
                }
                else
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = result.Message
                    });

                    ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                    {
                        Text = x.Ten,
                        Value = x.Id,
                        Selected = x.Id == request.NgonNguId
                    });
                }
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã có lỗi xãy ra trong quá trình xử lý"
                });

                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Suadvt(string id = "")
        {
            ViewData["Title"] = "Sửa đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _donViTinhService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == data.NgonNguId
            });
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadvt(string Id, DonViTinhModel request)
        {
            ViewData["Title"] = "Sửa đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

            request.Id = Convert.ToInt32(HashUtil.DecodeID(Id));

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _donViTinhService.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return RedirectToAction("Donvitinh", new { NgonNgu = request.NgonNguId });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
                ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Ten,
                    Value = x.Id,
                    Selected = x.Id == request.NgonNguId
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoatiennghi(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _tienNghiApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Xóa thành công",
                });

            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                });
            }
            return Redirect("/Danhmuc/Quoctich");
        }

        public async Task<IActionResult> Giayphep()
        {


            ViewData["Title"] = "Loại văn bản liên quan";
            ViewData["Title_parent"] = "Danh mục";

            var pageRequest = new GetPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
            };

            string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

            int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;

            var data = await _giayPhepService.GetPaging(loaihinh, pageRequest, ngonNguId);

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == ngonNguId
            });

            await OptionLinhVucKinhDoanh(loaihinh);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themloaigiayphep()
        {
            ViewData["Title"] = "Thêm loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            await OptionLinhVucKinhDoanh();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Themloaigiayphep(GiayPhepCreateRequest request, string type_submit)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Thêm loại giấy phép chứng chỉ";
                ViewData["Title_parent"] = "Danh mục";

                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _giayPhepService.Create(request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });

            if (result.IsSuccessed)
            {
                if (type_submit == "save")
                {
                    return RedirectToAction("Giayphep", new { NgonNgu = request.NgonNguId });
                }
                else
                {
                    return RedirectToAction("Themloaigiayphep");
                }
            }

            ViewData["Title"] = "Thêm loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = false,
                Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
            });

            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = x.Id == SystemConstants.DefaultLanguage
            });

            await OptionLinhVucKinhDoanh();

            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Sualoaigiayphep(string id)
        {

            ViewData["Title"] = "Sửa loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

            var data = await _giayPhepService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));
            var model = new GiayPhepUpdateRequest()
            {
                Id = data.Id,
                LoaiHinhId = !String.IsNullOrEmpty(data.LinhVucId) ? Array.ConvertAll(data.LinhVucId.Split(','), int.Parse) : null,
                Ten = data.Ten,
                NgonNguId = data.NgonNguId
            };
            ViewBag.NgonNguOptions = (await _ngonNguService.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id,
                Selected = data.NgonNguId == x.Id
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Sualoaigiayphep(GiayPhepUpdateRequest request, string type_submit)
        {

            ViewData["Title"] = "Sửa loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _giayPhepService.Update(request.Id, request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });

            if (result.IsSuccessed)
            {
                if (type_submit == "save")
                {
                    return RedirectToAction("Giayphep", new { NgonNgu = request.NgonNguId });
                }
                else
                {
                    return RedirectToAction("Themloaigiayphep");
                }
            }
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Xoaloaigiayphep(string id)
        {

            ViewData["Title"] = "Sửa loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";


            var result = await _giayPhepService.Delete(Convert.ToInt32(HashUtil.DecodeID(id)));


            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });


            return Redirect(Request.GetBackUrl());
        }

        #region ĐỒNG BỘ
        public async Task<IActionResult> DongBoLoaiDichVu()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucCoSoMuaSam();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục cơ sở mua sắm thành công" });
                return Redirect("/DanhMuc/LoaiDichVu");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/LoaiDichVu");
            }
        }

        public async Task<IActionResult> DongBoDanhMucDiemDuLich()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucDiemDuLich();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục điểm du lịch thành công" });
                return Redirect("/DanhMuc/Diemdulich");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Diemdulich");
            }
        }

        public async Task<IActionResult> DongBoDanhMucLuHanh()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucLuHanh();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục công ty lữ hành thành công" });
                return Redirect("/DanhMuc/Congtyluhanh");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Congtyluhanh");
            }
        }

        public async Task<IActionResult> DongBoDanhMucVanChuyen()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucVanChuyen();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục công ty vận chuyển thành công" });
                return Redirect("/DanhMuc/Vanchuyen");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Vanchuyen");
            }
        }

        public async Task<IActionResult> DongBoDanhMucKhuVuiChoi()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucKhuVuiChoi();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục khu vui chơi giải trí thành công" });
                return Redirect("/DanhMuc/Khuvcgt");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Khuvcgt");
            }
        }

        public async Task<IActionResult> DongBoDanhMucTheThao()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucTheThao();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục thể thao thành công" });
                return Redirect("/DanhMuc/Thethao");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Thethao");
            }
        }

        public async Task<IActionResult> DongBoDanhMucCssk()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhCSSK();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục chăm sóc sức khỏe thành công" });
                return Redirect("/DanhMuc/Cssk");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/DanhMuc/Cssk");
            }
        }


        #endregion

        #region Dữ liệu thống kê
        [HttpGet]
        public IActionResult Dulieuthongke()
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

        public async Task<IActionResult> Dulieuthongkelist(GetPagingFormRequest request)
        {
            try
            {
                ViewData["Title"] = "Danh mục";
                request.PageIndex = !string.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex;
                request.PageSize = !string.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize;

                var data = await _danhMucDuLieuThongKeService.GetPaging(request);

                return PartialView(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xem danh sách");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Themdulieuthongke()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdulieuthongke(DanhMucDuLieuThongKeCreateRequest request)
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
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Suadulieuthongke(string id)
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
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadulieuthongke(DanhMucDuLieuThongKeUpdateRequest request)
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
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpGet]
        public IActionResult Xoadulieuthongke(string id)
        {
            var model = new FormRequest()
            {
                Title = "Cảnh báo!",
                Caption = "Bạn chắc chắn muốn xóa danh mục này",
                Id = id,
                IsLoadPage = false,
                Url = "/Danhmuc/Xoadulieuthongke",
                UrlBack = "/Danhmuc/Dulieuthongkelist",
                Note = "(Lưu ý sẽ xóa toàn bộ danh mục con đi kèm)"
            };

            return PartialView("_ModalConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadulieuthongke(FormRequest request)
        {
            try
            {
                int id = Convert.ToInt32(HashUtil.DecodeID(request.Id));
                var result = await _danhMucDuLieuThongKeService.Delete(id);
                await Tracking(result.Message);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xóa danh mục");
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDuLieuThongKe(string id, int value)
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
                return StatusCode(500, "Đã có lỗi xảy ra");
            }
        }
        #endregion
    }
}
