using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.App.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IThongKeService _thongKeService;

        public HomeController(IUserService userService
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
            , IDuLieuDuLichService duLieuDuLichService
            , IThongKeService thongKeService)
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
                  , configuration
                  , fileUploadService, nhaCungCapService, trackingService)
        {
            _duLieuDuLichService = duLieuDuLichService;
            _thongKeService = thongKeService;
        }

        public async Task<IActionResult> Index(string username = "")
        {
            var luutru = await _duLieuDuLichService.LuuTruTheoLoaiHinh();
            ViewBag.LuuTru = luutru;

            var khachsan = await _duLieuDuLichService.KhachSanTheoHangSao();
            ViewBag.KhachSan = khachsan;

            var nhahang = await _duLieuDuLichService.NhaHangTheoLoaiHinh();
            ViewBag.NhaHang = nhahang;

            var muasam = await _duLieuDuLichService.MuaSamTheoLoaiHinh();
            ViewBag.MuaSam = muasam;

            var hdv = await _duLieuDuLichService.HDVTheoLoaiThe();
            ViewBag.HDV = hdv;

            var tour = await _duLieuDuLichService.TourTheoLoaiHinh();
            ViewBag.Tour = tour;

            var diemdulich = await _duLieuDuLichService.DiemDuLichTheoLoaiHinh();
            ViewBag.DiemDuLich = diemdulich;

            var luhanh = await _duLieuDuLichService.LuHanhTheoLoaiHinh();
            ViewBag.LuHanh = luhanh;

            var diadiemanuong = await _thongKeService.DiaDiemAnUong();
            ViewBag.DiaDiemAnUong = diadiemanuong;

            var disan = await _thongKeService.DiSanVanHoa();
            ViewBag.DiSan = disan;

            var vuichoi = await _thongKeService.KhuVuiChoi();
            ViewBag.VuiChoi = vuichoi;

            var vesinh = await _thongKeService.VeSinhCongCong();
            ViewBag.VeSinh = vesinh;

            var diemgiaodich = await _thongKeService.DiemGiaoDich();
            ViewBag.DiemGiaoDich = diemgiaodich;

            return View();
        }

        public IActionResult Default(string username)
        {
            ViewBag.Name = username;
            return View();
        }

        public IActionResult Error(string type)
        {
            if (type == "file_is_empty")
            {
                ViewBag.Message = "Lỗi không tìm thấy file! Vui lòng kiểm tra lại.";
            }
            else
            {
                ViewBag.Message = "Đã có lỗi xảy ra!";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return Redirect(viewModel.ReturnUrl);
        }

        public async Task<IActionResult> ListDiaPhuongByParent(int id)
        {
            var diaphuong = await _diaPhuongService.GetAllByParent(id);

            return Ok(diaphuong);
        }
    }
}