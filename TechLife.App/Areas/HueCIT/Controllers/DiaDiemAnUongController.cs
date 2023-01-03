using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Areas.HueCIT.Repository;
using TechLife.App.Controllers;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model;
using TechLife.Model.BoPhan;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.GiayPhepChungChi;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HueCIT;
using TechLife.Model.TienNghi;
using TechLife.Service;
using TechLife.Service.HueCIT;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class DiaDiemAnUongController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IDanhGiaService _danhGiaService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IGiayPhepService _giayPhepService;
        private readonly IFileApiClient _fileApiClient;
        private readonly ITienNghiService _tienNghiService;
        private readonly IBoPhanService _boPhanService;
        private readonly IHuongDanVienService _huongDanVienService;

        private readonly IHoSoService _hoSoService;
        private readonly IHoSoScheduleRepository _hoSoScheduleRepository;
        private readonly IDanhMucRepository _respository;
        private readonly IDanhMucDongBoRepository _danhMucDongBoRepository;

        private readonly IConfiguration _config;

        public DiaDiemAnUongController(IUserService userService
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
            , IDanhGiaService danhGiaService
            , IDuLieuDuLichService duLieuDuLichService
            , IGiayPhepService giayPhepService
            , IFileApiClient fileApiClient
            , ITienNghiService tienNghiService
            , IBoPhanService boPhanService
            , IHuongDanVienService huongDanVienService
            , IHoSoService hoSoService
            , IHoSoScheduleRepository hoSoScheduleRepository
            , IDanhMucRepository respository
            , IDanhMucDongBoRepository danhMucDongBoRepository)
            : base(userService, diaPhuongApiClient
                  , donViTinhApiClient, loaiHinhApiClient
                  , dichVuApiClient, ngoaiNguApiClient
                  , trinhDoApiClient, boPhanApiClient
                  , loaiPhongApiClient, mucDoThongThaoNgoaiNguApiClient
                  , tienNghiApiClient, huongDanVienApiClient
                  , diemVeSinhApiClient, loaiGiuongApiClient
                  , csdlDuLichApiClient, quocTichApiClient
                  , loaiDichVuApiClient, danhMucApiClient
                  , loaiHinhLaoDongApiClient, tinhChatLaoDongApiClient
                  , diaPhuongService
                  , configuration
                  , fileUploadService
                  , nhaCungCapService, trackingService)
        {
            _danhGiaService = danhGiaService;
            _duLieuDuLichService = duLieuDuLichService;
            _giayPhepService = giayPhepService;
            _fileApiClient = fileApiClient;
            _tienNghiService = tienNghiService;
            _boPhanService = boPhanService;
            _huongDanVienService = huongDanVienService;
            _hoSoService = hoSoService;
            _respository = respository;
            _hoSoScheduleRepository = hoSoScheduleRepository;
            _config = configuration;
            _danhMucDongBoRepository = danhMucDongBoRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách địa điểm ăn uống";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
            int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
            int name = !String.IsNullOrEmpty(Request.Query["name"]) ? Convert.ToInt32(Request.Query["name"]) : -1;
            int phucvu = !String.IsNullOrEmpty(Request.Query["phucvu"]) ? Convert.ToInt32(Request.Query["phucvu"]) : -1;
            int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

            ViewBag.LoaiHinh = loaihinh;
            ViewBag.Huyen = huyen;
            ViewBag.Name = name;
            ViewBag.PhucVu = phucvu;
            ViewBag.Nguon = nguon;

            await OptionLoaiDiaDiemAnUong(loaihinh);
            await OptionHuyen(1, huyen);
            await OptionDiaDiemAnUong(name);
            OptionPhucVu(phucvu);
            await OptionNguonDongBo(nguon);

            var pageRequest = new FilterRequestPaging()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                phucvu = phucvu,
                loaidiadiem = loaihinh,
                huyen = huyen,
                namenhahang = name,
                nguondongbo = nguon
            };

            var data = await _hoSoService.GetsHoSoPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.NhaHang, pageRequest);

            return View(data);
        }

        public async Task<IActionResult> Detail(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var model = await _hoSoService.GetHoSo(Id);

            if (model != null)
            {
                ViewData["Title"] = "Thông tin hồ sơ " + model.Ten;
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Thêm mới địa điểm ăn uống";

            var csltModel = new DuLieuDuLichModel();

            csltModel.DSThucDon = ListThucDonHoSo();
            csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
            csltModel.DSTrinhDo = await ListTrinhDoHoSo();
            csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.NhaHang);
            csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
            csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.NhaHang);
            csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.NhaHang);

            OptionPhucVuAdd();
            await OptionHuyen();
            await OptionXa(-1);
            await OptionDonViTinh(2);
            await OptionTieuChuanCoSo();
            await OptionLoaiNhaHang();
            await OptionLoaiDiaDiemAnUong();
            await OptionNhaCungCap();

            var model = new DuLieuDuLichCreateExtRequest()
            {
                DuLieuDuLich = csltModel,
                Images = new ImageUploadExtRequest()
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Cập nhật địa điểm ăn uống";

            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var csltModel = await _hoSoService.GetHoSo(Id);

            if (csltModel != null)
            {
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSDichVu = await ListDichVuHoSo(csltModel.Id, csltModel.DSDichVu);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSTienNghi);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSVanBan);
                
                OptionPhucVuAdd(csltModel.PhucVu);
                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiNhaHang();
                await OptionLoaiDiaDiemAnUong(csltModel.LoaiDiaDiemAnUong);
                await OptionNhaCungCap();
            }

            var model = new DuLieuDuLichUpdateExtRequest()
            {
                DuLieuDuLich = csltModel,
                Images = new ImageUploadExtRequest()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "create_nhahang,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DuLieuDuLichCreateExtRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

                if (request.DuLieuDuLich.MaDoanhNghiep != null && request.DuLieuDuLich.MaDoanhNghiep != "")
                {
                    bool check = await _hoSoService.CheckMaDoanhNghiep(request.DuLieuDuLich.MaDoanhNghiep);
                    if (check == true)
                    {
                        TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Mã doanh nghiệp đã tồn tại!" });
                        return Redirect("/HueCIT/DiaDiemAnUong/Add");
                    }
                }
                
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);

                request.DuLieuDuLich.NguonDongBo = 0;

                if (result.IsSuccessed)
                {
                    if (request.Images != null && request.Images != null)
                    {
                        var upload = await _csdlDuLichApiClient.UploadImageExt(result.ResultObj.Id, request.Images);
                    }
                    if (request.Files != null)
                    {
                        var upload = await _csdlDuLichApiClient.UploadFile(result.ResultObj.Id, request.Files);
                    }
                }

                if (result.IsSuccessed && request.DuLieuDuLich.ToaDoX != null && request.DuLieuDuLich.ToaDoX != 0 && request.DuLieuDuLich.ToaDoY != null && request.DuLieuDuLich.ToaDoY != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.DuLieuDuLich.ToaDoY,
                        y = request.DuLieuDuLich.ToaDoX
                    };

                    TechLife.Model.HueCIT.HoSoDongBoAdd data = new Model.HueCIT.HoSoDongBoAdd
                    {
                        id = request.DuLieuDuLich.Id,
                        ten = request.DuLieuDuLich.Ten,
                        linhvuckin = request.DuLieuDuLich.LinhVucKinhDoanhId,
                        loaihinhid = request.DuLieuDuLich.LoaiHinhId,
                        sonha = request.DuLieuDuLich.SoNha,
                        duongpho = request.DuLieuDuLich.DuongPho,
                        sodienthoa = request.DuLieuDuLich.SoDienThoai
                    };

                    AddEditGIS(data, geo);
                }

                await Tracking("Thêm địa điểm ăn uống " + request.DuLieuDuLich.Ten);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/DiaDiemAnUong/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/DiaDiemAnUong/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/DiaDiemAnUong/Add");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiNhaHang();
                await OptionLoaiDiaDiemAnUong();
                OptionPhucVu();

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/HueCIT/DiaDiemAnUong/Add");
        }

        [HttpPost]
        [Authorize(Roles = "edit_nhahang,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DuLieuDuLichUpdateExtRequest request, string type_sumit, string maDoanhNghiepCu)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

                if (request.DuLieuDuLich.MaDoanhNghiep != null && request.DuLieuDuLich.MaDoanhNghiep != "")
                {
                    bool thayDoiMa = string.Equals(request.DuLieuDuLich.MaDoanhNghiep, maDoanhNghiepCu);
                    if (thayDoiMa == false)
                    {
                        bool check = await _hoSoService.CheckMaDoanhNghiep(request.DuLieuDuLich.MaDoanhNghiep);
                        if (check == true)
                        {
                            TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Mã doanh nghiệp đã tồn tại!" });
                            return Redirect("/HueCIT/DiaDiemAnUong/Edit?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                        }
                    }
                }

                var csltModel = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                request.DuLieuDuLich.ToaDoX = csltModel.ToaDoX;
                request.DuLieuDuLich.ToaDoY = csltModel.ToaDoY;

                var ds = await _duLieuDuLichService.GetListVanBanByHoSo(request.DuLieuDuLich.Id);

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        if (v.IsStatus == true)
                        {
                            var f = ds.Where(x => x.GiayPhepId == v.GiayPhepId).FirstOrDefault();
                            if (f != null)
                            {
                                v.FilePath = f.FilePath;
                                v.FileName = f.FileName;
                            }

                            if (v.Files != null)
                            {
                                v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : v.FilePath;
                                v.FileName = v.Files != null ? v.Files.FileName : v.FileName;
                            }
                        }
                        else
                        {
                            v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : v.FilePath;
                            v.FileName = v.Files != null ? v.Files.FileName : v.FileName;
                        }
                    }
                }

                var result = await _duLieuDuLichService.Update(request.DuLieuDuLich.Id, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = messageError });
                    return Redirect("/HueCIT/DiaDiemAnUong/Edit?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }

                if (request.Images != null && request.Images.Images != null && request.Images.Images.Any())
                {
                    var upload = await _csdlDuLichApiClient.UploadImageExt(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                if (result.IsSuccessed && request.DuLieuDuLich.ToaDoX != null && request.DuLieuDuLich.ToaDoX != 0 && request.DuLieuDuLich.ToaDoY != null && request.DuLieuDuLich.ToaDoY != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.DuLieuDuLich.ToaDoY,
                        y = request.DuLieuDuLich.ToaDoX
                    };

                    TechLife.Model.HueCIT.HoSoDongBoAdd data = new Model.HueCIT.HoSoDongBoAdd
                    {
                        id = request.DuLieuDuLich.Id,
                        ten = request.DuLieuDuLich.Ten,
                        linhvuckin = request.DuLieuDuLich.LinhVucKinhDoanhId,
                        loaihinhid = request.DuLieuDuLich.LoaiHinhId,
                        sonha = request.DuLieuDuLich.SoNha,
                        duongpho = request.DuLieuDuLich.DuongPho,
                        sodienthoa = request.DuLieuDuLich.SoDienThoai
                    };

                    AddEditGIS(data, geo);
                }

                await Tracking("Cập nhật địa điểm ăn uống " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/DiaDiemAnUong/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/DiaDiemAnUong/Add");
                    }
                }
                return Redirect("/HueCIT/DiaDiemAnUong/Edit?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiNhaHang();
                await OptionCoSoLuuTru();
                await OptionNhaCungCap();
                await OptionLoaiDiaDiemAnUong();
                OptionPhucVu(request.DuLieuDuLich.PhucVu);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/DiaDiemAnUong/Edit?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_nhahang,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var result = await _duLieuDuLichService.Delete(Id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            if (result.IsSuccessed)
            {
                RemoveGIS(id, (int)LinhVucKinhDoanh.NhaHang);
            }

            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        public async Task OptionLoaiDiaDiemAnUong(int? seletedId = 0)
        {
            var loai = (await _respository.GetsLoaiDiaDiemAnUong()).ToList();

            var list = loai.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDiaDiemAnUong = list;
        }

        public async Task OptionDiaDiemAnUong(int seletedId = 0)
        {
            var pageRequest = new FilterRequest()
            {
                Keyword = "",
            };

            var loai = (await _hoSoService.GetsHoSoForFilter(Request.GetLanguageId(), (int)LinhVucKinhDoanh.NhaHang, pageRequest));

            var list = loai.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllDiaDiemAnUong = list;
        }

        public void OptionPhucVu(int? seletedId = 0)
        {
            List<StaticModel> phucvu = new List<StaticModel>
            {
                new StaticModel { Id = 0, Ten = "Mang về" },
                new StaticModel { Id = 1, Ten = "Tại chỗ" }
            };

            var list = phucvu.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listPhucVuDiaDiemAnUong = list;
        }

        public void OptionPhucVuAdd(int? seletedId = 2)
        {
            List<StaticModel> phucvu = new List<StaticModel>
            {
                new StaticModel { Id = 2, Ten = "Tất cả" },
                new StaticModel { Id = 0, Ten = "Mang về" },
                new StaticModel { Id = 1, Ten = "Tại chỗ" },
            };

            var list = phucvu.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = x.Id == seletedId
            });

            ViewBag.listPhucVuDiaDiemAnUong = list;
        }

        private async Task<List<DichVuHoSoModel>> ListDichVuHoSo(int hosoId = 0, List<DichVuHoSoModel> listValue = null)
        {
            var list = await _dichVuApiClient.GetAll();
            var listItem = new List<DichVuHoSoModel>();
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.DichVuId == x.Id).FirstOrDefault() : null;

                listItem.Add(new DichVuHoSoModel()
                {
                    DichVu = x,
                    DonViTinhId = value != null ? value.DonViTinhId : 0,
                    HoSoId = hosoId,
                    QuyMo = value != null ? value.QuyMo : 0,
                });
            }
            return listItem;
        }

        private List<ThucDonHoSoModel> ListThucDonHoSo(int hosoId = 0, List<ThucDonHoSoModel> listValue = null)
        {
            var listItem = new List<ThucDonHoSoModel>();
            if (listValue != null && listValue.Count() > 0)
            {
                foreach (var val in listValue)
                {
                    listItem.Add(new ThucDonHoSoModel()
                    {
                        DonGia = val.DonGia,
                        HosoId = val.HosoId,
                        MoTa = val.MoTa,
                        Id = val.Id,
                        TenThucDon = val.TenThucDon
                    });
                }
            }
            else
            {
                listItem.Add(new ThucDonHoSoModel()
                {
                    DonGia = 0,
                    HosoId = hosoId,
                    MoTa = "",
                    Id = 0,
                    TenThucDon = ""
                });
            }
            return listItem;
        }

        private async Task<List<NgoaiNguHoSoModel>> ListNgoaiNguHoSo(int hosoId = 0, List<NgoaiNguHoSoModel> listValue = null)
        {
            List<NgoaiNguModel> list = await _ngoaiNguApiClient.GetAll();
            var listItem = new List<NgoaiNguHoSoModel>();
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.NgoaiNguId == x.Id).FirstOrDefault() : null;

                listItem.Add(new NgoaiNguHoSoModel()
                {
                    NgoaiNgu = x,
                    HoSoId = hosoId,
                    DonViTinhId = value != null ? value.DonViTinhId : 2,
                    SoLuong = value != null ? value.SoLuong : 0
                });
            }
            return listItem;
        }
        private async Task<List<TrinhDoHoSoModel>> ListTrinhDoHoSo(int hosoId = 0, List<TrinhDoHoSoModel> listValue = null)
        {
            List<TrinhDoModel> list = await _trinhDoApiClient.GetAll();
            var listItem = new List<TrinhDoHoSoModel>();
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.TrinhDoId == x.Id).FirstOrDefault() : null;

                listItem.Add(new TrinhDoHoSoModel()
                {
                    TrinhDo = x,
                    DonViTinhId = value != null ? value.DonViTinhId : 2,
                    HoSoId = hosoId,
                    SoLuong = value != null ? value.SoLuong : 0
                });
            }
            return listItem;
        }

        private async Task<List<BoPhanHoSoModel>> ListBoPhanHoSo(int linhvucId, int hosoId = 0, List<BoPhanHoSoModel> listValue = null)
        {
            List<BoPhanVm> list = await _boPhanService.GetAll(linhvucId);
            var listItem = new List<BoPhanHoSoModel>();
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.BoPhanId == x.Id).FirstOrDefault() : null;

                listItem.Add(new BoPhanHoSoModel()
                {
                    BoPhan = x,
                    DonViTinhId = value != null ? value.DonViTinhId : 2,
                    HoSoId = hosoId,
                    SoLuong = value != null ? value.SoLuong : 0
                });
            }
            return listItem;
        }

        private async Task<List<MucDoTTNNHoSoModel>> ListMucDoThongThaoHoSo(int hosoId = 0, List<MucDoTTNNHoSoModel> listValue = null)
        {
            List<MucDoThongThaoNgoaiNguModel> list = await _mucDoThongThaoNgoaiNguApiClient.GetAll();

            var listItem = new List<MucDoTTNNHoSoModel>();
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.MucDoId == x.Id).FirstOrDefault() : null;

                listItem.Add(new MucDoTTNNHoSoModel()
                {
                    MucDoThongThao = x,
                    DonViTinhId = value != null ? value.DonViTinhId : 2,
                    HoSoId = hosoId,
                    SoLuong = value != null ? value.SoLuong : 0
                });
            }
            return listItem;
        }

        private async Task<List<TienNghiHoSoModel>> ListMucTienNghiHoSo(int linhvucId, int hosoId = 0, List<TienNghiHoSoModel> listValue = null)
        {
            var listItem = new List<TienNghiHoSoModel>();
            List<TienNghiVm> list = await _tienNghiService.GetAll(linhvucId);
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.TienNghiId == x.Id).FirstOrDefault() : null;

                listItem.Add(new TienNghiHoSoModel()
                {
                    TienNghi = x,
                    IsPhuPhi = value != null ? value.IsPhuPhi : false,
                    IsSuDung = value != null ? value.IsSuDung : false,
                    SoLuong = value != null ? value.SoLuong : 0,
                    DonGia = value != null ? value.DonGia : 0,
                    HoSoId = 0
                });
            }
            return listItem;
        }

        private async Task<List<HoSoVanBanVm>> ListVanBanHoSo(int linhvucId, int hosoId = 0, List<HoSoVanBanVm> listValue = null)
        {
            var listItem = new List<HoSoVanBanVm>();
            List<GiayPhepVm> list = await _giayPhepService.GetAll(linhvucId);
            foreach (var x in list)
            {
                var value = listValue != null ? listValue.Where(v => v.GiayPhepId == x.Id).FirstOrDefault() : null;

                listItem.Add(new HoSoVanBanVm()
                {
                    GiayPhepId = x.Id,
                    MaSo = value != null ? value.MaSo : "",
                    NgayHetHan = value != null ? value.NgayHetHan : DateTime.Now,
                    NgayCap = value != null ? value.NgayCap : DateTime.Now,
                    NoiCap = value != null ? value.NoiCap : "",
                    IsStatus = value != null ? value.IsStatus : false,
                    TenGoi = x.Ten,
                    FilePath = value != null ? value.FilePath : "",
                    FileName = value != null ? value.FileName : "",
                    Id = value != null ? value.Id : 0,
                });
            }
            return listItem;
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _hoSoScheduleRepository.GetDataDiaDiemAnUong();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ nhà hàng thành công" });
                return Redirect("/HueCIT/DiaDiemAnUong/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DiaDiemAnUong/Index");
            }
        }

        private void AddEditGIS(TechLife.Model.HueCIT.HoSoDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            string ctk = _config.GetValue<string>("ArcGisToken");
            int layer = 3;
            List<TechLife.Model.HueCIT.DongBoDieuHanhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            //chk.AddHeader("cookie", "D1N=54e1a5ed82b0092d1f4d2fb8f1353ba4; AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            IRestResponse res = check.Execute(chk);
            //TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = res.Content });

            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo.objectIds == null)
            {
                TechLife.Model.HueCIT.DongBoDieuHanhAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhAdd
                {
                    geometry = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = geo.x,
                        y = geo.y,
                        spatialReference = new TechLife.Model.HueCIT.SpatialReference
                        {
                            wkid = 4326
                        }
                    },
                    attributes = new TechLife.Model.HueCIT.HoSoDongBoAdd
                    {
                        id = data.id,
                        ten = data.ten,
                        linhvuckin = data.linhvuckin,
                        loaihinhid = data.loaihinhid,
                        sonha = data.sonha,
                        duongpho = data.duongpho,
                        sodienthoa = data.sodienthoa
                    }
                };

                dta.Add(dt);

                var client = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/addFeatures");
                client.Timeout = -1;
                var req = new RestRequest(Method.POST);
                //req.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                req.AddHeader("Cookie", ctk);
                req.AlwaysMultipartFormData = true;
                req.AddParameter("features", JsonConvert.SerializeObject(dta));
                req.AddParameter("f", "json");
                req.AddParameter("rollbackOnFailure", "false");
                IRestResponse response = client.Execute(req);
            }
            else
            {
                TechLife.Model.HueCIT.DongBoDieuHanhEdit dt = new TechLife.Model.HueCIT.DongBoDieuHanhEdit
                {
                    geometry = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = geo.x,
                        y = geo.y,
                        spatialReference = new TechLife.Model.HueCIT.SpatialReference
                        {
                            wkid = 4326
                        }
                    },
                    attributes = new TechLife.Model.HueCIT.HoSoDongBoEdit
                    {
                        objectid = chkinfo.objectIds.First(),
                        id = data.id,
                        ten = data.ten,
                        linhvuckin = data.linhvuckin,
                        loaihinhid = data.loaihinhid,
                        sonha = data.sonha,
                        duongpho = data.duongpho,
                        sodienthoa = data.sodienthoa
                    }
                };

                dte.Add(dt);

                string test = JsonConvert.SerializeObject(dta);

                var client = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/updateFeatures");
                client.Timeout = -1;
                var req = new RestRequest(Method.POST);
                //req.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                req.AddHeader("Cookie", ctk);
                req.AlwaysMultipartFormData = true;
                req.AddParameter("features", JsonConvert.SerializeObject(dte));
                req.AddParameter("f", "json");
                req.AddParameter("rollbackOnFailure", "false");
                IRestResponse response = client.Execute(req);
            }
        }

        private void RemoveGIS(string Id, int linhvuc)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));
            string ctk = _config.GetValue<string>("ArcGisToken");

            int layer = 3;
            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo != null)
            {
                var client = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/deleteFeatures");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cookie", ctk);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("where", "id=" + id);
                request.AddParameter("f", "json");
                IRestResponse response = client.Execute(request);
            }
        }
    }
}