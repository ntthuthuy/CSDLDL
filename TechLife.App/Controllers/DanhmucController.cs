using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.BoPhan;
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
        private readonly IDanhMucScheduleRepository _danhMucScheduleRepository;

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
            _danhMucDongBoRepository = danhMucDongBoRepository;
            _danhMucDongBoService = danhMucDongBoService;
            _loaiDichVuDongBoService = loaiDichVuDongBoService;
            _danhMucScheduleRepository = danhMucScheduleRepository;
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
        public async Task<IActionResult> Quoctich()
        {

            ViewData["Title"] = "Quốc tịch";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _quocTichApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themquoctich()
        {

            ViewData["Title"] = "Thêm quốc tịch";
            ViewData["Title_parent"] = "Danh mục";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themquoctich(QuocTichModel request)
        {

            ViewData["Title"] = "Thêm bộ phận";
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
            var result = await _quocTichApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return Redirect("/Danhmuc/Quoctich");
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
        public async Task<IActionResult> Suaquoctich(string id = "")
        {
            ViewData["Title"] = "Sửa quốc tịch";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _quocTichApiClient.GetById(Id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suaquoctich(string Id, QuocTichModel request)
        {
            ViewData["Title"] = "Sửa quốc tịch";
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
            var result = await _quocTichApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Danhmuc/Quoctich");
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
        public async Task<IActionResult> Xoaquoctich(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _quocTichApiClient.Delete(id);
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
        #endregion

        #region BỘ PHẬN
        public async Task<IActionResult> Bophan()
        {

            ViewData["Title"] = "Bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            int linhvuc = !String.IsNullOrEmpty(Request.Query["linhvuc"]) ? Convert.ToInt32(Request.Query["linhvuc"]) : 0;

            await OptionLinhVucKinhDoanh(linhvuc);

            var data = await _boPhanService.GetAll(linhvuc);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Thembophan()
        {

            ViewData["Title"] = "Thêm bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Thembophan(BoPhanCreateRequest request)
        {

            ViewData["Title"] = "Thêm bộ phận";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();

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

            var model = new BoPhanUpdateRequest()
            {
                Id = data.Id,
                LinhVucId = !String.IsNullOrEmpty(data.LinhVucId) ? Array.ConvertAll(data.LinhVucId.Split(','), int.Parse) : null,
                MoTa = data.MoTa,
                TenBoPhan = data.TenBoPhan
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

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoabophan(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _boPhanApiClient.Delete(id);
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

            var data = await _dichVuApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themdichvu()
        {

            ViewData["Title"] = "Thêm dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _dichVuApiClient.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/Danhmuc/Dichvu");
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
        public async Task<IActionResult> Suadichvu(string id = "")
        {
            ViewData["Title"] = "Sửa dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _dichVuApiClient.GetById(Id);
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

            var result = await _dichVuApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Danhmuc/Dichvu");
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
        public async Task<IActionResult> Xoadichvu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _dichVuApiClient.Delete(id);
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

            var data = await _loaiDichVuApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themloaidichvu()
        {

            ViewData["Title"] = "Thêm loại dịch vụ";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _loaiDichVuApiClient.Create(request);
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
            var data = await _loaiDichVuApiClient.GetById(Id);
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

            var result = await _loaiDichVuApiClient.Update(id, request);
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

            var result = await _loaiDichVuApiClient.Delete(id);
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

            var data = await _loaiHinhApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themloaihinhkinhdoanh()
        {

            ViewData["Title"] = "Thêm loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _loaiHinhApiClient.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/Danhmuc/Loaihinhkinhdoanh");
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
        public async Task<IActionResult> Sualoaihinhkinhdoanh(string id = "")
        {
            ViewData["Title"] = "Sửa loại hình kinh doanh";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiHinhApiClient.GetById(Id);
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

            var result = await _loaiHinhApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Danhmuc/Loaihinhkinhdoanh");
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
        public async Task<IActionResult> Xoaloaihinhkinhdoanh(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhApiClient.Delete(id);
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

            var data = await _ngoaiNguApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themngoaingu()
        {

            ViewData["Title"] = "Thêm ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _ngoaiNguApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return Redirect("/Danhmuc/Ngoaingu");
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
        public async Task<IActionResult> Suangoaingu(string id = "")
        {
            ViewData["Title"] = "Sửa ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _ngoaiNguApiClient.GetById(Id);
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

            var result = await _ngoaiNguApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Ngoaingu");
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
        public async Task<IActionResult> Xoangoaingu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _ngoaiNguApiClient.Delete(id);
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
            var data = await _mucDoThongThaoNgoaiNguApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themmucdothongthaongoaingu()
        {

            ViewData["Title"] = "Thêm mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _mucDoThongThaoNgoaiNguApiClient.Create(request);

            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                return Redirect("/Danhmuc/Dothanhthaongonngu");
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
        public async Task<IActionResult> Suamucdothongthaongoaingu(string id = "")
        {
            ViewData["Title"] = "Sửa mức độ thông thạo ngoại ngữ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _mucDoThongThaoNgoaiNguApiClient.GetById(Id);
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

            var result = await _mucDoThongThaoNgoaiNguApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Danhmuc/Dothanhthaongonngu");
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
        public async Task<IActionResult> Xoamucdothongthaongoaingu(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _mucDoThongThaoNgoaiNguApiClient.Delete(id);
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

            var data = await _loaiHinhLaoDongApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themloaihinhlaodong()
        {

            ViewData["Title"] = "Thêm loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _loaiHinhLaoDongApiClient.Create(request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });
                return Redirect("/Danhmuc/Loaihinhlaodong");
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
        public async Task<IActionResult> Sualoaihinhlaodong(string id = "")
        {
            ViewData["Title"] = "Sửa loại hình lao động";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _loaiHinhLaoDongApiClient.GetById(Id);
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

            var result = await _loaiHinhLaoDongApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Loaihinhlaodong");
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
        public async Task<IActionResult> Xoaloaihinhlaodong(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _loaiHinhLaoDongApiClient.Delete(id);
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

            var data = await _trinhDoApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themtrinhdo()
        {

            ViewData["Title"] = "Thêm trình độ";
            ViewData["Title_parent"] = "Danh mục";
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

            var result = await _trinhDoApiClient.Create(request);

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
        public async Task<IActionResult> Suatrinhdo(string id = "")
        {
            ViewData["Title"] = "Sửa trình độ";
            ViewData["Title_parent"] = "Danh mục";
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var data = await _trinhDoApiClient.GetById(Id);
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
            var result = await _trinhDoApiClient.Update(id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Danhmuc/Trinhdo");
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
        public async Task<IActionResult> Xoatrinhdo(string Id)
        {

            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _trinhDoApiClient.Delete(id);
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

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);

            return View(data);
        }
        public async Task<IActionResult> Nhahang()
        {

            ViewData["Title"] = "Nhà hàng";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.NhaHang);

            return View(data);
        }
        public async Task<IActionResult> Congtyluhanh()
        {

            ViewData["Title"] = "Công ty lữ hành";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.LuHanh);

            return View(data);
        }
        public async Task<IActionResult> Vanchuyen()
        {

            ViewData["Title"] = "Công ty vận chuyển";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.VanChuyen);

            return View(data);
        }

        public async Task<IActionResult> Diemdulich()
        {

            ViewData["Title"] = "Điểm du lịch";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.DiemDuLich);

            return View(data);
        }
        public async Task<IActionResult> Khudulich()
        {

            ViewData["Title"] = "Khu du lịch";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.KhuDuLich);

            return View(data);
        }
        public async Task<IActionResult> Khuvcgt()
        {

            ViewData["Title"] = "Khu vui chơi giải trí";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.KhuVuiChoi);

            return View(data);
        }
        public async Task<IActionResult> Cssk()
        {

            ViewData["Title"] = "Dịch vụ chăm sóc sức khỏe";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.CSSK);

            return View(data);
        }
        public async Task<IActionResult> Thethao()
        {

            ViewData["Title"] = "Dịch vụ thể dục, thể thao";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.TheThao);

            return View(data);
        }
        public async Task<IActionResult> Cosomuasam()
        {

            ViewData["Title"] = "Cơ sở mua sắm";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.MuaSam);

            return View(data);
        }
        public async Task<IActionResult> Huongdanvien()
        {

            ViewData["Title"] = "Hướng dẫn viên";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.HDV);

            return View(data);
        }
        public async Task<IActionResult> Vesinhcongcong()
        {

            ViewData["Title"] = "Vệ sinh công cộng";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.VSCC);

            return View(data);
        }
        [HttpGet]
        public IActionResult Themdanhmuc(string act)
        {

            ViewData["Title"] = "Thêm mới danh mục";
            ViewData["Title_parent"] = "Danh mục";

            ViewBag.Action = act;
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
                var result = await _danhMucApiClient.Create(request);

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
                var result = await _danhMucApiClient.Create(request);
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
            var data = await _danhMucApiClient.GetById(Id);

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
                var result = await _danhMucApiClient.Update(id, request);
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
                var result = await _danhMucApiClient.Update(id, request);
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

            var result = await _danhMucApiClient.Delete(id);
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

            await OptionLinhVucKinhDoanh(linhvuc);
            var data = await _tienNghiService.GetAll(linhvuc);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themtiennghi()
        {

            ViewData["Title"] = "Thêm tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

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

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Suatiennghi(string id = "")
        {

            ViewData["Title"] = "Sửa tiện nghi";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _tienNghiService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            await OptionLinhVucKinhDoanh();
            await OptionDonViTinh();

            var model = new TienNghiUpdateRequest()
            {
                DonViTinhId = data.DonViTinhId,
                Id = data.Id,
                LinhVucId = !String.IsNullOrEmpty(data.LinhVucId) ? Array.ConvertAll(data.LinhVucId.Split(','), int.Parse) : null,
                MoTa = data.MoTa,
                Ten = data.Ten
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


            var data = await _donViTinhApiClient.GetAll();

            return View(data);
        }
        [HttpGet]
        public IActionResult Themdvt()
        {

            ViewData["Title"] = "Thêm đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

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

            var result = await _donViTinhApiClient.Create(request);

            if (result != null)
            {
                if (result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });

                    return Redirect("/Danhmuc/Donvitinh");
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
        public async Task<IActionResult> Suadvt(string id = "")
        {


            ViewData["Title"] = "Sửa đơn vị tính";
            ViewData["Title_parent"] = "Danh mục";

            var data = await _donViTinhApiClient.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

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

            var result = await _donViTinhApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Cập nhật thành công",
                });

                return Redirect("/Danhmuc/Donvitinh");
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

            int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;

            var data = await _giayPhepService.GetPaging(loaihinh, pageRequest);

            await OptionLinhVucKinhDoanh(loaihinh);

            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Themloaigiayphep()
        {

            ViewData["Title"] = "Thêm loại giấy phép chứng chỉ";
            ViewData["Title_parent"] = "Danh mục";

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
                    return RedirectToAction("Giayphep");
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
                Ten = data.Ten
            };
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
                    return RedirectToAction("Giayphep");
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
    }
}
