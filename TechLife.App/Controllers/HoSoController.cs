using DocumentFormat.OpenXml.Vml.Office;
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
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model;
using TechLife.Model.BoPhan;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.GiayPhepChungChi;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HuongDanVien;
using TechLife.Model.LichSuCapNhat;
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
        private readonly IDanhMucService _danhMucService;
        private readonly ILichSuCapNhatService _lichSuCapNhatService;
        private readonly INgonNguService _ngonNguService;
        private readonly IDonViTinhService _donViTinhService;
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
            , ILoaiDichVuService loaiDichVuService
            , IDonViTinhService donViTinhService
            , IDichVuService dichVuService
            , INgoaiNguService ngoaiNguService
            , ITrinhDoService trinhDoService
            , IMucDoThongThaoNgoaiNguService mucDoThongThaoNgoaiNguService
            , ILoaiPhongService loaiPhongService
            , ILoaiGiuongService loaiGiuongService
            , ILoaiHinhService loaiHinhService
            , IDanhMucService danhMucService
            , ILichSuCapNhatService lichSuCapNhatService
            , INgonNguService ngonNguService
            , ILogger<HoSoController> logger

            )
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
            _danhMucService = danhMucService;
            _lichSuCapNhatService = lichSuCapNhatService;
            _ngonNguService = ngonNguService;
            _donViTinhService = donViTinhService;
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

        private async Task<List<NgoaiNguHoSoModel>> ListNgoaiNguHoSo(int hosoId = 0, List<NgoaiNguHoSoModel> listValue = null, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            List<NgoaiNguModel> list = await _ngoaiNguService.GetAll(ngonNguId);
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

        private async Task<List<TrinhDoHoSoModel>> ListTrinhDoHoSo(int hosoId = 0, List<TrinhDoHoSoModel> listValue = null, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            List<TrinhDoModel> list = await _trinhDoService.GetAll(ngonNguId);
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

        private async Task<List<BoPhanHoSoModel>> ListBoPhanHoSo(int linhvucId, int hosoId = 0, List<BoPhanHoSoModel> listValue = null, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            List<BoPhanVm> list = await _boPhanService.GetAll(linhvucId, ngonNguId);
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

        private async Task<List<MucDoTTNNHoSoModel>> ListMucDoThongThaoHoSo(int hosoId = 0, List<MucDoTTNNHoSoModel> listValue = null, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            List<MucDoThongThaoNgoaiNguModel> list = await _mucDoThongThaoNgoaiNguService.GetAll(ngonNguId);

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

        private async Task<List<TienNghiHoSoModel>> ListMucTienNghiHoSo(int linhvucId, int hosoId = 0, List<TienNghiHoSoModel> listValue = null, string NgonNguId = SystemConstants.DefaultLanguage)
        {
            var listItem = new List<TienNghiHoSoModel>();
            List<TienNghiVm> list = await _tienNghiService.GetAll(linhvucId, NgonNguId);
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

        private async Task<List<HoSoVanBanVm>> ListVanBanHoSo(int linhvucId, int hosoId = 0, List<HoSoVanBanVm> listValue = null, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var listItem = new List<HoSoVanBanVm>();
            List<GiayPhepVm> list = await _giayPhepService.GetAll(linhvucId, ngonNguId);
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
                    IsStatus = value != null && value.IsStatus,
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

        private async Task OptionDonViTinh(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var donvitinh = await _donViTinhService.GetAll(ngonNguId);

            var list = donvitinh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listDonViTinh = list;
        }

        private async Task OptionLoaiHinhKinhDoanh(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var loaihinhkinhdoanh = await _loaiHinhService.GetAll(ngonNguId);

            var list = loaihinhkinhdoanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = x.Id == seletedId
            });

            ViewBag.listLoaiHinhKD = list;
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
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
                var diaPhuongData = await _diaPhuongService.GetHierarchy();

                var diaPhuongOption = diaPhuongData.Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong.ToString(),
                    Value = x.Id.ToString(),
                    Selected = (int)x.Id == phuongXa ? true : false
                });

                ViewBag.DiaPhuongOption = diaPhuongOption;

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
                    PhuongXa = phuongXa,
                    namecslt = namecslt,

                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.CoSoLuuTru, pageRequest);

                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);

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
                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                var tienNghi = await _tienNghiService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru, ngonNguId);
                var csltModel = new DuLieuDuLichModel();
                ViewData["Title"] = "Thêm cơ sở lưu trú";
                ViewData["Title_parent"] = "Hồ sơ";
                var amenities = tienNghi.Select(x => new AmenityVm()
                {
                    Id = x.Id,
                    Name = x.Ten
                }).ToList();
                csltModel.DSLoaiPhong = await ListLoaiPhongHoSo();
                csltModel.DSDichVu = await ListDichVuHoSo();
                csltModel.DSNhaHang = ListNhaHangLuuTru();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, 0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, 0, null, ngonNguId);
                csltModel.Amenities = amenities;

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                //await OptionHuyen();
                //await OptionXa(-1);

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiHinhKinhDoanh(0, ngonNguId);
                await this.OptionDonViTinh(2, ngonNguId);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest(),
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int Id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(Id);
                        ViewData["Title"] = data.Ten;
                        return View("ThemcosoluutruEnglish", model);
                    }
                }

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
        public async Task<IActionResult> Themcosoluutru(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = result.Message,
                    });

                    //await OptionHuyen();
                    //await OptionXa(request.DuLieuDuLich.QuanHuyenId);

                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiHinhKinhDoanh();
                    await this.OptionDonViTinh(2);

                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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

                var history = new LichSuCapNhatCreateRequest
                {
                    HoSoId = result.ResultObj.Id,
                    OldValue = new DuLieuDuLichModel(),
                    NewValue = result.ResultObj,
                    UpdateByUserId = Common.Extension.HttpRequestExtensions.GetUser(Request).Id,

                };

                await _lichSuCapNhatService.Create(history);

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

                //await OptionHuyen();
                //await OptionXa(request.DuLieuDuLich.QuanHuyenId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiHinhKinhDoanh();
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
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));
                var csltModel = await _duLieuDuLichService.GetById(Id);
                var tienNghi = await _tienNghiService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.NgonNguId);
                var amenities = tienNghi.Select(x => new AmenityVm()
                {
                    Id = x.Id,
                    Name = x.Ten,
                    IsSelect = csltModel.Amenities.Select(v => v.Id).Contains(x.Id) ? true : false
                }).ToList();
                if (csltModel != null)
                {
                    csltModel.DSLoaiPhong = await ListLoaiPhongHoSo(csltModel.Id, csltModel.DSLoaiPhong);
                    csltModel.DSDichVu = await ListDichVuHoSo(csltModel.Id, csltModel.DSDichVu);
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);
                    csltModel.DSNhaHang = ListNhaHangLuuTru(csltModel.Id, csltModel.DSNhaHang);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                    csltModel.Amenities = amenities;
                    //await OptionHuyen();
                    //await OptionXa(csltModel.QuanHuyenId);

                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });

                    await OptionNhaCungCap();
                    await OptionTieuChuanCoSo();

                    await this.OptionDonViTinh(0, csltModel.NgonNguId);
                    await this.OptionLoaiHinhKinhDoanh(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest(),
                    DSLoaiPhongGiuong = csltModel.DSLoaiPhongGiuong,

                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuacosoluutruEnglish", model);
                }

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

                var oldValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                request.DuLieuDuLich.ToaDoX = oldValue.ToaDoX;
                request.DuLieuDuLich.ToaDoY = oldValue.ToaDoY;

                var tienNghi = await _tienNghiService.GetAll((int)LinhVucKinhDoanh.CoSoLuuTru);

                var amenities = tienNghi.Select(x => new AmenityVm()
                {
                    Id = x.Id,
                    Name = x.Ten,
                    IsSelect = oldValue.Amenities.Select(v => v.Id).Contains(x.Id)
                }).ToList();

                if (oldValue != null)
                {
                    oldValue.DSLoaiPhong = await ListLoaiPhongHoSo(oldValue.Id, oldValue.DSLoaiPhong);
                    oldValue.DSDichVu = await ListDichVuHoSo(oldValue.Id, oldValue.DSDichVu);
                    oldValue.DSNgoaiNgu = await ListNgoaiNguHoSo(oldValue.Id, oldValue.DSNgoaiNgu, oldValue.NgonNguId);
                    oldValue.DSTrinhDo = await ListTrinhDoHoSo(oldValue.Id, oldValue.DSTrinhDo, oldValue.NgonNguId);
                    oldValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, oldValue.Id, oldValue.DSBoPhan, oldValue.NgonNguId);
                    oldValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(oldValue.Id, oldValue.DSMucDoTTNN, oldValue.NgonNguId);
                    oldValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, oldValue.Id, oldValue.DSTienNghi, oldValue.NgonNguId);
                    oldValue.DSNhaHang = ListNhaHangLuuTru(oldValue.Id, oldValue.DSNhaHang);
                    oldValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, oldValue.Id, oldValue.DSVanBan, oldValue.NgonNguId);
                    oldValue.Amenities = amenities;
                }

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
                    var newValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                    amenities = tienNghi.Select(x => new AmenityVm()
                    {
                        Id = x.Id,
                        Name = x.Ten,
                        IsSelect = newValue.Amenities.Select(v => v.Id).Contains(x.Id)
                    }).ToList();
                    if (newValue != null)
                    {
                        newValue.DSLoaiPhong = await ListLoaiPhongHoSo(newValue.Id, newValue.DSLoaiPhong);
                        newValue.DSDichVu = await ListDichVuHoSo(newValue.Id, newValue.DSDichVu);
                        newValue.DSNgoaiNgu = await ListNgoaiNguHoSo(newValue.Id, newValue.DSNgoaiNgu, newValue.NgonNguId);
                        newValue.DSTrinhDo = await ListTrinhDoHoSo(newValue.Id, newValue.DSTrinhDo, newValue.NgonNguId);
                        newValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, newValue.Id, newValue.DSBoPhan, newValue.NgonNguId);
                        newValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(newValue.Id, newValue.DSMucDoTTNN, newValue.NgonNguId);
                        newValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, newValue.Id, newValue.DSTienNghi, newValue.NgonNguId);
                        newValue.DSNhaHang = ListNhaHangLuuTru(newValue.Id, newValue.DSNhaHang);
                        newValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CoSoLuuTru, newValue.Id, newValue.DSVanBan, newValue.NgonNguId);
                        newValue.Amenities = amenities;
                    }

                    var history = new LichSuCapNhatCreateRequest
                    {
                        HoSoId = newValue.Id,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdateByUserId = Common.Extension.HttpRequestExtensions.GetUser(Request).Id
                    };

                    await _lichSuCapNhatService.Create(history);
                }

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
                        return Redirect($"/Hoso/Suacosoluutru/?Id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={oldValue.NgonNguId}");
                    }
                    else
                    {
                        return RedirectToAction("Themcosoluutru");
                    }
                }
                else
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionDonViTinh(2);
                    await this.OptionLoaiHinhKinhDoanh();
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiHinhKinhDoanh();
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
            return RedirectToAction("Cosoluutru");
        }

        private async Task OptionLoaiNhaHang(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _dichVuService.GetAll(ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenDichVu.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiNhaHang = list;
        }

        private async Task OptionGetAllNhaHang(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.NhaHang);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllNhaHang = list;
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
                int namenhahang = !String.IsNullOrEmpty(Request.Query["namenhahang"]) ? Convert.ToInt32(Request.Query["namenhahang"]) : -1;

                await OptionTieuChuanCoSo(hangsao);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await this.OptionLoaiNhaHang(loaihinh);
                await this.OptionGetAllNhaHang(namenhahang);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa,
                    namenhahang = namenhahang
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.NhaHang, pageRequest);

                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);

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

                string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                csltModel.DSThucDon = ListThucDonHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.NhaHang, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.NhaHang, 0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.NhaHang, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiNhaHang(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(id);
                        ViewData["Title"] = data.Ten;
                        return View("ThemmoinhahangEnglish", model);
                    }
                }
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
        public async Task<IActionResult> Themmoinhahang(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);

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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);

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
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.NhaHang, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);

                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });

                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiNhaHang(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                    await this.OptionDonViTinh(2);
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtinnhahangEnglish", model);
                }
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
                        return Redirect($"/Hoso/Suathongtinnhahang/?id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={csltModel.NgonNguId}");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoinhahang/");
                    }
                }
                return Redirect($"/Hoso/Suathongtinnhahang/?id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={csltModel.NgonNguId}");
            }
            catch (Exception ex)
            {
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionTieuChuanCoSo();
                await OptionCoSoLuuTru();
                await OptionNhaCungCap();

                await this.OptionLoaiNhaHang();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Nhahang");
        }

        private async Task OptionLoaiCTLuHanh(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.LuHanh, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCTLH = list;
        }

        private async Task OptionGetAllLuHanh(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllLuHanh = list;
        }

        [Authorize(Roles = "view_congtyluhanh,root")]
        public async Task<IActionResult> Congtyluhanh()
        {
            try
            {
                ViewData["Title"] = "Danh sách công ty lữ hành";
                ViewData["Title_parent"] = "Hồ sơ";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
                int nameluhanh = !String.IsNullOrEmpty(Request.Query["nameluhanh"]) ? Convert.ToInt32(Request.Query["nameluhanh"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNguonDongBo(nguon);

                await this.OptionGetAllLuHanh(nameluhanh);
                await this.OptionLoaiCTLuHanh(loaihinh);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    PhuongXa = phuongXa,
                    loaihinh = loaihinh,
                    nameluhanh = nameluhanh,
                    nguon = nguon
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.LuHanh, pageRequest);
                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);

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

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo();
                csltModel.DSTrinhDo = await ListTrinhDoHoSo();
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo();
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiCTLuHanh(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(id);
                        ViewData["Title"] = data.Ten;
                        return View("ThemmoicongtyluhanhEnglish", model);
                    }
                }
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

        public async Task<IActionResult> Themmoicongtyluhanh(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionDonViTinh(2);
                    await this.OptionLoaiCTLuHanh(request.DuLieuDuLich.LoaiHinhId, ngonNguId);

                    ModelState.AddModelError("", result.Message);
                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
                    if (!string.IsNullOrWhiteSpace(ParentId))
                    {
                        return Redirect(Request.GetBackUrl());
                    }
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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiCTLuHanh();

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
                ViewData["Title"] = "Sửa thông tin công ty lữ hành";
                ViewData["Title_parent"] = "Hồ sơ";

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                if (csltModel != null)
                {
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);

                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionDonViTinh(2);
                    await this.OptionLoaiCTLuHanh(csltModel.LoaiHinhId, csltModel.NgonNguId);
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtincongtyluhanhEnglish", model);
                }
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

                var oldValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                request.DuLieuDuLich.ToaDoX = oldValue.ToaDoX;
                request.DuLieuDuLich.ToaDoY = oldValue.ToaDoY;

                if (oldValue != null)
                {
                    oldValue.DSNgoaiNgu = await ListNgoaiNguHoSo(oldValue.Id, oldValue.DSNgoaiNgu, oldValue.NgonNguId);
                    oldValue.DSTrinhDo = await ListTrinhDoHoSo(oldValue.Id, oldValue.DSTrinhDo, oldValue.NgonNguId);
                    oldValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh, oldValue.Id, oldValue.DSBoPhan, oldValue.NgonNguId);
                    oldValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(oldValue.Id, oldValue.DSMucDoTTNN, oldValue.NgonNguId);
                    oldValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh, oldValue.Id, oldValue.DSVanBan, oldValue.NgonNguId);
                }

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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == oldValue.PhuongXaId
                    });
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiCTLuHanh(oldValue.LoaiHinhId, oldValue.NgonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }
                else
                {
                    var newValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);

                    newValue.DSNgoaiNgu = await ListNgoaiNguHoSo(newValue.Id, newValue.DSNgoaiNgu, newValue.NgonNguId);
                    newValue.DSTrinhDo = await ListTrinhDoHoSo(newValue.Id, newValue.DSTrinhDo, newValue.NgonNguId);
                    newValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.LuHanh, newValue.Id, newValue.DSBoPhan, newValue.NgonNguId);
                    newValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(newValue.Id, newValue.DSMucDoTTNN, newValue.NgonNguId);
                    newValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.LuHanh, newValue.Id, newValue.DSVanBan, newValue.NgonNguId);

                    var history = new LichSuCapNhatCreateRequest
                    {
                        HoSoId = newValue.Id,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdateByUserId = Common.Extension.HttpRequestExtensions.GetUser(Request).Id
                    };

                    await _lichSuCapNhatService.Create(history);
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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiCTLuHanh();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Congtyluhanh");
        }

        private async Task OptionLoaiCoSoMuaSam(int seletedId = 0, string NgonNgu = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _loaiDichVuService.GetAll(NgonNgu);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDichVu = list;
        }

        private async Task OptionGetAllCSMS(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.MuaSam);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllCSMS = list;
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
                int namecsms = !String.IsNullOrEmpty(Request.Query["namecsms"]) ? Convert.ToInt32(Request.Query["namecsms"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                await OptionTieuChuanCoSo(hangsao);
                await OptionNguonDongBo(nguon);

                await this.OptionGetAllCSMS(namecsms);
                await this.OptionLoaiCoSoMuaSam(loaihinh);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa,
                    namecsms = namecsms,
                    nguon = nguon,
                };

                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.MuaSam, pageRequest);

                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);

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

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.MuaSam, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.MuaSam, 0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.MuaSam, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiCoSoMuaSam(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(id);
                        ViewData["Title"] = data.Ten;
                        return View("ThemmoicosomuasamEnglish", model);
                    }
                }
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
        public async Task<IActionResult> Themmoicosomuasam(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);

                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiCoSoMuaSam(request.DuLieuDuLich.LoaiHinhId, ngonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);

                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiCoSoMuaSam(0, NgonNgu);
                await this.OptionDonViTinh(2);

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

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                var csltModel = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

                if (csltModel != null)
                {
                    csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                    csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                    csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                    csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                    csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                    csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSTienNghi, ngonNguId);
                    csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.MuaSam, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);

                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });

                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiCoSoMuaSam(csltModel.LoaiHinhId, csltModel.NgonNguId);
                    await this.OptionDonViTinh(2);
                }

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtincosomuasamEnglish", model);
                }
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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });
                    await OptionTieuChuanCoSo();
                    await OptionNhaCungCap();

                    await this.OptionLoaiCoSoMuaSam();
                    await this.OptionDonViTinh(2);

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
                        return Redirect($"/Hoso/Suathongtincosomuasam/?id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={csltModel.NgonNguId}");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoicosomuasam/");
                    }
                }
                else
                {
                    return Redirect($"/Hoso/Suathongtincosomuasam/?id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={csltModel.NgonNguId}");
                }
            }
            catch (Exception ex)
            {
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionTieuChuanCoSo();
                await OptionNhaCungCap();

                await this.OptionLoaiCoSoMuaSam();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Cosomuasam");
        }

        private async Task OptionLoaiDiemDuLich(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.DiemDuLich, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDDL = list;
        }

        private async Task OptionGetAllDDL(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.DiemDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listGetAllDDL = list;
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
                int nameddl = !String.IsNullOrEmpty(Request.Query["nameddl"]) ? Convert.ToInt32(Request.Query["nameddl"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                //await OptionHuyen(1, phuongXa);
                await OptionNguonDongBo(nguon);

                await this.OptionGetAllDDL(nameddl);
                await this.OptionLoaiDiemDuLich(loaihinh);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa,
                    nameddl = nameddl,
                    nguon = nguon
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.DiemDuLich, pageRequest);

                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);

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

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich, 0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNhaCungCap();

                await this.OptionLoaiDiemDuLich(0, ngonNguId);
                await this.OptionDonViTinh(2, ngonNguId);

                var model = new DuLieuDuLichCreateExtRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadExtRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(id);
                        ViewData["Title"] = data.Ten;
                        return View("ThemmoidiemdulichEnglish", model);
                    }
                }

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
        public async Task<IActionResult> Themmoidiemdulich(DuLieuDuLichCreateExtRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionNhaCungCap();

                    await this.OptionLoaiDiemDuLich(request.DuLieuDuLich.LoaiHinhId, ngonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);
                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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
                //await OptionHuyen();
                //await OptionXa();

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiDiemDuLich();
                await this.OptionDonViTinh(2);

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
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiDiemDuLich(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichUpdateExtRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadExtRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtindiemdulichEnglish", model);
                }

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

                var oldValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                request.DuLieuDuLich.ToaDoX = oldValue.ToaDoX;
                request.DuLieuDuLich.ToaDoY = oldValue.ToaDoY;

                var ds = await _duLieuDuLichService.GetListVanBanByHoSo(request.DuLieuDuLich.Id);

                oldValue.DSVeDichVu = ListVeDichVuHoSo(oldValue.Id, oldValue.DSVeDichVu);
                oldValue.DSThucDon = ListThucDonHoSo(oldValue.Id, oldValue.DSThucDon);
                oldValue.DSNgoaiNgu = await ListNgoaiNguHoSo(oldValue.Id, oldValue.DSNgoaiNgu, oldValue.NgonNguId);
                oldValue.DSTrinhDo = await ListTrinhDoHoSo(oldValue.Id, oldValue.DSTrinhDo, oldValue.NgonNguId);
                oldValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich, oldValue.Id, oldValue.DSBoPhan, oldValue.NgonNguId);
                oldValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(oldValue.Id, oldValue.DSMucDoTTNN, oldValue.NgonNguId);
                oldValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich, oldValue.Id, oldValue.DSVanBan, oldValue.NgonNguId);
                oldValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich, oldValue.Id, oldValue.DSTienNghi, oldValue.NgonNguId);

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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });
                    await OptionNhaCungCap();

                    await this.OptionLoaiDiemDuLich(oldValue.LoaiHinh.Id, oldValue.NgonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }
                else
                {
                    var newValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                    newValue.DSVeDichVu = ListVeDichVuHoSo(newValue.Id, newValue.DSVeDichVu);
                    newValue.DSThucDon = ListThucDonHoSo(newValue.Id, newValue.DSThucDon);
                    newValue.DSNgoaiNgu = await ListNgoaiNguHoSo(newValue.Id, newValue.DSNgoaiNgu, newValue.NgonNguId);
                    newValue.DSTrinhDo = await ListTrinhDoHoSo(newValue.Id, newValue.DSTrinhDo, newValue.NgonNguId);
                    newValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.DiemDuLich, newValue.Id, newValue.DSBoPhan, newValue.NgonNguId);
                    newValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(newValue.Id, newValue.DSMucDoTTNN, newValue.NgonNguId);
                    newValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.DiemDuLich, newValue.Id, newValue.DSVanBan, newValue.NgonNguId);
                    newValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.DiemDuLich, newValue.Id, newValue.DSTienNghi, newValue.NgonNguId);

                    var history = new LichSuCapNhatCreateRequest
                    {
                        HoSoId = newValue.Id,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdateByUserId = Common.Extension.HttpRequestExtensions.GetUser(Request).Id
                    };

                    await _lichSuCapNhatService.Create(history);
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
                        return Redirect($"/Hoso/Suathongtindiemdulich/?Id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={oldValue.NgonNguId}");
                    }
                    else
                    {
                        return Redirect("/Hoso/Themmoidiemdulich/");
                    }
                }
                else
                {
                    return Redirect($"/Hoso/Suathongtindiemdulich/?Id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={oldValue.NgonNguId}");
                }
            }
            catch (Exception ex)
            {
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiDiemDuLich();

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
            return RedirectToAction("Diemdulich");
        }

        [Authorize(Roles = "view_huongdanvien,root")]
        public async Task<IActionResult> Huongdanvien()
        {
            try
            {
                ViewData["Title"] = "Hướng dẫn viên du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

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

        private async Task OptionNgoaiNgu(int seletedId = 0)
        {
            var luhanh = await _ngoaiNguService.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenNgoaiNgu.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listNgoaiNgu = list;
        }

        private async Task OptionCongTyLuHanh(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listCongTyLuHanh = list;
        }

        [Authorize(Roles = "create_huongdanvien,root")]
        public async Task<IActionResult> Themhuongdanvien()
        {
            ViewData["Title"] = "Thêm hướng dẫn viên du lịch";
            ViewData["Title_parent"] = "Hồ sơ";

            var hdvModel = new HuongDanVienModel();
            hdvModel.DSQuaTrinhHD = ListQuaTrinhHoatDong();
            hdvModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.HDV);

            await OptionLoaiTheHDV();

            await this.OptionNgoaiNgu();
            await this.OptionLoaiDiemDuLich();
            await this.OptionCongTyLuHanh();


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

            await OptionLoaiTheHDV();

            await this.OptionNgoaiNgu();
            await this.OptionLoaiDiemDuLich();
            await this.OptionCongTyLuHanh();

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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;

                await OptionLoaiKhuDuLich(loaihinh);
                //await OptionHuyen(1, huyen);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.KhuDuLich, pageRequest);
                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        private async Task OptionLoaiKhuDuLich(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.KhuDuLich, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuDL = list;
        }

        [Authorize(Roles = "create_khudulich,root")]
        public async Task<IActionResult> Themmoikhudulich()
        {
            try
            {
                ViewData["Title"] = "Thêm mới khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                string ngonNguId = !string.IsNullOrEmpty(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich, 0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiKhuDuLich(0, ngonNguId);
                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["Id"]))
                    {
                        int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                        var data = await _hoSoService.GetHoSo(id);
                        ViewData["Title"] = data.Ten;
                    }

                    return View("ThemmoikhudulichEnglish", model);
                }
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
        public async Task<IActionResult> Themmoikhudulich(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });
                    await OptionNhaCungCap();

                    await this.OptionLoaiKhuDuLich(request.DuLieuDuLich.LoaiHinhId, ngonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);

                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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
                //await OptionHuyen();
                //await OptionXa();

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNhaCungCap();

                await this.OptionLoaiKhuDuLich();
                await this.OptionDonViTinh(2);

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_khudulich,root")]
        public async Task<IActionResult> Suathongtinkhudulich(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var csltModel = await _duLieuDuLichService.GetById(Id);

                ViewData["Title"] = "Sửa thông tin khu du lịch";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiKhuDuLich(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                await this.OptionDonViTinh(2);
                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtinkhudulichEnglish", model);
                }

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

                var oldValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                oldValue.DSVeDichVu = ListVeDichVuHoSo(oldValue.Id, oldValue.DSVeDichVu);
                oldValue.DSThucDon = ListThucDonHoSo(oldValue.Id, oldValue.DSThucDon);
                oldValue.DSNgoaiNgu = await ListNgoaiNguHoSo(oldValue.Id, oldValue.DSNgoaiNgu, oldValue.NgonNguId);
                oldValue.DSTrinhDo = await ListTrinhDoHoSo(oldValue.Id, oldValue.DSTrinhDo, oldValue.NgonNguId);
                oldValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich, oldValue.Id, oldValue.DSBoPhan, oldValue.NgonNguId);
                oldValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(oldValue.Id, oldValue.DSMucDoTTNN, oldValue.NgonNguId);
                oldValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich, oldValue.Id, oldValue.DSVanBan, oldValue.NgonNguId);
                oldValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich, oldValue.Id, oldValue.DSTienNghi, oldValue.NgonNguId);

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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = oldValue.PhuongXaId == x.Id
                    });
                    await OptionNhaCungCap();

                    await this.OptionLoaiKhuDuLich();
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);
                    return View(request);
                }
                else
                {
                    var newValue = await _duLieuDuLichService.GetById(request.DuLieuDuLich.Id);
                    newValue.DSVeDichVu = ListVeDichVuHoSo(newValue.Id, newValue.DSVeDichVu);
                    newValue.DSThucDon = ListThucDonHoSo(newValue.Id, newValue.DSThucDon);
                    newValue.DSNgoaiNgu = await ListNgoaiNguHoSo(newValue.Id, newValue.DSNgoaiNgu, newValue.NgonNguId);
                    newValue.DSTrinhDo = await ListTrinhDoHoSo(newValue.Id, newValue.DSTrinhDo, newValue.NgonNguId);
                    newValue.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuDuLich, newValue.Id, newValue.DSBoPhan, newValue.NgonNguId);
                    newValue.DSMucDoTTNN = await ListMucDoThongThaoHoSo(newValue.Id, newValue.DSMucDoTTNN, newValue.NgonNguId);
                    newValue.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuDuLich, newValue.Id, newValue.DSVanBan, newValue.NgonNguId);
                    newValue.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuDuLich, newValue.Id, newValue.DSTienNghi, newValue.NgonNguId);

                    var history = new LichSuCapNhatCreateRequest
                    {
                        HoSoId = newValue.Id,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdateByUserId = Common.Extension.HttpRequestExtensions.GetUser(Request).Id
                    };

                    await _lichSuCapNhatService.Create(history);
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
                    return Redirect($"/Hoso/Suathongtinkhudulich/?Id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={oldValue.NgonNguId}");
                }
            }
            catch (Exception ex)
            {
                //await OptionHuyen();
                //await OptionXa(request.DuLieuDuLich.QuanHuyenId);
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiKhuDuLich();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Khudulich");
        }

        private async Task OptionLoaiKhuVuiChoi(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.KhuVuiChoi, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuVuiChoi = list;
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;
                int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });
                await OptionNguonDongBo(nguon);

                await this.OptionLoaiKhuVuiChoi(loaihinh);

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa,
                    nguon = nguon
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.KhuVuiChoi, pageRequest);
                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);
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

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;
                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;
                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, 0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiKhuVuiChoi(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                    var data = await _hoSoService.GetHoSo(id);
                    ViewData["Title"] = data.Ten;
                    return View("ThemmoikhuvcgtEnglish", model);
                }
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
        public async Task<IActionResult> Themmoikhuvcgt(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });
                    await OptionNhaCungCap();

                    await this.OptionDonViTinh(2);
                    await this.OptionLoaiKhuVuiChoi(request.DuLieuDuLich.LoaiHinhId, ngonNguId);

                    ModelState.AddModelError("", result.Message);

                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiKhuVuiChoi();
                await this.OptionDonViTinh(2);

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvuvuichoi,root")]
        public async Task<IActionResult> Suathongtinkhuvcgt(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var csltModel = await _duLieuDuLichService.GetById(Id);

                ViewData["Title"] = "Sửa thông tin khu vui chơi, giải trí";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);

                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.KhuVuiChoi, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });

                await OptionNhaCungCap();
                await this.OptionLoaiKhuVuiChoi(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtinkhuvcgtEnglish", model);
                }
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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });
                    await OptionNhaCungCap();

                    await this.OptionLoaiKhuVuiChoi(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                    await this.OptionDonViTinh(2);

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
                    return Redirect($"/Hoso/Suathongtinkhuvcgt/?id={HashUtil.EncodeID(request.DuLieuDuLich.Id.ToString())}&NgonNgu={csltModel.NgonNguId}");
                }
            }
            catch (Exception ex)
            {
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiKhuVuiChoi();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Vcgt");
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;

                await OptionLoaiHinhCSSK(loaihinh);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.CSSK, pageRequest);
                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        private async Task OptionLoaiHinhCSSK(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.CSSK, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCSSK = list;
        }

        [Authorize(Roles = "create_dichvucssk,root")]
        public async Task<IActionResult> Themmoicssk()
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);

                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CSSK, 0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CSSK, 0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CSSK, 0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNhaCungCap();

                await this.OptionLoaiHinhCSSK(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                    var data = await _hoSoService.GetHoSo(id);
                    ViewData["Title"] = data.Ten;
                    return View("ThemmoicsskEnglish", model);
                }
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
        public async Task<IActionResult> Themmoicssk(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionNhaCungCap();

                    await this.OptionLoaiHinhCSSK(request.DuLieuDuLich.LoaiHinhId, ngonNguId);
                    await this.OptionDonViTinh(2);
                    ViewBag.NgonNgu = ngonNguId;
                    ModelState.AddModelError("", result.Message);
                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiHinhCSSK(request.DuLieuDuLich.LoaiHinhId, request.DuLieuDuLich.NgonNguId);
                await this.OptionDonViTinh(2);

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvucssk,root")]
        public async Task<IActionResult> Suathongtincssk(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var csltModel = await _duLieuDuLichService.GetById(Id);

                ViewData["Title"] = "Sửa thông tin cơ sở chăm sóc sức khỏe";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.CSSK, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiHinhCSSK(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtincsskEnglish", model);
                }
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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });

                    await OptionNhaCungCap();

                    await this.OptionLoaiHinhCSSK(csltModel.LoaiHinh.Id, csltModel.NgonNguId);
                    await this.OptionDonViTinh(2);

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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiHinhCSSK();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Cssk");
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
                int phuongXa = !String.IsNullOrEmpty(Request.Query["PhuongXa"]) ? Convert.ToInt32(Request.Query["PhuongXa"]) : -1;

                await OptionLoaiHinhTheThao(loaihinh);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                var pageRequest = new HoSoFromRequets()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    hangsao = hangsao,
                    loaihinh = loaihinh,
                    PhuongXa = phuongXa
                };
                var data = await _duLieuDuLichService.GetPaging(Request.GetLanguageId(), (int)LinhVucKinhDoanh.TheThao, pageRequest);
                ViewBag.ListDuLieuDuLichEnglish = await _duLieuDuLichService.DuLieuDuLichEnglish(data.Items);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi");
                return View(pageError404);
            }
        }

        private async Task OptionLoaiHinhTheThao(int seletedId = 0, string ngonNguId = SystemConstants.DefaultLanguage)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.TheThao, ngonNguId);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiTheThao = list;
        }

        [Authorize(Roles = "create_dichvuthethao,root")]
        public async Task<IActionResult> Themmoicstt()
        {
            try
            {
                ViewData["Title"] = "Thêm mới cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

                string ngonNguId = !string.IsNullOrWhiteSpace(Request.Query["NgonNgu"]) ? Request.Query["NgonNgu"] : SystemConstants.DefaultLanguage;

                if (!ngonNguId.Equals("vi", StringComparison.OrdinalIgnoreCase)
                 && !ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase)) ngonNguId = SystemConstants.DefaultLanguage;

                ViewBag.NgonNgu = ngonNguId;

                if (!string.IsNullOrWhiteSpace(Request.Query["Id"])) ViewBag.ParentId = Request.Query["Id"];

                var csltModel = new DuLieuDuLichModel();

                csltModel.DSVeDichVu = ListVeDichVuHoSo();
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(0, null, ngonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(0, null, ngonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.TheThao, 0, null, ngonNguId);
                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.TheThao, 0, null, ngonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.TheThao, 0, null, ngonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(0, null, ngonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString()
                });

                await OptionNhaCungCap();

                await this.OptionLoaiHinhTheThao(0, ngonNguId);
                await this.OptionDonViTinh(2);

                var model = new DuLieuDuLichCreateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (ngonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    int id = Convert.ToInt32(HashUtil.DecodeID(Request.Query["Id"]));
                    var data = await _hoSoService.GetHoSo(id);
                    ViewData["Title"] = data.Ten;
                    return View("ThemmoicsttEnglish", model);
                }
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
        public async Task<IActionResult> Themmoicstt(DuLieuDuLichCreateRequest request, string type_sumit, string NgonNgu, string ParentId)
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

                string ngonNguId = !string.IsNullOrWhiteSpace(NgonNgu) ? NgonNgu : SystemConstants.DefaultLanguage;

                if (!string.IsNullOrWhiteSpace(ParentId)) request.DuLieuDuLich.ParentId = Convert.ToInt32(HashUtil.DecodeID(ParentId));

                var result = await _duLieuDuLichService.Create(ngonNguId, request.DuLieuDuLich);
                if (!result.IsSuccessed)
                {
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                    });

                    await OptionNhaCungCap();

                    await this.OptionLoaiHinhTheThao(request.DuLieuDuLich.LoaiHinhId, ngonNguId);
                    await this.OptionDonViTinh(2);

                    ModelState.AddModelError("", result.Message);
                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = result.Message });
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

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionLoaiHinhTheThao(0, NgonNgu);
                await this.OptionDonViTinh(2);

                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize(Roles = "edit_dichvuthethao,root")]
        public async Task<IActionResult> Suathongtincstt(string id)
        {
            try
            {
                int Id = Convert.ToInt32(HashUtil.DecodeID(id));

                var csltModel = await _duLieuDuLichService.GetById(Id);

                ViewData["Title"] = "Sửa thông tin cơ sở thể thao";
                ViewData["Title_parent"] = "Hồ sơ";

                csltModel.DSVeDichVu = ListVeDichVuHoSo(csltModel.Id, csltModel.DSVeDichVu);
                csltModel.DSThucDon = ListThucDonHoSo(csltModel.Id, csltModel.DSThucDon);
                csltModel.DSNgoaiNgu = await ListNgoaiNguHoSo(csltModel.Id, csltModel.DSNgoaiNgu, csltModel.NgonNguId);
                csltModel.DSTrinhDo = await ListTrinhDoHoSo(csltModel.Id, csltModel.DSTrinhDo, csltModel.NgonNguId);
                csltModel.DSBoPhan = await ListBoPhanHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSBoPhan, csltModel.NgonNguId);
                csltModel.DSMucDoTTNN = await ListMucDoThongThaoHoSo(csltModel.Id, csltModel.DSMucDoTTNN, csltModel.NgonNguId);

                csltModel.DSVanBan = await ListVanBanHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSVanBan, csltModel.NgonNguId);
                csltModel.DSTienNghi = await ListMucTienNghiHoSo((int)LinhVucKinhDoanh.TheThao, csltModel.Id, csltModel.DSTienNghi, csltModel.NgonNguId);

                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == csltModel.PhuongXaId
                });

                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiHinhTheThao(csltModel.LoaiHinh.Id, csltModel.NgonNguId);

                var model = new DuLieuDuLichUpdateRequest()
                {
                    DuLieuDuLich = csltModel,
                    Images = new ImageUploadRequest()
                };

                if (csltModel.NgonNguId.Equals("en", StringComparison.OrdinalIgnoreCase))
                {
                    ViewData["Title"] = csltModel.Ten;
                    return View("SuathongtincsttEnglish", model);
                }
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
                    ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                    {
                        Text = x.TenDiaPhuong,
                        Value = x.Id.ToString(),
                        Selected = x.Id == csltModel.PhuongXaId
                    });
                    await OptionNhaCungCap();

                    await this.OptionDonViTinh(2);
                    await this.OptionLoaiHinhTheThao(csltModel.LoaiHinh.Id, csltModel.NgonNguId);

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
                ViewBag.DiaPhuongOption = (await _diaPhuongService.GetHierarchy()).Where(x => x.ParentId != 0).Select(x => new SelectListItem
                {
                    Text = x.TenDiaPhuong,
                    Value = x.Id.ToString(),
                    Selected = x.Id == request.DuLieuDuLich.PhuongXaId
                });
                await OptionNhaCungCap();

                await this.OptionLoaiHinhTheThao();
                await this.OptionDonViTinh(2);

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
            return RedirectToAction("Thethao");
        }

        [Authorize(Roles = "view_luutru,view_nhahang" +
            ",view_diemdulich,view_khudulich" +
            ",view_dichvuvuichoi,view_vesinhcongcong" +
            ",view_dichvuthethao,view_dichvucssk" +
            ",root")]

        public async Task<IActionResult> Chitiet(string id)
        {

            var model = await _duLieuDuLichService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));
            ViewBag.LichSuCapNhat = (await _lichSuCapNhatService.GetByHoSoId(Convert.ToInt32(HashUtil.DecodeID(id)))).ResultObj;
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
                    PhuongXa = huyen,
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

        private async Task OptionLoaiCTVanChuyen(int seletedId = 0)
        {
            var luhanh = await _danhMucService.GetAll((int)LinhVucKinhDoanh.VanChuyen);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCTVC = list;
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
                await OptionNhaCungCap();

                await this.OptionLoaiCTVanChuyen();
                await this.OptionDonViTinh(2);
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
                    await OptionNhaCungCap();

                    await this.OptionLoaiCTVanChuyen();
                    await this.OptionDonViTinh(2);

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
                await OptionNhaCungCap();

                await this.OptionLoaiCTVanChuyen();
                await this.OptionDonViTinh(2);

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
                    await OptionNhaCungCap();

                    await this.OptionLoaiCTVanChuyen();
                    await this.OptionDonViTinh(2);
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
                    await OptionNhaCungCap();

                    await this.OptionLoaiCTVanChuyen();
                    await this.OptionDonViTinh(2);

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
                await OptionNhaCungCap();

                await this.OptionDonViTinh(2);
                await this.OptionLoaiCTVanChuyen();

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