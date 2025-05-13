using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Model.BoPhan;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.GiayPhepChungChi;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HuongDanVien;
using TechLife.Model.NhaCungCap;
using TechLife.Model.TienNghi;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.App.Controllers
{
    public class HoSoController : BaseController
    {
        private readonly IDanhGiaService _danhGiaService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IGiayPhepService _giayPhepService;
        private readonly IFileApiClient _fileApiClient;
        private readonly ITienNghiService _tienNghiService;
        private readonly IBoPhanService _boPhanService;
        private readonly IHuongDanVienService _huongDanVienService;
        private readonly ILoaiDichVuService _loaiDichVuService;

        private readonly IDichVuService _dichVuService;
        private readonly INgoaiNguService _ngoaiNguService;
        private readonly ITrinhDoService _trinhDoService;
        private readonly IMucDoThongThaoNgoaiNguService _mucDoThongThaoNgoaiNguService;
        private readonly ILoaiPhongService _loaiPhongService;
        private readonly ILoaiGiuongService _loaiGiuongService;
        private readonly ILoaiHinhService _loaiHinhService;
        private readonly ILogger<HoSoController> _logger;
        //HueCIT
        private readonly IHoSoService _hoSoService;
        private readonly IConfiguration _config;

        public HoSoController(IUserService userService
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
            , ILoaiDichVuService  loaiDichVuService
            , ILogger<HoSoController> logger,
            IDichVuService dichVuService,
            INgoaiNguService ngoaiNguService,
            ITrinhDoService trinhDoService,
            IMucDoThongThaoNgoaiNguService mucDoThongThaoNgoaiNguService,
            ILoaiPhongService loaiPhongService,
            ILoaiGiuongService loaiGiuongService,
            ILoaiHinhService loaiHinhService)
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
            _loaiDichVuService = loaiDichVuService;
            _dichVuService = dichVuService;
            _ngoaiNguService = ngoaiNguService;
            _trinhDoService = trinhDoService;
            _mucDoThongThaoNgoaiNguService = mucDoThongThaoNgoaiNguService;
            _loaiPhongService = loaiPhongService;
            _loaiGiuongService = loaiGiuongService;
            _loaiHinhService = loaiHinhService;
            _config = configuration;
            _logger = logger;
        }

        private async Task<List<DichVuHoSoModel>> ListDichVuHoSo(int hosoId = 0, List<DichVuHoSoModel> listValue = null)
        {
            var list = await _dichVuService.GetAll();
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

        private async Task<List<NgoaiNguHoSoModel>> ListNgoaiNguHoSo(int hosoId = 0, List<NgoaiNguHoSoModel> listValue = null)
        {
            List<NgoaiNguModel> list = await _ngoaiNguService.GetAll();
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
            List<TrinhDoModel> list = await _trinhDoService.GetAll();
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
            List<MucDoThongThaoNgoaiNguModel> list = await _mucDoThongThaoNgoaiNguService.GetAll();

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

        private async Task<List<LoaiPhongHoSoModel>> ListLoaiPhongHoSo(int hosoId = 0, List<LoaiPhongHoSoModel> listValue = null)
        {
            List<LoaiPhongModel> list = await _loaiPhongService.GetAll(0);
            List<LoaiGiuongModel> listLoaiGiuong = await _loaiGiuongService.GetAll();

            var listItem = new List<LoaiPhongHoSoModel>();
            foreach (var x in list)
            {
                List<LoaiGiuongHoSoModel> listGiuongHoSo = new List<LoaiGiuongHoSoModel>();

                var listLP = listValue != null ? listValue.Where(v => v.LoaiPhongId == x.Id).ToList() : null;
                if (listLP != null && listLP.Count() > 0)
                {
                    foreach (var l in listLP)
                    {
                        foreach (var lg in listLoaiGiuong)
                        {
                            var g = l.DSLoaiGiuong.Where(v => v.Id == lg.Id).FirstOrDefault();
                            if (g != null)
                            {
                                listGiuongHoSo.Add(new LoaiGiuongHoSoModel()
                                {
                                    Id = lg.Id,
                                    Ten = lg.Ten,
                                    DienTich = g.DienTich,
                                    GiaPhong = g.GiaPhong,
                                    SoPhong = g.SoPhong,
                                    SoTreEm = g.SoTreEm,
                                    SoNguoiLon = g.SoNguoiLon,
                                    TenHienThi = g.TenHienThi
                                });
                            }
                            else
                            {
                                listGiuongHoSo.Add(new LoaiGiuongHoSoModel()
                                {
                                    Id = lg.Id,
                                    Ten = lg.Ten,
                                    DienTich = 0,
                                    GiaPhong = 0,
                                    SoPhong = 0,
                                    SoNguoiLon = 0,
                                    SoTreEm = 0,
                                    TenHienThi = ""
                                });
                            }
                        }
                    }
                }
                else
                {
                    foreach (var lg in listLoaiGiuong)
                    {
                        listGiuongHoSo.Add(new LoaiGiuongHoSoModel()
                        {
                            Id = lg.Id,
                            Ten = lg.Ten,
                            DienTich = 0,
                            GiaPhong = 0,
                            SoPhong = 0,
                            TenHienThi = ""
                        });
                    }
                }
                listItem.Add(new LoaiPhongHoSoModel()
                {
                    LoaiPhong = x,
                    DSLoaiGiuong = listGiuongHoSo,
                    LoaiPhongId = x.Id
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

        private List<VeDichVuHoSoModel> ListVeDichVuHoSo(int hosoId = 0, List<VeDichVuHoSoModel> listValue = null)
        {
            var listItem = new List<VeDichVuHoSoModel>();
            if (listValue != null && listValue.Count() > 0)
            {
                foreach (var val in listValue)
                {
                    listItem.Add(new VeDichVuHoSoModel()
                    {
                        GiaVe = val.GiaVe,
                        HosoId = val.HosoId,
                        MoTa = val.MoTa,
                        Id = val.Id,
                        TenVe = val.TenVe
                    });
                }
            }
            else
            {
                listItem.Add(new VeDichVuHoSoModel()
                {
                    GiaVe = 0,
                    HosoId = hosoId,
                    MoTa = "",
                    Id = 0,
                    TenVe = ""
                });
            }
            return listItem;
        }

        private List<QuyMoNhaHangVm> ListNhaHangLuuTru(int hosoId = 0, List<QuyMoNhaHangVm> listValue = null)
        {
            var listItem = new List<QuyMoNhaHangVm>();
            if (listValue != null && listValue.Count() > 0)
            {
                foreach (var val in listValue)
                {
                    listItem.Add(new QuyMoNhaHangVm()
                    {
                        DienTich = val.DienTich,
                        HoSoId = val.HoSoId,
                        SoGhe = val.SoGhe,
                        Id = val.Id,
                        TenGoi = val.TenGoi
                    });
                }
            }
            else
            {
                listItem.Add(new QuyMoNhaHangVm()
                {
                    DienTich = 0,
                    HoSoId = hosoId,
                    SoGhe = 0,
                    Id = 0,
                    TenGoi = ""
                });
            }
            return listItem;
        }

        private List<QuaTrinhHoatDongModel> ListQuaTrinhHoatDong(int hdvId = 0, List<QuaTrinhHoatDongModel> listValue = null)
        {
            var listItem = new List<QuaTrinhHoatDongModel>();
            if (listValue != null && listValue.Count() > 0)
            {
                foreach (var val in listValue)
                {
                    listItem.Add(new QuaTrinhHoatDongModel()
                    {
                        HDVId = hdvId,
                        HoatDong = val.HoatDong,
                        Id = val.Id,
                        KetQua = val.KetQua,
                        ThoiGian = val.ThoiGian
                    });
                }
            }
            else
            {
                listItem.Add(new QuaTrinhHoatDongModel()
                {
                    HDVId = hdvId,
                    HoatDong = "",
                    Id = 0,
                    KetQua = "",
                    ThoiGian = ""
                });
            }
            return listItem;
        }

        [Authorize(Roles = "view_luutru,root")]
        public async Task<IActionResult> Cosoluutru()
        {
            try
            {
                ViewData["Title"] = "Danh sách cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int namecslt = !String.IsNullOrEmpty(Request.Query["namecslt"]) ? Convert.ToInt32(Request.Query["namecslt"]) : -1;

                var loaihinhkinhdoanh = await _loaiHinhService.GetAll();

                var list = loaihinhkinhdoanh.Select(x => new SelectListItem
                {
                    Text = x.TenLoai.ToString(),
                    Value = x.Id.ToString(),
                    Selected = (int)x.Id == loaihinh ? true : false
                });

                ViewBag.listLoaiHinhKD = list;

                await OptionTieuChuanCoSo(hangsao);
               // await OptionHuyen(1, huyen);
                var huyenData= await _diaPhuongService.GetAllByParent(1);

                var huyenItems = huyenData.Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong.ToString(),
                    Value = x.Id.ToString(),
                    Selected = (int)x.Id == huyen ? true : false
                });

                ViewBag.listHuyen = huyenItems;

                /// await OptionGetAllCSLT(namecslt);


                var csdlData = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);

                var csdlItems = csdlData.Select(x => new SelectListItem
                {
                    Text = x.Ten.ToString(),
                    Value = x.Id.ToString(),
                    Selected = (int)x.Id == namecslt ? true : false
                });

                ViewBag.listGetAllCSLT = csdlItems;

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen,
                    namecslt = namecslt,

                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.CoSoLuuTru, pageRequest);

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [HttpGet]
        [Authorize(Roles = "create_luutru,root")]
        public async Task<IActionResult> Themcosoluutru()
        {
            try
            {
                var tienNghi = await _tienNghiService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);
                var csltModel = new DuLieuDuLichModel();
                ViewData["Title"] = "Thêm cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";
                var amenities = tienNghi.Select(x => new Model.DuLieuDuLich.AmenityVm()
                {
                    Id = x.Id,
                    Name = x.Ten
                }).ToList();
                csltModel.DSLoaiPhong = await ListLoaiPhongHoSo();
                csltModel.DSDichVu = await ListDichVuHoSo();
                csltModel.DSNhaHang = ListNhaHangLuuTru();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru);
                csltModel.Amenities = amenities;
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionLoaiHinhKinhDoanh();
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_luutru,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themcosoluutru(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = result.Message,
                    });

                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhKinhDoanh();
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    return View(request);
                }
                if (request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(result.ResultObj.Id, request.Images);
                }

                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(result.ResultObj.Id, request.Files);
                }

                if (request.Docs != null)
                {
                    await _csdlDuLichApiClient.UploadDoc(result.ResultObj.Id, request.Docs);
                }

                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = "Thêm mới thành công",
                });

                await Tracking("Thêm cơ sở lưu trú " + request.DuLieuDuLich.Ten);
                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Suacosoluutru/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
                else
                {
                    return Redirect("/Hoso/Themcosoluutru/");
                }
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhKinhDoanh();
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_luutru,root")]
        public async Task<IActionResult> Suacosoluutru(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";
                var tienNghi = await _tienNghiService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));
                var csltModel = await _duLieuDuLichService.GetById(Id);
                var amenities = tienNghi.Select(x => new Model.DuLieuDuLich.AmenityVm()
                {
                    Id = x.Id,
                    Name = x.Ten,
                    IsSelect = csltModel.Amenities.Select(v => v.Id).Contains(x.Id) ? true : false
                }).ToList();
                if (csltModel != null)
                {
                    csltModel.DSLoaiPhong = await ListLoaiPhongHoSo(csltModel.Id, csltModel.DSLoaiPhong);
                    csltModel.DSDichVu = await ListDichVuHoSo(csltModel.Id, csltModel.DSDichVu);
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSBoPhan);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSTienNghi);
                    csltModel.DSNhaHang = ListNhaHangLuuTru(csltModel.Id, csltModel.DSNhaHang);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSVanBan);
                    csltModel.Amenities = amenities;
                    await OptionHuyen();
                    await OptionXa(csltModel.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhKinhDoanh();
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest(),
                    DSLoaiPhongGiuong = csltModel.DSLoaiPhongGiuong,

                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_luutru,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suacosoluutru(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";

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
                if (result.IsSuccessed)
                {
                    if (request.Images != null)
                    {
                        await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                    }
                    if (request.Files != null)
                    {
                        await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                    }

                    if (request.Docs != null)
                    {
                        await _csdlDuLichApiClient.UploadDoc(request.DuLieuDuLich.Id, request.Docs);
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

                await Tracking("Sửa cơ sở lưu trú " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Suacosoluutru/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                    }
                    else
                    {
                        return Redirect("/Hoso/Themcosoluutru/");
                    }
                }
                else
                {
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhKinhDoanh();
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhKinhDoanh();
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();
                return View(request);
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_luutru,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoacosoluutru(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.CoSoLuuTru);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_nhahang,root")]
        public async Task<IActionResult> Nhahang()
        {
            try
            {
                ViewData["Title"] = "Danh sách nhà hàng";
                ViewData["Title_parent"] = "Hồ sơ";
                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int namenhahang = !String.IsNullOrEmpty(Request.Query["namenhahang"]) ? Convert.ToInt32(Request.Query["namenhahang"]) : -1;

                await OptionLoaiNhaHang(loaihinh);
                await OptionTieuChuanCoSo(hangsao);
                await OptionHuyen(1, huyen);
                await OptionGetAllNhaHang(namenhahang);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen,
                    namenhahang = namenhahang
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.NhaHang, pageRequest);

                return View(data);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_nhahang,root")]
        public async Task<IActionResult> Themmoinhahang()
        {
            try
            {
                ViewData["Title"] = "Thêm mới nhà hàng";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();



                csltModel.DSThucDon = ListThucDonHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.NhaHang);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.NhaHang);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.NhaHang);

                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiNhaHang();


                await OptionNhaCungCap();

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_nhahang,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoinhahang(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);

                if (result.IsSuccessed)
                {
                    if (request.Images != null && request.Images != null)
                    {
                        var upload = await _csdlDuLichApiClient.UploadImage(result.ResultObj.Id, request.Images);
                    }
                    if (request.Files != null)
                    {
                        var upload = await _csdlDuLichApiClient.UploadFile(result.ResultObj.Id, request.Files);
                    }
                }
                await Tracking("Thêm nhà hàng " + request.DuLieuDuLich.Ten);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });

                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Nhahang/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoinhahang/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Themmoinhahang/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/Hoso/Themmoinhahang/");
        }

        [HttpGet]
        [Authorize(Roles = "edit_nhahang,root")]
        public async Task<IActionResult> Suathongtinnhahang(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin nhà hàng";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

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
                    await OptionHuyen();
                    await OptionXa(csltModel.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiNhaHang();
                    await OptionNhaCungCap();
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_nhahang,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtinnhahang(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

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
                    return Redirect("/Hoso/Suathongtinnhahang/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa nhà hàng " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Suathongtinnhahang/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoinhahang/");
                    }
                }
                return Redirect("/Hoso/Suathongtinnhahang/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
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

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/Hoso/Suathongtinnhahang/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_nhahang,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtinnhahang(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.NhaHang);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }


        [Authorize(Roles = "view_congtyluhanh,root")]
        public async Task<IActionResult> Congtyluhanh()
        {
            try
            {
                ViewData["Title"] = "Danh sách công ty lữ hành";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int nameluhanh = !String.IsNullOrEmpty(Request.Query["nameluhanh"]) ? Convert.ToInt32(Request.Query["nameluhanh"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionLoaiCTLuHanh(loaihinh);
                await OptionHuyen(1, huyen);
                await OptionGetAllLuHanh(nameluhanh);
                await OptionNguonDongBo(nguon);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    huyen = huyen,
                    loaihinh = loaihinh,
                    nameluhanh = nameluhanh,
                    nguon = nguon
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.LuHanh, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_congtyluhanh,root")]
        public async Task<IActionResult> Themmoicongtyluhanh()
        {
            try
            {
                ViewData["Title"] = "Thêm mới công ty lữ hành";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh);

                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiCTLuHanh();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_congtyluhanh,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Themmoicongtyluhanh(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(-1);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCTLuHanh();
                    await OptionNhaCungCap();
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm công ty lữ hành " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công!" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Congtyluhanh/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoicongtyluhanh/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiCTLuHanh();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_congtyluhanh,root")]
        public async Task<IActionResult> Suathongtincongtyluhanh(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin công ty lữ hàn   h";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                if (csltModel != null)
                {
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh, csltModel.Id, csltModel.DSBoPhan);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh, csltModel.Id, csltModel.DSVanBan);

                    await OptionHuyen();
                    await OptionXa(csltModel.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCTLuHanh();
                    await OptionNhaCungCap();
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_congtyluhanh,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtincongtyluhanh(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

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
                    await OptionHuyen();
                    await OptionXa(-1);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCTLuHanh();
                    await OptionNhaCungCap();
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa công ty lữ hành " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Congtyluhanh/");
                }
                else
                {
                    return Redirect("/Hoso/Suathongtincongtyluhanh/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiCTLuHanh();
                await OptionNhaCungCap();
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_congtyluhanh,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtincongtyluhanh(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.LuHanh);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }


        [Authorize(Roles = "view_muasam,root")]
        public async Task<IActionResult> Cosomuasam()
        {
            try
            {
                ViewData["Title"] = "Danh sách cơ sở mua sắm";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int namecsms = !String.IsNullOrEmpty(Request.Query["namecsms"]) ? Convert.ToInt32(Request.Query["namecsms"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionLoaiCoSoMuaSam(loaihinh);
                await OptionTieuChuanCoSo(hangsao);
                await OptionHuyen(1, huyen);
                await OptionGetAllCSMS(namecsms);
                await OptionNguonDongBo(nguon);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen,
                    namecsms = namecsms,
                    nguon = nguon,
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.MuaSam, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_muasam,root")]
        public async Task<IActionResult> Themmoicosomuasam()
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở mua sắm";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.MuaSam);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSTienNghi);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.MuaSam);

                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();

                await OptionLoaiCoSoMuaSam();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_muasam,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoicosomuasam(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }


                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(-1);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCoSoMuaSam();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm cơ sở mua sắm " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công!" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Cosomuasam/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoicosomuasam/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiCoSoMuaSam();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_muasam,root")]
        public async Task<IActionResult> Suathongtincosomuasam(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin cơ sở mua sắm";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                if (csltModel != null)
                {
                    csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSBoPhan);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSTienNghi);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSVanBan);

                    await OptionHuyen();
                    await OptionXa(csltModel.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCoSoMuaSam();
                    await OptionNhaCungCap();
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_muasam,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtincosomuasam(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin cơ sở mua sắm";
                ViewData["Title_parent"] = "Hồ sơ";

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
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionTieuChuanCoSo();
                    await OptionLoaiCoSoMuaSam();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa cơ sở mua sắm " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Suathongtincosomuasam/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoicosomuasam/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtincosomuasam/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionTieuChuanCoSo();
                await OptionLoaiCoSoMuaSam();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_muasam,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtincosomuasam(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.MuaSam);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_diemdulich,root")]
        public async Task<IActionResult> Diemdulich()
        {
            try
            {
                ViewData["Title"] = "Danh sách điểm du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int nameddl = !String.IsNullOrEmpty(Request.Query["nameddl"]) ? Convert.ToInt32(Request.Query["nameddl"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionLoaiDiemDuLich(loaihinh);
                await OptionHuyen(1, huyen);
                await OptionGetAllDDL(nameddl);
                await OptionNguonDongBo(nguon);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen,
                    nameddl = nameddl,
                    nguon = nguon
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.DiemDuLich, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_diemdulich,root")]
        public async Task<IActionResult> Themmoidiemdulich()
        {
            try
            {
                ViewData["Title"] = "Thêm mới điểm du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich);

                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiDiemDuLich();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichCreateExtRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadExtRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_diemdulich,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoidiemdulich(DuLieuDuLichCreateExtRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới điểm du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa();
                    await OptionDonViTinh(2);
                    await OptionLoaiDiemDuLich();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    //var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                    //HueCIT
                    var upload = await _csdlDuLichApiClient.UploadImageExt(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }
                await Tracking("Thêm điểm du lịch " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Diemdulich/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoidiemdulich/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiDiemDuLich();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_diemdulich,root")]
        public async Task<IActionResult> Suathongtindiemdulich(string id)
        {
            try
            {
                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                ViewData["Title"] = "Sửa thông tin điểm du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSVanBan);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSTienNghi);
                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiDiemDuLich();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichUpdateExtRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadExtRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_diemdulich,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtindiemdulich(DuLieuDuLichUpdateExtRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin điểm du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

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
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiDiemDuLich();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    //var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                    //HueCIT
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

                await Tracking("Sửa điểm du lịch " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Suathongtindiemdulich/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoidiemdulich/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtindiemdulich/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiDiemDuLich();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_diemdulich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtindiemdulich(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.DiemDuLich);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_huongdanvien,root")]
        public async Task<IActionResult> Huongdanvien()
        {
            try
            {
                ViewData["Title"] = "Hướng dẫn viên du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                //int namehdv = !String.IsNullOrEmpty(Request.Query["namehdv"]) ? Convert.ToInt32(Request.Query["namehdv"]) : -1;           
                //int loaithe = !String.IsNullOrEmpty(Request.Query["loaithe"]) ? Convert.ToInt32(Request.Query["loaithe"]) : -1;           
                //await OptionGetAllHDV(namehdv);
                var pageRequest = new GetPagingRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    namehdv = !String.IsNullOrEmpty(Request.Query["namehdv"]) ? Convert.ToInt32(Request.Query["namehdv"]) : -1,
                    loaithe = !String.IsNullOrEmpty(Request.Query["loaithe"]) ? Convert.ToInt32(Request.Query["loaithe"]) : -1,
                    TinhTrang = !String.IsNullOrEmpty(Request.Query["TinhTrang"]) ? Request.Query["TinhTrang"].ToString() : "",

                };

                var data = await _huongDanVienService.GetPaging(pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_huongdanvien,root")]
        public async Task<IActionResult> Themhuongdanvien()
        {
            ViewData["Title"] = "Thêm hướng dẫn viên du lịch";
            ViewData["Title_parent"] = "Hồ sơ";

            var hdvModel = new HuongDanVienModel();
            hdvModel.DSQuaTrinhHD = ListQuaTrinhHoatDong();
            hdvModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.HDV);

            await OptionNgoaiNgu();
            await OptionLoaiTheHDV();
            await OptionLoaiDiemDuLich();
            await OptionCongTyLuHanh();


            var model = new HuongDanVienRequest()
            {
                HuongDanVien = hdvModel,
                Images = new ImageUploadRequest()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "create_huongdanvien,root")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Themhuongdanvien(HuongDanVienRequest request)
        {
            try
            {
                ViewData["Title"] = "Thêm hướng dẫn viên du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                if (request.HuongDanVien.DSVanBan != null)
                {
                    foreach (var v in request.HuongDanVien.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : v.FilePath;
                        v.FileName = v.Files != null ? v.Files.FileName : v.FileName;
                    }
                }
                var result = await _huongDanVienService.Create(request.HuongDanVien);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null)
                {
                    var upload = await _huongDanVienApiClient.UploadImage(request.HuongDanVien.Id, request.Images);

                    if (!upload.IsSuccessed)
                    {
                        ModelState.AddModelError("", upload.Message);
                        return View(request);
                    }
                }
                await Tracking("Thêm hướng dẫn viên " + request.HuongDanVien.HoVaTen);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });
                return RedirectToAction("Huongdanvien");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [Authorize(Roles = "edit_huongdanvien,root")]
        public async Task<IActionResult> Suahuongdanvien(string id)
        {
            ViewData["Title"] = "Sửa thông tin hướng dẫn viên";
            ViewData["Title_parent"] = "Hồ sơ";

            var hdvModel = await _huongDanVienService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            hdvModel.DSQuaTrinhHD = ListQuaTrinhHoatDong(hdvModel.Id, hdvModel.DSQuaTrinhHD);
            hdvModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.HDV, hdvModel.Id, hdvModel.DSVanBan);

            await OptionNgoaiNgu();
            await OptionLoaiTheHDV();
            await OptionLoaiDiemDuLich();
            await OptionCongTyLuHanh();

            var model = new HuongDanVienRequest()
            {
                HuongDanVien = hdvModel,
                Images = new ImageUploadRequest()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "edit_huongdanvien,root")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Suahuongdanvien(HuongDanVienRequest request)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin hướng dẫn viên";
                ViewData["Title_parent"] = "Hồ sơ";

                if (request.HuongDanVien.DSVanBan != null)
                {
                    foreach (var v in request.HuongDanVien.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : v.FilePath;
                        v.FileName = v.Files != null ? v.Files.FileName : v.FileName;
                    }
                }
                var result = await _huongDanVienService.Update(request.HuongDanVien.Id, request.HuongDanVien);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null)
                {
                    var upload = await _huongDanVienApiClient.UploadImage(request.HuongDanVien.Id, request.Images);

                    if (!upload.IsSuccessed)
                    {
                        ModelState.AddModelError("", upload.Message);
                        return View(request);
                    }
                }

                await Tracking("Sửa hướng dẫn viên " + request.HuongDanVien.HoVaTen);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });
                return RedirectToAction("Huongdanvien");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [Authorize(Roles = "manage_travel_data,root")]
        public async Task<IActionResult> Chitiet_Huongdanvien(string id)
        {
            ViewData["Title"] = "Chi tiết hướng dẫn viên du lịch";
            ViewData["Title_parent"] = "Hồ sơ";

            var loaihinh = await _danhMucApiClient.GetAll((int)LinhVucKinhDoanh.DiemDuLich);
            var ngonngu = await _ngoaiNguApiClient.GetAll();


            var data = await _huongDanVienService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));
            data.LoaiHinh = loaihinh.Where(v => data.LoaiHinhId.Contains(v.Id)).Select(v => v.Ten).ToList();
            data.NgonNgu = ngonngu.Where(v => data.NgonNguId.Contains(v.Id)).Select(v => v.TenNgoaiNgu).ToList();
            data.LoaiThe = data.LoaiTheId == 1 ? "Thẻ nội địa" : "Thẻ quốc tế";
            return View(data);
        }

        [Authorize(Roles = "manage_travel_data")]
        public async Task<IActionResult> Option_huongdanvien(string term)
        {
            var data = await _huongDanVienService.GetAll(term);
            return Ok(data);
        }

        public async Task<IActionResult> Option_CSLT(string term)
        {

            var data = await _duLieuDuLichService.GetAllCSLT(term);
            return Ok(data);
        }
        [HttpPost]
        [Authorize(Roles = "manage_travel_data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoahoso(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }
        [HttpPost]
        [Authorize(Roles = "delete_huongdanvien,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoa_huongdanvien(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _huongDanVienService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_vesinhcongcong,root")]
        public async Task<IActionResult> Vesinhcongcong()
        {
            try
            {
                ViewData["Title"] = "Điểm vệ sinh công cộng";
                ViewData["Title_parent"] = "Hồ sơ";

                var pageRequest = new GetPagingRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    NguonDongBo = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1
                };
                OptionDongBoDiem(pageRequest.NguonDongBo ?? -1);
                var data = await _diemVeSinhApiClient.GetPagings(pageRequest);

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
          
        }

        [Authorize(Roles = "create_vesinhcongcong,root")]
        public IActionResult Themdiemvesinh()
        {
            ViewData["Title"] = "Thêm    điểm vệ sinh công cộng";
            ViewData["Title_parent"] = "Hồ sơ";

            var hdvModel = new DiemVeSinhModel();

            return View(hdvModel);
        }

        [HttpPost]
        [Authorize(Roles = "create_vesinhcongcong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themdiemvesinh(DiemVeSinhModel request)
        {
            try
            {
                ViewData["Title"] = "Thêm điểm vệ sinh công cộng";
                ViewData["Title_parent"] = "Hồ sơ";

                var result = await _diemVeSinhApiClient.Create(request);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                await Tracking("Thêm điểm vệ sinh " + request.MoTa);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });
                return RedirectToAction("Vesinhcongcong");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [Authorize(Roles = "edit_vesinhcongcong,root")]
        public async Task<IActionResult> Suadiemvesinh(int id)
        {
            ViewData["Title"] = "Sửa thông tin điểm vệ sinh";
            ViewData["Title_parent"] = "Hồ sơ";

            var hdvModel = await _diemVeSinhApiClient.GetById(id);

            return View(hdvModel);
        }

        [HttpPost]
        [Authorize(Roles = "edit_vesinhcongcong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadiemvesinh(DiemVeSinhModel request)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin điểm vệ sinh";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _diemVeSinhApiClient.GetById(request.Id);
                request.X = csltModel.X;
                request.Y = csltModel.Y;

                var result = await _diemVeSinhApiClient.Update(request.Id, request);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (result.IsSuccessed && request.X != null && request.X != 0 && request.Y != null && request.Y != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.Y,
                        y = request.X
                    };

                    TechLife.Model.HueCIT.DiemVeSinhDongBoAdd data = new Model.HueCIT.DiemVeSinhDongBoAdd
                    {
                        id = request.Id,
                        ten = request.Ten,
                        vitri = request.ViTri,
                        hientrang = request.HienTrang,
                        mota = request.MoTa,
                    };

                    AddEditVeSinhGIS(data, geo);
                }

                await Tracking("Sửa điểm vệ sinh " + request.MoTa);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = result.Message });
                return RedirectToAction("Vesinhcongcong");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_vesinhcongcong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadiemvesinh(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _diemVeSinhApiClient.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, 1105);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_khudulich,root")]
        public async Task<IActionResult> Khudulich()
        {
            try
            {
                ViewData["Title"] = "Danh sách khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;

                await OptionLoaiKhuDuLich(loaihinh);
                await OptionHuyen(1, huyen);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.KhuDuLich, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_khudulich,root")]
        public async Task<IActionResult> Themmoikhudulich()
        {
            try
            {
                ViewData["Title"] = "Thêm mới khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich);

                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiKhuDuLich();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_khudulich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoikhudulich(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }

                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa();
                    await OptionDonViTinh(2);
                    await OptionLoaiKhuDuLich();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm khu du lịch " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Khudulich/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoikhudulich/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiKhuDuLich();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_khudulich,root")]
        public async Task<IActionResult> Suathongtinkhudulich(int id)
        {
            try
            {
                var csltModel = await _duLieuDuLichService.GetById(id);

                ViewData["Title"] = "Sửa thông tin khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSVanBan);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSTienNghi);
                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiKhuDuLich();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_khudulich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtinkhudulich(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";
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

                var result = await _csdlDuLichApiClient.Update(request.DuLieuDuLich.Id, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiKhuDuLich();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Sửa khu du lịch " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Khudulich/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoikhudulich/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtinkhudulich/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiKhuDuLich();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_khudulich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtinkhudulich(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_dichvuvuichoi,root")]
        public async Task<IActionResult> Vcgt()
        {
            try
            {
                ViewData["Title"] = "Danh sách khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionLoaiKhuVuiChoi(loaihinh);
                await OptionHuyen(1, huyen);
                await OptionNguonDongBo(nguon);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen,
                    nguon = nguon
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.KhuVuiChoi, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_dichvuvuichoi,root")]
        public async Task<IActionResult> Themmoikhuvcgt()
        {
            try
            {
                ViewData["Title"] = "Thêm mới khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuVuiChoi);

                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiKhuVuiChoi();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_dichvuvuichoi,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoikhuvcgt(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }
                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa();
                    await OptionDonViTinh(2);
                    await OptionLoaiKhuVuiChoi();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm khu vui chơi, giải trí " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Vcgt/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoikhuvcgt/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiKhuVuiChoi();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvuvuichoi,root")]
        public async Task<IActionResult> Suathongtinkhuvcgt(int id)
        {
            try
            {
                var csltModel = await _duLieuDuLichService.GetById(id);

                ViewData["Title"] = "Sửa thông tin khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);

                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSVanBan);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSTienNghi);


                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiKhuVuiChoi();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_dichvuvuichoi,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtinkhuvcgt(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";

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
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiKhuVuiChoi();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa khu vui chơi, giải trí " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Vcgt/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoikhuvcgt/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtinkhuvcgt/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiKhuVuiChoi();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_dichvuvuichoi,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtinkhuvcgt(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.KhuVuiChoi);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_dichvucssk,root")]
        public async Task<IActionResult> Cssk()
        {
            try
            {
                ViewData["Title"] = "Danh sách cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;

                await OptionLoaiHinhCSSK(loaihinh);
                await OptionHuyen(1, huyen);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.CSSK, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_dichvucssk,root")]
        public async Task<IActionResult> Themmoicssk()
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();

                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CSSK);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CSSK);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CSSK);

                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiHinhCSSK();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_dichvucssk,root")]
        public async Task<IActionResult> Themmoicssk(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }
                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa();
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhCSSK();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm cơ sở chăm sóc sức khỏe " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Cssk/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoicssk/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiHinhCSSK();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvucssk,root")]
        public async Task<IActionResult> Suathongtincssk(int id)
        {
            try
            {
                var csltModel = await _duLieuDuLichService.GetById(id);

                ViewData["Title"] = "Sửa thông tin cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSVanBan);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSTienNghi);
                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhCSSK();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_dichvucssk,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtincssk(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

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
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhCSSK();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa cơ sở chăm sóc sức khỏe " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Cssk/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoicssk/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtincssk/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhCSSK();
                await OptionNhaCungCap();
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_dichvucssk,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtincssk(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.CSSK);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_dichvuthethao,root")]
        public async Task<IActionResult> Thethao()
        {
            try
            {
                ViewData["Title"] = "Danh sách cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int hangsao = !String.IsNullOrEmpty(Request.Query["hangsao"]) ? Convert.ToInt32(Request.Query["hangsao"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;

                await OptionLoaiHinhTheThao(loaihinh);
                await OptionHuyen(1, huyen);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    huyen = huyen
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.TheThao, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_dichvuthethao,root")]
        public async Task<IActionResult> Themmoicstt()
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.TheThao);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.TheThao);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.TheThao);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();


                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiHinhTheThao();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_dichvuthethao,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoicstt(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }
                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa();
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhTheThao();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm cơ sở thể thao " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Thethao/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoicstt/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa();
                await OptionDonViTinh(2);
                await OptionLoaiHinhTheThao();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvuthethao,root")]
        public async Task<IActionResult> Suathongtincstt(int id)
        {
            try
            {
                var csltModel = await _duLieuDuLichService.GetById(id);

                ViewData["Title"] = "Sửa thông tin cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSBoPhan);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);

                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSVanBan);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSTienNghi);

                await OptionHuyen();
                await OptionXa(csltModel.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhTheThao();
                await OptionNhaCungCap();

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_dichvuthethao,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suathongtincstt(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

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

                var result = await _csdlDuLichApiClient.Update(request.DuLieuDuLich.Id, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiHinhTheThao();
                    await OptionNhaCungCap();

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa cơ sở thể thao " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Thethao/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoicstt/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suathongtincstt/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                await OptionDonViTinh(2);
                await OptionLoaiHinhTheThao();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_dichvuthethao,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathongtincstt(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.TheThao);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_luutru,view_nhahang" +
            ",view_diemdulich,view_khudulich" +
            ",view_dichvuvuichoi,view_vesinhcongcong" +
            ",view_dichvuthethao,view_dichvucssk" +
            ",root")]

        public async Task<IActionResult> Chitiet(string id)
        {

            var model = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            if (model != null)
            {
                ViewData["Title"] = "Thông tin hồ sơ " + model.Ten;
            }

            ViewData["Title_parent"] = "Hồ sơ";

            return View(model);
        }

        [Authorize(Roles = "view_donviquanly,root")]
        public async Task<IActionResult> Nhacungcap()
        {
            try
            {
                ViewData["Title"] = "Danh sách đơn vị quản lý";
                ViewData["Title_parent"] = "Hồ sơ";

                var pageRequest = new GetPagingRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                };

                var data = await _nhaCungCapService.GetPaging(pageRequest);

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_donviquanly,root")]
        public IActionResult Themnhacungcap()
        {
            try
            {
                ViewData["Title"] = "Thêm mới đơn vị quản lý";
                ViewData["Title_parent"] = "Hồ sơ";

                return View();
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "create_donviquanly,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themnhacungcap(NhaCungCapCreateRequets request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Thêm mới đơn vị quản lý";
                ViewData["Title_parent"] = "Hồ sơ";

                var result = await _nhaCungCapService.Create(request);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                await Tracking("Thêm nhà đơn vị quản lý " + request.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Nhacungcap/");
                }
                else
                {
                    return Redirect("/Hoso/Themnhacungcap/");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_donviquanly,root")]
        public async Task<IActionResult> Suanhacungcap(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin nhà đơn vị quản lý";
                ViewData["Title_parent"] = "Hồ sơ";

                var obj = await _nhaCungCapService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                var model = new NhaCungCapUpdateRequets()
                {
                    Id = obj.Id,
                    ChucVu = obj.ChucVu,
                    EmailDoanhNghiep = obj.EmailDoanhNghiep,
                    EmailNguoiDaiDien = obj.EmailNguoiDaiDien,
                    MaSoDoanhNghiep = obj.MaSoDoanhNghiep,
                    MoTa = obj.MoTa,
                    NgayDangKy = obj.NgayDangKy,
                    SDTDoanhNghiep = obj.SDTDoanhNghiep,
                    SDTNguoiDaiDien = obj.SDTNguoiDaiDien,
                    Ten = obj.Ten,
                    TenNguoiDaiDien = obj.TenNguoiDaiDien
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Authorize(Roles = "edit_donviquanly,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suanhacungcap(NhaCungCapUpdateRequets request, string type_sumit)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin đơn vị quản lý";
                ViewData["Title_parent"] = "Hồ sơ";

                var result = await _nhaCungCapService.Update(request.Id, request);
                if (!result.IsSuccessed)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                await Tracking("Sửa nhà cung cấp " + request.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });
                if (result.IsSuccessed)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/Hoso/Nhacungcap/");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themnhacungcap/");
                    }
                }
                else
                {
                    return Redirect("/Hoso/Suanhacungcap/?id=" + HashUtil.EncodeID(request.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_donviquanly,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoanhacungcap(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _nhaCungCapService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        [Authorize(Roles = "view_congtyvanchuyen,root")]
        public async Task<IActionResult> Vanchuyen()
        {
            try
            {
                ViewData["Title"] = "Danh sách công ty vận chuyển";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionLoaiCTVanChuyen(loaihinh);
                await OptionHuyen(1, huyen);
                await OptionNguonDongBo(nguon);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    huyen = huyen,
                    nguon = nguon,
                    loaihinh = loaihinh
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.VanChuyen, pageRequest);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        [Authorize(Roles = "create_congtyvanchuyen,root")]
        public async Task<IActionResult> Themmoivanchuyen()
        {
            try
            {
                ViewData["Title"] = "Thêm mới công ty lữ hành";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.VanChuyen);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.VanChuyen);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.VanChuyen);

                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionLoaiCTVanChuyen();
                await OptionNhaCungCap();
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_congtyvanchuyen,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Themmoivanchuyen(DuLieuDuLichCreateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");
                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : "";
                        v.FileName = v.Files != null ? v.Files.FileName : "";
                    }
                }
                var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(-1);
                    await OptionDonViTinh(2);
                    await OptionLoaiCTVanChuyen();
                    await OptionNhaCungCap();
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
                }
                if (request.Files != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadFile(request.DuLieuDuLich.Id, request.Files);
                }

                await Tracking("Thêm công ty vận chuyển " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công!" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Vanchuyen/");
                }
                else
                {
                    return Redirect("/Hoso/Themmoivanchuyen/");
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionLoaiCTVanChuyen();
                await OptionNhaCungCap();

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_congtyvanchuyen,root")]
        public async Task<IActionResult> Suacongtyvanchuyen(string id)
        {
            try
            {
                ViewData["Title"] = "Sửa thông tin công ty vận chuyển";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                if (csltModel != null)
                {
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.VanChuyen, csltModel.Id, csltModel.DSBoPhan);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.VanChuyen, csltModel.Id, csltModel.DSTienNghi);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.VanChuyen, csltModel.Id, csltModel.DSVanBan);

                    await OptionHuyen();
                    await OptionXa(csltModel.QuanHuyenId);
                    await OptionDonViTinh(2);
                    await OptionLoaiCTVanChuyen();
                    await OptionNhaCungCap();
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_congtyvanchuyen,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suacongtyvanchuyen(DuLieuDuLichUpdateRequest request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuDuLich.Id");

                var csltModel = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                request.DuLieuDuLich.ToaDoX = csltModel.ToaDoX;
                request.DuLieuDuLich.ToaDoY = csltModel.ToaDoY;

                if (request.DuLieuDuLich.DSVanBan != null)
                {
                    foreach (var v in request.DuLieuDuLich.DSVanBan)
                    {
                        v.FilePath = v.Files != null ? await _fileApiClient.Upload(v.Files) : v.FilePath;
                        v.FileName = v.Files != null ? v.Files.FileName : v.FileName;
                    }
                }
                var result = await _duLieuDuLichService.Update(request.DuLieuDuLich.Id, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    await OptionHuyen();
                    await OptionXa(-1);
                    await OptionDonViTinh(2);
                    await OptionLoaiCTVanChuyen();
                    await OptionNhaCungCap();
                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }

                if (request.Images != null && request.Images != null)
                {
                    var upload = await _csdlDuLichApiClient.UploadImage(request.DuLieuDuLich.Id, request.Images);
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

                await Tracking("Sửa công ty lữ hành " + request.DuLieuDuLich.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (type_sumit == "save")
                {
                    return Redirect("/Hoso/Vanchuyen/");
                }
                else
                {
                    return Redirect("/Hoso/Suacongtyvanchuyen/?id=" + HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionHuyen();
                await OptionXa(-1);
                await OptionDonViTinh(2);
                await OptionLoaiCTVanChuyen();
                await OptionNhaCungCap();
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }
        [HttpPost]
        [Authorize(Roles = "delete_congtyvanchuyen,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoacongtyvanchuyen(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _duLieuDuLichService.Delete(id);

            if (result.IsSuccessed)
            {
                RemoveGIS(Id, (int)LinhVucKinhDoanh.VanChuyen);
            }

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

        public async Task<IActionResult> GetInfoDanhGia(int id)
        {
            var data = await _danhGiaService.GetByID(id);

            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> Suadanhgia(int id)
        {

            var data = await _danhGiaService.GetByID(id);

            await OptionTieuChuanDanhGia(data.SoSao);

            return PartialView("Suadanhgia", data);
        }

        [HttpGet]
        [Authorize(Roles = "manage_travel_data,root")]
        public async Task<IActionResult> Xoadanhgia(int id)
        {
            var data = await _danhGiaService.GetByID(id);

            return PartialView("Xoadanhgia", data);
        }

        [HttpPost]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suadanhgia(DanhGiaModel request)
        {
            try
            {
                var result = await _danhGiaService.Update(request);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (result.IsSuccessed)
                {
                    await Tracking("Sửa đánh giá " + request.HoVaTen);

                    return Redirect(Request.GetBackUrl());
                }
                else
                {
                    return Redirect(Request.GetBackUrl());
                }
            }
            catch (Exception ex)
            {
                return Redirect(Request.GetBackUrl());
            }
        }

        [HttpPost]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoadanhgia(DanhGiaModel request)
        {
            try
            {
                var result = await _danhGiaService.Delete(request.Id);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (result.IsSuccessed)
                {
                    await Tracking(result.ResultObj);

                    return Redirect(Request.GetBackUrl());
                }
                else
                {
                    return Redirect(Request.GetBackUrl());
                }
            }
            catch (Exception ex)
            {
                return Redirect(Request.GetBackUrl());
            }
        }

        [HttpPost]
        [Authorize(Roles = "manage_travel_data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoanhahang_luutru(int id)
        {
            var result = await _duLieuDuLichService.DeleteNhaHangLuuTru(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Authorize(Roles = "manage_travel_data,root")]
        public async Task<IActionResult> ChangeAvata(int id)
        {
            var obj = await _fileUploadService.GetFileById(id);

            var result = await _fileUploadService.SetIsAvata(obj);

            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoaanh(int id)
        {

            var result = await _fileUploadService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload_Cosoluutru(IFormFile file)
        {

            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            //get file path 
            var filePath = Path.Combine(MainPath, file.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //get extension
            string extension = Path.GetExtension(filename);


            string conString = string.Empty;

            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }

            var list = new List<DuLieuDuLichImport>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int loai = Convert.ToInt32(dt.Rows[i][0].ToString());
                int huyen = Convert.ToInt32(dt.Rows[i][1].ToString());
                string ten = dt.Rows[i][2].ToString();
                int sao = Convert.ToInt32(dt.Rows[i][3].ToString());
                string diachi = dt.Rows[i][4].ToString();
                string sdt = dt.Rows[i][5].ToString();
                string fax = dt.Rows[i][6].ToString();
                int sophong = !String.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? Convert.ToInt32(dt.Rows[i][7].ToString()) : 0;
                int sogiuong = !String.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? Convert.ToInt32(dt.Rows[i][8].ToString()) : 0;
                var obj = new DuLieuDuLichImport()
                {
                    DiaChi = diachi,
                    SoDienThoai = sdt,
                    Ten = ten,
                    LoaiHinh = loai,
                    SoGiuong = sogiuong,
                    SoPhong = sophong,
                    Sao = sao,
                    Fax = fax,
                    Huyen = huyen,
                    LangId = Request.GetLanguageId()
                };
                list.Add(obj);
            }

            var result = await _duLieuDuLichService.ImportLuuTru(list);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload_Congtyluhanh(IFormFile file)
        {

            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            //get file path 
            var filePath = Path.Combine(MainPath, file.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //get extension
            string extension = Path.GetExtension(filename);


            string conString = string.Empty;

            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }

            var list = new List<DuLieuDuLichImport>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int loai = Convert.ToInt32(dt.Rows[i][0].ToString());
                int huyen = Convert.ToInt32(dt.Rows[i][1].ToString());
                string ten = dt.Rows[i][2].ToString();
                int sao = Convert.ToInt32(dt.Rows[i][3].ToString());
                string diachi = dt.Rows[i][4].ToString();
                string sdt = dt.Rows[i][5].ToString();
                string fax = dt.Rows[i][6].ToString();
                int sophong = !String.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? Convert.ToInt32(dt.Rows[i][7].ToString()) : 0;
                int sogiuong = !String.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? Convert.ToInt32(dt.Rows[i][8].ToString()) : 0;
                string email = dt.Rows[i][9].ToString();
                string lienhe = dt.Rows[i][10].ToString();
                var obj = new DuLieuDuLichImport()
                {
                    DiaChi = diachi,
                    SoDienThoai = sdt,
                    Ten = ten,
                    LoaiHinh = loai,
                    SoGiuong = sogiuong,
                    SoPhong = sophong,
                    Sao = sao,
                    Fax = fax,
                    Huyen = huyen,
                    Email = email,
                    LienHe = lienhe,
                    LangId = Request.GetLanguageId()
                };
                list.Add(obj);
            }

            var result = await _duLieuDuLichService.ImportLuHanh(list);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return Redirect(Request.GetBackUrl());
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "manage_travel_data,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload_Huongdanvien(IFormFile file)
        {

            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            //get file path 
            var filePath = Path.Combine(MainPath, file.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //get extension
            string extension = Path.GetExtension(filename);


            string conString = string.Empty;

            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }

            var list = new List<HuongDanVienImport>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int loai = Convert.ToInt32(dt.Rows[i][0].ToString());
                string ten = dt.Rows[i][1].ToString();
                DateTime ngaysinh = !String.IsNullOrEmpty(dt.Rows[i][2].ToString())
                                    ? Convert.ToDateTime(Functions.ConvertDateToSql(dt.Rows[i][2].ToString()))
                                    : DateTime.MinValue;

                string gioitinh = dt.Rows[i][3].ToString();
                string cmnd = dt.Rows[i][4].ToString();
                string email = dt.Rows[i][5].ToString();
                string sothe = dt.Rows[i][6].ToString();

                DateTime ngaycap = !String.IsNullOrEmpty(dt.Rows[i][7].ToString())
                                ? Convert.ToDateTime(Functions.ConvertDateToSql(dt.Rows[i][7].ToString()))
                                : DateTime.MinValue;
                DateTime ngayhethan = !String.IsNullOrEmpty(dt.Rows[i][8].ToString())
                                ? Convert.ToDateTime(Functions.ConvertDateToSql(dt.Rows[i][8].ToString()))
                                : DateTime.MinValue;
                var obj = new HuongDanVienImport()
                {
                    LoaiTheId = loai,
                    HoVaTen = ten,
                    NgaySinh = ngaysinh,
                    GioiTinh = gioitinh,
                    CMND = cmnd,
                    Email = email,
                    SoTheHDV = sothe,
                    NgayCapThe = ngaycap,
                    NgayHetHan = ngayhethan,
                    NgayCapCMND = DateTime.MinValue

                };
                list.Add(obj);
            }


            var result = await _huongDanVienService.ImportHuongDanVien(list);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Authorize(Roles = "manage_travel_data,root")]
        public async Task<IActionResult> Xoafile_vanban(int id)
        {
            try
            {
                var result = await _duLieuDuLichService.Delete_File_Vanban(id);
                TempData.AddAlert(new Result<string>() { IsSuccessed = result.IsSuccessed, Message = "Cập nhật thành công" });

                if (result.IsSuccessed)
                {
                    await Tracking(result.Message);

                    return Redirect(Request.GetBackUrl());
                }
                else
                {
                    return Redirect(Request.GetBackUrl());
                }
            }
            catch (Exception ex)
            {
                return Redirect(Request.GetBackUrl());
            }
        }

        private void AddEditGIS(TechLife.Model.HueCIT.HoSoDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            int layer = ReturnLayer(data.linhvuckin);
            string ctk = _config.GetValue<string>("ArcGisToken");
            List<TechLife.Model.HueCIT.DongBoDieuHanhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
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
                        loaihinhid = layer == 4 ? ReturnLoaiHinhDiemDuLich(data.loaihinhid) : data.loaihinhid,
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
                        loaihinhid = layer == 4 ? ReturnLoaiHinhDiemDuLich(data.loaihinhid) : data.loaihinhid,
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

        private void AddEditVeSinhGIS(TechLife.Model.HueCIT.DiemVeSinhDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            int layer = 11;
            string ctk = _config.GetValue<string>("ArcGisToken");
            List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo.objectIds == null)
            {
                TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd
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
                    attributes = new TechLife.Model.HueCIT.DiemVeSinhDongBoAdd
                    {
                        id = data.id,
                        ten = data.ten,
                        vitri = data.vitri,
                        hientrang = data.hientrang,
                        mota = data.mota,
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
                TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit dt = new TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit
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
                    attributes = new TechLife.Model.HueCIT.DiemVeSinhDongBoEdit
                    {
                        objectid = chkinfo.objectIds.First(),
                        id = data.id,
                        ten = data.ten,
                        vitri = data.vitri,
                        hientrang = data.hientrang,
                        mota = data.mota,
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

            int layer = ReturnLayer(linhvuc);
            string ctk = _config.GetValue<string>("ArcGisToken");
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

        private int ReturnLayer(int loai)
        {
            if (loai == (int)LinhVucKinhDoanh.CoSoLuuTru)
            {
                return 0;
            }
            else if (loai == (int)LinhVucKinhDoanh.LuHanh)
            {
                return 1;
            }
            else if (loai == (int)LinhVucKinhDoanh.MuaSam)
            {
                return 2;
            }
            else if (loai == (int)LinhVucKinhDoanh.NhaHang)
            {
                return 3;
            }
            else if (loai == (int)LinhVucKinhDoanh.DiemDuLich)
            {
                return 4;
            }
            else if (loai == (int)LinhVucKinhDoanh.KhuVuiChoi)
            {
                return 5;
            }
            else if (loai == (int)LinhVucKinhDoanh.CSSK)
            {
                return 6;
            }
            else if (loai == (int)LinhVucKinhDoanh.TheThao)
            {
                return 7;
            }
            else if (loai == (int)LinhVucKinhDoanh.VanChuyen)
            {
                return 8;
            }
            else if (loai == (int)LinhVucKinhDoanh.DiSanVanHoa)
            {
                return 9;
            }
            else if (loai == 1105)   //VeSinh
            {
                return 11;
            }
            return 0;
        }

        private int ReturnLoaiHinhDiemDuLich(int input)
        {
            if (input == 6)
            {
                return 561;
            }
            else if (input == 9)
            {
                return 562;
            }
            else if (input == 10)
            {
                return 563;
            }
            else if (input == 11)
            {
                return 564;
            }
            else if (input == 12)
            {
                return 565;
            }
            return input;
        }

        public void OptionDongBoDiem(int seletedId = -1)
        {
            List<StaticModel> dongbo = new List<StaticModel>
            {
                new StaticModel { Id = 0, Ten = "Cập nhật" },
                new StaticModel { Id = 1, Ten = "Đồng bộ" },
            };

            var list = dongbo.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = x.Id == seletedId
            });

            ViewBag.listNguonDongBo = list;
        }
    }
}