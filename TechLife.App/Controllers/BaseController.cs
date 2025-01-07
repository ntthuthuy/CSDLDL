using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model;
using TechLife.Model.Tracking;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public readonly IUserService _userService;
        public readonly IConfiguration _configuration;
        public readonly IDiaPhuongApiClient _diaPhuongApiClient;
        public readonly IDonViTinhApiClient _donViTinhApiClient;
        public readonly ILoaiHinhApiClient _loaiHinhApiClient;
        public readonly IDichVuApiClient _dichVuApiClient;
        public readonly INgoaiNguApiClient _ngoaiNguApiClient;
        public readonly ITrinhDoApiClient _trinhDoApiClient;
        public readonly IBoPhanApiClient _boPhanApiClient;
        public readonly ILoaiPhongApiClient _loaiPhongApiClient;
        public readonly ILoaiGiuongApiClient _loaiGiuongApiClient;
        public readonly ITienNghiApiClient _tienNghiApiClient;
        public readonly IHuongDanVienApiClient _huongDanVienApiClient;
        public readonly IDiemVeSinhApiClient _diemVeSinhApiClient;
        public readonly IQuocTichApiClient _quocTichApiClient;
        public readonly IDuLieuDuLichApiClient _csdlDuLichApiClient;
        public readonly ILoaiDichVuApiClient _loaiDichVuApiClient;
        public readonly IDanhMucApiClient _danhMucApiClient;
        public readonly ILoaiHinhLaoDongApiClient _loaiHinhLaoDongApiClient;
        public readonly ITinhChatLaoDongApiClient _tinhChatLaoDongApiClient;
        public readonly IMucDoThongThaoNgoaiNguApiClient _mucDoThongThaoNgoaiNguApiClient;

        public readonly IDiaPhuongService _diaPhuongService;
        public readonly INhaCungCapService _nhaCungCapService;

        public readonly IFileUploadService _fileUploadService;
        public readonly ITrackingService _trackingService;
        public BaseController(IUserService userService
            , IConfiguration configuration
            , ITrackingService trackingService = null)
        {
            _userService = userService;
            _configuration = configuration;
            _trackingService = trackingService;
        }

        public BaseController(IUserService userService
            , IDiaPhuongService diaPhuongService
            , IConfiguration configuration
            , ITrackingService trackingService = null)
        {
            _userService = userService;
            _configuration = configuration;
            _trackingService = trackingService;
            _diaPhuongService = diaPhuongService;
        }

        public BaseController(IUserService userService,
            IDiaPhuongApiClient diaPhuongApiClient,
            IDonViTinhApiClient donViTinhApiClient,
            ILoaiHinhApiClient loaiHinhApiClient,
            IDichVuApiClient dichVuApiClient,
            INgoaiNguApiClient ngoaiNguApiClient,
            ITrinhDoApiClient trinhDoApiClient,
            IBoPhanApiClient boPhanApiClient,
            ILoaiPhongApiClient loaiPhongApiClient,
            IMucDoThongThaoNgoaiNguApiClient mucDoThongThaoNgoaiNguApiClient,
            ITienNghiApiClient tienNghiApiClient,
            IHuongDanVienApiClient huongDanVienApiClient,
            IDiemVeSinhApiClient diemVeSinhApiClient,
            ILoaiGiuongApiClient loaiGiuongApiClient,
            IDuLieuDuLichApiClient csdlDuLichApiClient,
            IQuocTichApiClient quocTichApiClient,
            ILoaiDichVuApiClient loaiDichVuApiClient,
            IDanhMucApiClient danhMucApiClient,
            ILoaiHinhLaoDongApiClient loaiHinhLaoDongApiClient,
            ITinhChatLaoDongApiClient tinhChatLaoDongApiClient,
            IDiaPhuongService diaPhuongService,
            IConfiguration configuration,
            IFileUploadService fileUploadService,
            INhaCungCapService nhaCungCapService,
            ITrackingService trackingService)
        {
            _userService = userService;
            _configuration = configuration;
            _diaPhuongApiClient = diaPhuongApiClient;
            _donViTinhApiClient = donViTinhApiClient;
            _loaiHinhApiClient = loaiHinhApiClient;
            _dichVuApiClient = dichVuApiClient;
            _ngoaiNguApiClient = ngoaiNguApiClient;
            _trinhDoApiClient = trinhDoApiClient;
            _boPhanApiClient = boPhanApiClient;
            _loaiPhongApiClient = loaiPhongApiClient;
            _tienNghiApiClient = tienNghiApiClient;
            _huongDanVienApiClient = huongDanVienApiClient;
            _diemVeSinhApiClient = diemVeSinhApiClient;
            _loaiGiuongApiClient = loaiGiuongApiClient;
            _csdlDuLichApiClient = csdlDuLichApiClient;
            _quocTichApiClient = quocTichApiClient;
            _loaiDichVuApiClient = loaiDichVuApiClient;
            _danhMucApiClient = danhMucApiClient;
            _loaiHinhLaoDongApiClient = loaiHinhLaoDongApiClient;
            _tinhChatLaoDongApiClient = tinhChatLaoDongApiClient;
            _mucDoThongThaoNgoaiNguApiClient = mucDoThongThaoNgoaiNguApiClient;

            _diaPhuongService = diaPhuongService;
            _fileUploadService = fileUploadService;
            _nhaCungCapService = nhaCungCapService;
            _trackingService = trackingService;
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                string userName = User.Identity.Name;
                var result = _userService.Authencate(userName);
                if (result.Result.ResultObj == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
                var userPrincipal = this.ValidateToken(result.Result.ResultObj);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                };
                context.HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
                context.HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.Result.ResultObj);
                context.HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);

                var user = _userService.GetByUserName(userName);
                string jsonUser = JsonConvert.SerializeObject(user.Result.ResultObj);

                HttpContext.Session.SetString(SystemConstants.AppSettings.UserInfo, jsonUser);


                context.Result = new RedirectToActionResult("Index", "Home", null);
                //context.Result = new RedirectResult(Request.GetBackUrl());
            }

            base.OnActionExecuting(context);
        }

        public const string pageError404 = "/Home/Error/?type=404";
        public const string pageErrorNotFile = "/Home/Error/?type=file_is_empty";
        public const string messageError = "Đã có lỗi xãy ra trong quá trình xử lý!";
        public const string messageSussecc = "Cập nhật thành công!";

        public List<SelectListItem> listItem;
        public List<DiaPhuongModel> listDiaPhuong;

        public async Task OptionHuyen(int tinhid = 1, int seletedId = 0)
        {
            var huyen = await _diaPhuongService.GetAllByParent(tinhid);

            var list = huyen.Select(x => new SelectListItem
            {
                Text = x.TenDiaPhuong.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listHuyen = list;
        }
        public async Task OptionXa(int huyenid = 1)
        {
            var xa = await _diaPhuongService.GetAllByParent(huyenid);

            var list = xa.Select(x => new SelectListItem
            {
                Text = x.TenDiaPhuong.ToString(),
                Value = x.Id.ToString()
            });

            ViewBag.listXa = list;
        }
        public async Task OptionDiaPhuong()
        {
            var diaphuong = await _diaPhuongService.GetAll();
            listItem = new List<SelectListItem>();
            SelectListDiaPhuong(diaphuong);
        }

        private void SelectListDiaPhuong(List<DiaPhuongModel> list, int seletedId = 0, int parentId = 0, int level = 0)
        {
            var diaphuong = list.Where(v => v.ParentId == parentId);
            foreach (var x in diaphuong)
            {
                bool IsSelected = false;
                if (x.Id == seletedId)
                {
                    IsSelected = true;
                }
                string space = "";
                for (int i = 0; i < level; i++)
                {
                    space += "- ";
                }
                x.TenDiaPhuong = space + x.TenDiaPhuong;
                listItem.Add(new SelectListItem(x.TenDiaPhuong, x.Id.ToString(), IsSelected));

                var list_chird = list.Where(v => v.ParentId == x.Id);
                if (list_chird.Count() > 0)
                {
                    int level_next = level + 1;
                    SelectListDiaPhuong(list, seletedId, x.Id, level_next);
                }
            }
            ViewBag.listDiaPhuong = listItem;
        }

        public async Task OptionDonViTinh(int seletedId = 0)
        {
            var donvitinh = await _donViTinhApiClient.GetAll();

            var list = donvitinh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listDonViTinh = list;
        }

        public async Task OptionLoaiHinhKinhDoanh(int seletedId = 0)
        {
            var loaihinhkinhdoanh = await _loaiHinhApiClient.GetAll();

            var list = loaihinhkinhdoanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiHinhKD = list;
        }

        public async Task OptionCongTyLuHanh(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listCongTyLuHanh = list;
        }

        public async Task OptionDuLieuDuLich(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listDLDL = list;
        }

        public async Task OptionCoSoLuuTru(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listCoSoLuuTru = list;
        }
        public async Task OptionGetAllCSLT(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllCSLT = list;
        }
        public async Task OptionGetAllHDV(int seletedId = 0)
        {
            var luhanh = await _huongDanVienApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.HoVaTen.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllHDV = list;
        }
        public async Task OptionGetAllDDL(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.DiemDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllDDL = list;
        }
        public async Task OptionGetAllLuHanh(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllLuHanh = list;
        }
        public async Task OptionGetAllNhaHang(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.NhaHang);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllNhaHang = list;
        }
        public async Task OptionGetAllCSMS(int seletedId = 0)
        {
            var luhanh = await _csdlDuLichApiClient.GetAll((int)LinhVucKinhDoanh.MuaSam);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllCSMS = list;
        }
        public async Task OptionTieuChuanCoSo(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(TieuChuanCoSo)).Cast<TieuChuanCoSo>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listTieuChuan = list.OrderByDescending(v => v.Value);
            });
        }
        public async Task OptionTieuChuanDanhGia(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(TieuChuanDanhGia)).Cast<TieuChuanDanhGia>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listTieuChuanDanhGia = list.OrderByDescending(v => v.Value);
            });
        }
        public async Task OptionHinhThucTour(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(HinhThucTour)).Cast<HinhThucTour>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listHinhThuc = list;
            });
        }

        public async Task OptionLinhVucKinhDoanh(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LinhVucKinhDoanh)).Cast<LinhVucKinhDoanh>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listLinhVucKinhDoanh = list;
            });
        }

        public async Task OptionLoaiTheHDV(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LoaiTheHuongDanVien)).Cast<LoaiTheHuongDanVien>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listLoaiThe = list;
            });
        }

        public async Task OptionNgoaiNgu(int seletedId = 0)
        {
            var luhanh = await _ngoaiNguApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenNgoaiNgu.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listNgoaiNgu = list;
        }

        public async Task OptionLoaiNhaHang(int seletedId = 0)
        {
            var luhanh = await _dichVuApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenDichVu.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiNhaHang = list;
        }

        public async Task OptionLoaiDiemDuLich(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.DiemDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDDL = list;
        }

        public async Task OptionLoaiKhuDuLich(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.KhuDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuDL = list;
        }

        public async Task OptionLoaiKhuVuiChoi(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.KhuVuiChoi);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuVuiChoi = list;
        }

        public async Task OptionLoaiHinhCSSK(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.CSSK);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCSSK = list;
        }

        public async Task OptionLoaiHinhTheThao(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.TheThao);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiTheThao = list;
        }

        public async Task OptionLoaiCoSoMuaSam(int seletedId = 0)
        {
            var luhanh = await _loaiDichVuApiClient.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDichVu = list;
        }

        public async Task OptionLoaiCTLuHanh(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCTLH = list;
        }

        public async Task OptionLoaiCTVanChuyen(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.VanChuyen);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCTVC = list;
        }

        public async Task OptionNhaCungCap(int seletedId = 0)
        {
            var luhanh = await _nhaCungCapService.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listNhaCungCap = list;
        }

        public async Task<IActionResult> Download(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var f = await _fileUploadService.GetFileById(Id);

            try
            {
                using (var client = new WebClient())
                {
                    string file_path = f.FileUrl;

                    string fileName = f.FileName;
                    string dir_path_download = _configuration["BaseAddress"] + file_path;
                    byte[] fileBytes = client.DownloadData(dir_path_download);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
            }
            catch (Exception ex)
            {
                return Redirect(pageErrorNotFile);
            }
        }

        public async Task Tracking(string content)
        {
            var obj = new TrackingCreateRequets()
            {
                Action = content,
                Time = DateTime.Now,
                UserName = User.Identity.Name,
            };

            await _trackingService.Create(obj);
        }
        public async Task OptionLoaiTaiKhoan(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LoaiTaiKhoan)).Cast<LoaiTaiKhoan>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listLoaiTaiKhoan = list;
            });
        }

        //HueCIT
        public async Task OptionNguonDongBo(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(Common.Enums.HueCIT.NguonDongBo)).Cast<Common.Enums.HueCIT.NguonDongBo>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listNguonDongBo = list;
            });
        }

        public async Task OptionLoaiDiSan(int seletedId = 0)
        {
            var luhanh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.DiSanVanHoa);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDDL = list;
        }
    }
}