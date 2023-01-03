using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Jobs;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Areas.HueCIT.Repository.Schedules;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    [Authorize(Roles = "root")]
    public class DanhMucController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IGiayPhepService _giayPhepService;
        private readonly ITienNghiService _tienNghiService;
        private readonly IBoPhanService _boPhanService;
        private readonly ILoaiPhongService _loaiPhongService;

        private readonly IDanhMucRepository _respository;
        private readonly IDanhMucDongBoRepository _danhMucDongBoRepository;
        private readonly IPhanAnhHienTruongLinhVucRepository _phanAnhHienTruongLinhVucRepository;
        private readonly IVeDiTichLoaiRepository _veDiTichLoaiRepository;
        private readonly IPhanAnhHienTruongScheduleRepository _phanAnhHienTruongScheduleRepository;
        private readonly IDanhMucScheduleRepository _danhMucScheduleRepository;

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public IScheduler Scheduler { get; set; }

        private readonly int _nguondongbo = (int)NguonDongBo.SoHoa;
        private readonly string serviceid_diadiemanuong = "4BTYSbqakdg1A7fWr9qa/Q==";
        private readonly string serviceid_amthuc = "7NL8tqxUups3P0nh9xI2+w==";
        private readonly string serviceid_lehoi = "Trgt9xxv4dco836j35ROgQ==";
        private readonly string serviceid_diemgiaodich = "NS5lpcoNzxyB2RLqPsVacw==";

        public DanhMucController(IUserService userService
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
            , IDanhMucRepository respository
            , IDanhMucDongBoRepository danhMucDongBoRepository
            , IPhanAnhHienTruongLinhVucRepository phanAnhHienTruongLinhVucRepository
            , IVeDiTichLoaiRepository veDiTichLoaiRepository
            , ISchedulerFactory schedulerFactory
            , IJobFactory jobFactory
            , IPhanAnhHienTruongScheduleRepository phanAnhHienTruongScheduleRepository
            , IDanhMucScheduleRepository danhMucScheduleRepository)
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
            _respository = respository;
            _danhMucDongBoRepository = danhMucDongBoRepository;
            _phanAnhHienTruongLinhVucRepository = phanAnhHienTruongLinhVucRepository;
            _veDiTichLoaiRepository = veDiTichLoaiRepository;
            _phanAnhHienTruongLinhVucRepository = phanAnhHienTruongLinhVucRepository;
            _phanAnhHienTruongScheduleRepository = phanAnhHienTruongScheduleRepository;
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _danhMucScheduleRepository = danhMucScheduleRepository;
        }

        #region LOẠI ĐỊA ĐIỂM ĂN UỐNG
        public async Task<IActionResult> LoaiDiaDiemAnUong(int? trang)
        {
            ViewData["Title"] = "Loại địa điểm ăn uống";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<LoaiDiaDiemAnUong> obj = new List<LoaiDiaDiemAnUong>();
            obj = (await _respository.GetsLoaiDiaDiemAnUong()).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public IActionResult ThemLoaiDiaDiemAnUong()
        {
            ViewData["Title"] = "Thêm loại địa điểm ăn uống";

            return View();
        }

        public async Task<IActionResult> SuaLoaiDiaDiemAnUong(int id)
        {
            ViewData["Title"] = "Sửa loại địa điểm ăn uống";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
            }
            LoaiDiaDiemAnUong obj = new LoaiDiaDiemAnUong();
            obj = (await _respository.GetLoaiDiaDiemAnUong(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiDiaDiemAnUong(LoaiDiaDiemAnUong request)
        {
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

            var result = await _respository.InsertLoaiDiaDiemAnUong(request);
            if (result != null)
            {
                // HueCIT
                // Thêm mới vào database số hóa
                var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                {
                    serviceid = serviceid_diadiemanuong,
                    eformid = "0",
                    idloaihinh = result.Id.ToString(),
                    tenloaihinh = result.TenLoai
                });

                if (success > 0) 
                { 
                    // Cập nhật Id đồng bộ vào danh mục
                    await _respository.UpdateLoaiDiaDiemAnUongDongBoID(result.Id, success);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Thêm mới thành công",
                    });
                }

                return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiDiaDiemAnUong(LoaiDiaDiemAnUong request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _respository.UpdateLoaiDiaDiemAnUong(request);
            if (result != null)
            {
                // HueCIT
                // Create database số hóa
                var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                {
                    serviceid = serviceid_diadiemanuong,
                    eformid = result.DongBoID.ToString(),
                    idloaihinh = result.Id.ToString(),
                    tenloaihinh = result.TenLoai,
                });

                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaLoaiDiaDiemAnUong(int id)
        {
            var result = await _respository.DeleteLoaiDiaDiemAnUong(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
        }
        #endregion

        #region LOẠI ẨM THỰC ĐỊA ĐIỂM ĂN UỐNG
        public async Task<IActionResult> LoaiAmThucDiaDiemAnUong(int? trang)
        {
            ViewData["Title"] = "Loại ẩm thực địa điểm ăn uống";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<LoaiAmThucDiaDiemAnUong> obj = new List<LoaiAmThucDiaDiemAnUong>();
            obj = (await _respository.GetsLoaiAmThucDiaDiemAnUong()).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public IActionResult ThemLoaiAmThucDiaDiemAnUong()
        {
            ViewData["Title"] = "Thêm loại ẩm thực địa điểm ăn uống";

            return View();
        }

        public async Task<IActionResult> SuaLoaiAmThucDiaDiemAnUong(int id)
        {
            ViewData["Title"] = "Sửa loại ẩm thực địa điểm ăn uống";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiAmThucDiaDiemAnUong");
            }
            LoaiAmThucDiaDiemAnUong obj = new LoaiAmThucDiaDiemAnUong();
            obj = (await _respository.GetLoaiAmThucDiaDiemAnUong(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong request)
        {
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

            var result = await _respository.InsertLoaiAmThucDiaDiemAnUong(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/HueCIT/DanhMuc/LoaiAmThucDiaDiemAnUong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _respository.UpdateLoaiAmThucDiaDiemAnUong(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/HueCIT/DanhMuc/LoaiAmThucDiaDiemAnUong");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaLoaiAmThucDiaDiemAnUong(int id)
        {
            var result = await _respository.DeleteLoaiAmThucDiaDiemAnUong(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return Redirect("/HueCIT/DanhMuc/LoaiAmThucDiaDiemAnUong");
        }
        #endregion

        #region LOẠI ẨM THỰC
        public async Task<IActionResult> LoaiAmThuc(int? trang)
        {
            ViewData["Title"] = "Loại ẩm thực";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<LoaiAmThuc> obj = new List<LoaiAmThuc>();
            obj = (await _respository.GetsLoaiAmThuc()).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }
        public IActionResult ThemLoaiAmThuc()
        {
            ViewData["Title"] = "Thêm loại ẩm thực";

            return View();
        }

        public async Task<IActionResult> SuaLoaiAmThuc(int id)
        {
            ViewData["Title"] = "Sửa loại ẩm thực";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
            }
            LoaiAmThuc obj = new LoaiAmThuc();
            obj = (await _respository.GetLoaiAmThuc(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiAmThuc(LoaiAmThuc request)
        {
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

            var result = await _respository.InsertLoaiAmThuc(request);
            if (result != null)
            {
                var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                {
                    serviceid = serviceid_amthuc,
                    eformid = "0",
                    idloaihinh = result.ID.ToString(),
                    tenloaihinh = result.TenLoai,
                });

                if (success == null)
                {
                    await _respository.DeleteLoaiAmThuc(result.ID);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Thêm mới số hóa thất bại!",
                    });
                }
                else
                {
                    await _respository.EditDongBoID(result.ID, success);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Thêm mới thành công",
                    });
                }
                return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiAmThuc(LoaiAmThuc request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var loaicu = await _respository.GetLoaiAmThuc(request.ID);

            var result = await _respository.UpdateLoaiAmThuc(request);
            if (result != null)
            {
                int? success;
                if (result.DongBoID > 0)
                {
                    success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                    {
                        serviceid = serviceid_amthuc,
                        eformid = result.DongBoID.ToString(),
                        idloaihinh = result.ID.ToString(),
                        tenloaihinh = result.TenLoai,
                    });
                }
                else
                {
                    success = -1;
                }

                if (success <= 0)
                {
                    await _respository.UpdateLoaiAmThuc(loaicu);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Cập nhật số hóa thất bại",
                    });
                }
                else
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Cập nhật thành công",
                    });

                }

                return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaLoaiAmThuc(int id)
        {
            var result = await _respository.DeleteLoaiAmThuc(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
        }
        #endregion

        #region CHỦ ĐỀ SỰ KIỆN
        public async Task<IActionResult> ChuDeSuKien(int? trang)
        {
            ViewData["Title"] = "Chủ đề sự kiện";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<ChuDeSuKien> obj = new List<ChuDeSuKien>();
            obj = (await _respository.GetsChuDeSuKien()).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }
        public IActionResult ThemChuDeSuKien()
        {
            ViewData["Title"] = "Thêm chủ đề sự kiện";

            return View();
        }

        public async Task<IActionResult> SuaChuDeSuKien(int id)
        {
            ViewData["Title"] = "Sửa chủ đề sự kiện";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/ChuDeSuKien");
            }
            ChuDeSuKien obj = new ChuDeSuKien();
            obj = (await _respository.GetChuDeSuKien(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemChuDeSuKien(ChuDeSuKien request)
        {
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

            var result = await _respository.InsertChuDeSuKien(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/HueCIT/DanhMuc/ChuDeSuKien");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaChuDeSuKien(ChuDeSuKien request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _respository.UpdateChuDeSuKien(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/HueCIT/DanhMuc/ChuDeSuKien");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaChuDeSuKien(int id)
        {
            var result = await _respository.DeleteChuDeSuKien(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return Redirect("/HueCIT/DanhMuc/ChuDeSuKien");
        }
        #endregion

        #region DANH MỤC - DI SẢN VĂN HÓA
        public async Task<IActionResult> LoaiDiSan(int? trang)
        {
            ViewData["Title"] = "Loại di sản văn hóa";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<DanhMuc> obj = new List<DanhMuc>();
            obj = (await _respository.GetsDanhMuc(Convert.ToInt32(LinhVucKinhDoanh.DiSanVanHoa))).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public IActionResult ThemLoaiDiSan()
        {
            ViewData["Title"] = "Thêm loại di sản văn hóa";

            return View();
        }

        public async Task<IActionResult> SuaLoaiDiSan(int id)
        {
            ViewData["Title"] = "Sửa loại di sản văn hóa";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiDiSan");
            }
            DanhMuc obj = new DanhMuc();
            obj = (await _respository.GetDanhMuc(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiDiSan(DanhMuc request)
        {
            ModelState.Remove("Id");

            request.LoaiId = Convert.ToInt32(LinhVucKinhDoanh.DiSanVanHoa);

            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _respository.InsertDanhMuc(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/HueCIT/DanhMuc/LoaiDiSan");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiDiSan(DanhMuc request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var result = await _respository.UpdateDanhMuc(request);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/HueCIT/DanhMuc/LoaiDiSan");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaLoaiDiSan(int id)
        {
            var result = await _respository.DeleteDanhMuc(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return Redirect("/HueCIT/DanhMuc/LoaiDiSan");
        }
        #endregion

        #region LĨNH VỰC HIỆN TRƯỜNG
        public async Task<IActionResult> LinhVucHienTruong(int? trang)
        {
            ViewData["Title"] = "Lĩnh vực phản ánh hiện trường";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<PhanAnhHienTruongLinhVuc> obj = new List<PhanAnhHienTruongLinhVuc>();
            obj = (await _phanAnhHienTruongLinhVucRepository.GetsLinhVucPhanAnhHienTruong()).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        [HttpPost]
        public async Task<ApiSuccessResult<bool>> LinhVucHienTruongUpdateIsEnable(int id, bool isEnable)
        {
            // Đồng bộ dữ liệu phản ánh hiện trường theo lĩnh vực 
            try
            {
                // Tìm kiếm phản ánh theo lĩnh vực trên database HueCIT
                var linhvuc = await _phanAnhHienTruongLinhVucRepository.GetLinhVucPhanAnhHienTruong(id);
                if (linhvuc != null)
                {
                    // Khởi tạo
                    Scheduler = await _schedulerFactory.GetScheduler();
                    Scheduler.JobFactory = _jobFactory;
                    await Scheduler.Start();

                    var jobkey = new JobKey("HienTruongByLinhVuc_Job", "LinhVuc_" + id);
                    var triggerkey = new TriggerKey("HienTruongByLinhVuc_Trigger", "LinhVuc_" + id);

                    // Kiểm tra jobkey có đang chạy không ?
                    // Nếu có : đợi chạy xong job trước mới tiếp tục được
                    var x = (await Scheduler.GetCurrentlyExecutingJobs()).Where(x => x.JobDetail.Key.Group == "LinhVuc_" + id).Select(x => x.JobDetail).ToList();
                    if (x.Any())
                    {
                        return new ApiSuccessResult<bool> { IsSuccessed = false, Message = "Phản ánh hiện trường đang được đồng bộ! Vui lòng thử lại sau!" };
                    }

                    // Kiểm tra job key có tồn tại
                    // Nếu có : Xóa job và trigger 
                    bool isJob = await Scheduler.CheckExists(jobkey);
                    if (isJob)
                    {
                        await Scheduler.DeleteJob(jobkey);
                        await Scheduler.UnscheduleJob(triggerkey);
                    }

                    // Tạo job
                    IJobDetail job = JobBuilder.Create(typeof(HienTruongTheoLinhVucJob))
                        .WithIdentity("HienTruongByLinhVuc_Job", "LinhVuc_" + id)
                        .UsingJobData("id", id)
                        .UsingJobData("isEnableUpdate", isEnable)
                        .WithDescription("Hiện trường theo lĩnh vực - JOB")
                        .Build();

                    // Tạo trigger
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("HienTruongByLinhVuc_Trigger", "LinhVuc_" + id)
                        .WithDescription("Hiện trường theo lĩnh vực - TRIGGER")
                        .StartAt(DateTime.Now.AddMinutes(30))
                        .Build();
                    await Scheduler.ScheduleJob(job, trigger);

                    // Cập nhật trường [isEnable] của lĩnh vực (phản ánh hiện trường)
                    await _phanAnhHienTruongLinhVucRepository.UpdateIsEnableLinhVucPhanAnhHienTruong(id, isEnable);

                    return new ApiSuccessResult<bool> { IsSuccessed = true, Message = "Cập nhật hoạt động thành công!" };
                }
                else
                {
                    return new ApiSuccessResult<bool> { IsSuccessed = false, Message = "Không tìm thấy lĩnh vực ID = " + id };
                }
            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<bool> { IsSuccessed = false, Message = "Cập nhật hoạt động thất bại!" };
            }
        }
        #endregion

        #region LOẠI VÉ DI TÍCH
        public async Task<IActionResult> LoaiVeDiTich(int? trang)
        {
            ViewData["Title"] = "Loại vé điện tử tham quan di tích";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loaikhach = !String.IsNullOrEmpty(Request.Query["loaikhach"]) ? Convert.ToInt32(Request.Query["loaikhach"]) : -1;
            int loaive = !String.IsNullOrEmpty(Request.Query["loaive"]) ? Convert.ToInt32(Request.Query["loaive"]) : -1;

            ViewBag.LoaiKhach = loaikhach;
            ViewBag.LoaiVe = loaive; ;

            VeDiTichLoaiRequest request = new VeDiTichLoaiRequest
            {
                LoaiDoiTuong = loaikhach,
                LoaiVe = loaive
            };

            await OptionLoaiKhach(loaikhach);
            await OptionLoaiVe(loaive);

            List<VeDiTichLoaiTrinhDien> obj = (await _veDiTichLoaiRepository.GetsTrinhDien(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        private async Task OptionLoaiKhach(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LoaiKhachVeDiTich)).Cast<LoaiKhachVeDiTich>()
                    .Select(x => new SelectListItem()
                    {
                        Text = StringEnum.GetStringValue(x),
                        Value = Convert.ToInt32(x).ToString(),
                        Selected = (int)x == seletedId ? true : false
                    });
                ViewBag.listloaikhach = list.OrderBy(v => v.Value);
            });
        }
        private async Task OptionLoaiVe(int seletedId = 0)
        {
            var list = (await _veDiTichLoaiRepository.Gets()).ToList();
            var obj = list.GroupBy(x => new { x.LoaiVeId, x.TenLoai }).Select(x => new SelectListItem
            {
                Text = x.Key.TenLoai,
                Value = x.Key.LoaiVeId.ToString(),
                Selected = x.Key.LoaiVeId == seletedId ? true : false
            });
            ViewBag.listloaive = obj;
        }
        #endregion

        #region LOẠI LỄ HỘI
        public async Task<IActionResult> LoaiLeHoi(int? trang)
        {
            ViewData["Title"] = "Loại lễ hội";

            if (trang == null) trang = 1;
            int pageNumber = trang ?? 1;

            List<LoaiLeHoiTrinhDien> list = new List<LoaiLeHoiTrinhDien>();
            list = (await _respository.GetsLoaiLeHoi()).ToList();

            return View(list.ToPagedList(pageNumber, _pageSize));
        }

        public IActionResult ThemLoaiLeHoi()
        {
            ViewData["Title"] = "Thêm loại lễ hội";

            return View();
        }

        public async Task<IActionResult> SuaLoaiLeHoi(int id)
        {
            ViewData["Title"] = "Sửa loại lễ hội";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
            }
            LoaiLeHoiTrinhDien obj = new LoaiLeHoiTrinhDien();
            obj = (await _respository.GetLoaiLeHoi(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiLeHoi(LoaiLeHoiModel request)
        {
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

            var result = await _respository.AddLoaiLeHoi(request);
            if (result != null)
            {
                var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                {
                    serviceid = serviceid_lehoi,
                    eformid = "0",
                    idloaihinh = result.Id.ToString(),
                    tenloaihinh = result.Ten,
                });

                if (success == null)
                {
                    await _respository.DeleteLoaiLeHoi(result.Id);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Thêm mới số hóa thất bại!",
                    });
                }
                else
                {
                    await _respository.EditLoaiLeHoi(new LoaiLeHoiModel
                    {
                        Id = result.Id,
                        Ten = result.Ten,
                        DongBoID = success,
                        NguonDongBo = _nguondongbo,
                    });
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Thêm mới thành công",
                    });
                }
                return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiLeHoi(LoaiLeHoiModel request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var loaicu = await _respository.GetLoaiLeHoi(request.Id);

            var result = await _respository.EditLoaiLeHoi(request);

            if (result != null)
            {
                int? success;
                if (result.DongBoID > 0)
                {
                    success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                    {
                        serviceid = serviceid_lehoi,
                        eformid = result.DongBoID.ToString(),
                        idloaihinh = result.Id.ToString(),
                        tenloaihinh = result.Ten,
                    });
                }
                else
                {
                    success = -1;
                }

                if (success <= 0)
                {
                    await _respository.EditLoaiLeHoi(new LoaiLeHoiModel
                    {
                        Id = loaicu.Id,
                        Ten = loaicu.Ten,
                        DongBoID = loaicu.DongBoID,
                        NguonDongBo = loaicu.NguonDongBo,
                        IsDelete = loaicu.IsDelete,
                        IsStatus = loaicu.IsStatus,
                    });
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Cập nhật số hóa thất bại",
                    });
                }
                else
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Cập nhật thành công",
                    });

                }

                return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        public async Task<IActionResult> XoaLoaiLeHoi(int id)
        {
            var result = await _respository.DeleteLoaiLeHoi(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }

            return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
        }

        #endregion

        #region  LOẠI ĐIỂM GIAO DỊCH
        public async Task<IActionResult> LoaiDiemGiaoDich(int? trang)
        {
            ViewData["Title"] = "Loại điểm giao dịch";

            if (trang == null) trang = 1;
            int pageNumber = trang ?? 1;

            List<LoaiDiemGiaoDichTrinhDien> list = new List<LoaiDiemGiaoDichTrinhDien>();
            list = (await _respository.GetsLoaiDiemGiaoDich()).ToList();

            return View(list.ToPagedList(pageNumber, _pageSize));
        }

        public IActionResult ThemLoaiDiemGiaoDich()
        {
            ViewData["Title"] = "Thêm loại điểm giao dịch";

            return View();
        }
        public async Task<IActionResult> SuaLoaiDiemGiaoDich(int id)
        {
            ViewData["Title"] = "Sửa loại điểm giao dịch";

            if (id == 0)
            {
                return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
            }
            LoaiDiemGiaoDichTrinhDien obj = new LoaiDiemGiaoDichTrinhDien();
            obj = (await _respository.GetLoaiDiemGiaoDich(id));

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemLoaiDiemGiaoDich(LoaiDiemGiaoDich request)
        {
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

            var result = await _respository.AddLoaiDiemGiaoDich(request);
            if (result != null)
            {
                var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                {
                    serviceid = serviceid_diemgiaodich,
                    eformid = "0",
                    idloaihinh = result.Id.ToString(),
                    tenloaihinh = result.Ten,
                });

                if (success == null)
                {
                    await _respository.DeleteLoaiDiemGiaoDich(result.Id);
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Thêm mới số hóa thất bại!",
                    });
                }
                else
                {
                    await _respository.EditLoaiDiemGiaoDich(new LoaiDiemGiaoDich
                    {
                        Id = result.Id,
                        Ten = result.Ten,
                        DongBoID = success,
                        NguonDongBo = _nguondongbo,
                    });
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Thêm mới thành công",
                    });
                }
                return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaLoaiDiemGiaoDich(LoaiDiemGiaoDich request)
        {
            if (!ModelState.IsValid)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Vui lòng nhập đầy đủ thông tin bắt buộc!"
                });

                return View(request);
            }

            var loaicu = await _respository.GetLoaiDiemGiaoDich(request.Id);

            var result = await _respository.EditLoaiDiemGiaoDich(request);

            if (result != null)
            {
                int? success;
                if (result.DongBoID > 0)
                {
                    success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                    {
                        serviceid = serviceid_diemgiaodich,
                        eformid = result.DongBoID.ToString(),
                        idloaihinh = result.Id.ToString(),
                        tenloaihinh = result.Ten,
                    });
                }
                else
                {
                    success = -1;
                }

                if (success <= 0)
                {
                    await _respository.EditLoaiDiemGiaoDich(new LoaiDiemGiaoDich
                    {
                        Id = loaicu.Id,
                        Ten = loaicu.Ten,
                        DongBoID = loaicu.DongBoID,
                        NguonDongBo = loaicu.NguonDongBo,
                        IsDelete = loaicu.IsDelete,
                        IsStatus = loaicu.IsStatus,
                    });
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = false,
                        Message = "Cập nhật số hóa thất bại",
                    });
                }
                else
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = true,
                        Message = "Cập nhật thành công",
                    });

                }

                return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }

            var obj = new LoaiDiemGiaoDichTrinhDien
            {
                Id = request.Id,
                Ten = request.Ten,
                DongBoID = request.DongBoID,
                NguonDongBo = request.NguonDongBo,
                IsDelete = request.IsDelete,
                IsStatus = request.IsStatus,
            };

            return View(obj);
        }

        public async Task<IActionResult> XoaLoaiDiemGiaoDich(int id)
        {
            var result = await _respository.DeleteLoaiDiemGiaoDich(id);
            if (result != null)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Xóa thành công",
                });
            }
            else
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = "Đã xảy ra lỗi"
                });
            }

            return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
        }

        #endregion

        #region ĐỒNG BỘ
        public async Task<IActionResult> DongBoDanhMucAmThuc()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucAmThuc();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục ẩm thực thành công" });
                return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DanhMuc/LoaiAmThuc");
            }
        }

        public async Task<IActionResult> DongBoDanhMucDiaDiemAnUong()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucDiaDiemAnUong();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục địa điểm ăn uống thành công" });
                return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DanhMuc/LoaiDiaDiemAnUong");
            }
        }

        public async Task<IActionResult> DongBoDanhMucLeHoi()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucLeHoi();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục lễ hội thành công" });
                return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DanhMuc/LoaiLeHoi");
            }
        }

        public async Task<IActionResult> DongBoDanhMucDiemGiaoDich()
        {
            try
            {
                await _danhMucScheduleRepository.GetDataDanhMucDiemGiaoDich();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ danh mục điểm giao dịch thành công" });
                return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DanhMuc/LoaiDiemGiaoDich");
            }
        }

        public async Task<IActionResult> DongBoLoaiVeDiTich()
        {
            try
            {
                await _veDiTichLoaiRepository.GetDataLoaiVe();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ loại vé di tích thành công" });
                return Redirect("/HueCIT/DanhMuc/LoaiVeDiTich");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DanhMuc/LoaiVeDiTich");
            }
        }

        #endregion
    }
}