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
    public class BanDoController : BaseController
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
        private readonly IDiemVeSinhDongBoService _diemVeSinhDongBoService;

        private readonly IConfiguration _config;

        public BanDoController(IUserService userService
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
            , IDiemVeSinhDongBoService diemVeSinhDongBoService)
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
            _config = configuration;
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
            _diemVeSinhDongBoService = diemVeSinhDongBoService;
        }

        public async Task<IActionResult> Edit(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var model = await _hoSoService.GetHoSo(Id);

            string link = HttpContext.Request.GetBackUrl();

            HoSoBanDo md = new HoSoBanDo
            { 
                ID = model.Id,
                HSID = model.Id,
                Ten = model.Ten,
                X = model.ToaDoX,
                Y = model.ToaDoY,
                Origin = link
            };

            if (model != null)
            {
                ViewData["Title"] = "Thông tin hồ sơ " + md.Ten;
            }

            return View(md);
        }

        public async Task<IActionResult> EditDiemVeSinh(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var model = await _diemVeSinhDongBoService.GetById(Id);

            string link = HttpContext.Request.GetBackUrl();

            HoSoBanDo md = new HoSoBanDo
            {
                ID = model.Id,
                HSID = model.Id,
                Ten = model.Ten,
                X = model.X,
                Y = model.Y,
                Origin = link
            };

            if (model != null)
            {
                ViewData["Title"] = "Thông tin hồ sơ " + md.Ten;
            }

            return View(md);
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HoSoBanDo request)
        {
            try
            {
                ModelState.Remove("ID");

                var result = await _hoSoService.EditBanDo(request.HSID, request);
                if (result == null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Đã xảy ra lỗi!" });
                    return Redirect("/HueCIT/BanDo/Edit?id=" + HashUtil.EncodeID(request.HSID.ToString()));
                }

                if (result.ToaDoX != null && result.ToaDoX != 0 && result.ToaDoY != null && result.ToaDoY != 0)
                {
                    string ctk = _config.GetValue<string>("ArcGisToken");
                    int layer = ReturnLayer(result.LinhVucKinhDoanhId);
                    List<TechLife.Model.HueCIT.DongBoDieuHanhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhAdd>();
                    List<TechLife.Model.HueCIT.DongBoDieuHanhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhEdit>();

                    var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + result.Id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
                    check.Timeout = -1;
                    var chk = new RestRequest(Method.GET);
                    //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                    chk.AddHeader("Cookie", ctk);
                    IRestResponse res = check.Execute(chk);
                    var chkinfo = JsonConvert.DeserializeObject<CheckResponse>(res.Content);

                    if (chkinfo.objectIds == null)
                    {
                        TechLife.Model.HueCIT.DongBoDieuHanhAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhAdd
                        {
                            geometry = new TechLife.Model.HueCIT.ToaDo
                            {
                                x = result.ToaDoY,
                                y = result.ToaDoX,
                                spatialReference = new TechLife.Model.HueCIT.SpatialReference
                                {
                                    wkid = 4326
                                }
                            },
                            attributes = new TechLife.Model.HueCIT.HoSoDongBoAdd
                            {
                                id = result.Id,
                                ten = result.Ten,
                                linhvuckin = result.LinhVucKinhDoanhId,
                                loaihinhid = layer == 4 ? ReturnLoaiHinhDiemDuLich(result.LoaiHinhId) : result.LoaiHinhId,
                                sonha = result.SoNha,
                                duongpho = result.DuongPho,
                                sodienthoa = result.SoDienThoai
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
                                x = result.ToaDoY,
                                y = result.ToaDoX,
                                spatialReference = new TechLife.Model.HueCIT.SpatialReference
                                {
                                    wkid = 4326
                                }
                            },
                            attributes = new TechLife.Model.HueCIT.HoSoDongBoEdit
                            {
                                objectid = chkinfo.objectIds.First(),
                                id = result.Id,
                                ten = result.Ten,
                                linhvuckin = result.LinhVucKinhDoanhId,
                                loaihinhid = layer == 4 ? ReturnLoaiHinhDiemDuLich(result.LoaiHinhId) : result.LoaiHinhId,
                                sonha = result.SoNha,
                                duongpho = result.DuongPho,
                                sodienthoa = result.SoDienThoai
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

                await Tracking("Cập nhật bản đồ " + request.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật thành công" });
                //return Redirect("/HueCIT/BanDo/Edit?id=" + HashUtil.EncodeID(request.HSID.ToString()));
                return Redirect(request.Origin);
            }
            catch (Exception ex)
            {

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/BanDo/Edit?id=" + HashUtil.EncodeID(request.HSID.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiemVeSinh(HoSoBanDo request)
        {
            try
            {
                ModelState.Remove("ID");

                var result = await _diemVeSinhDongBoService.EditBanDo(request.HSID, request);
                if (result == null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Đã xảy ra lỗi!" });
                    return Redirect("/HueCIT/BanDo/EditDiemVeSinh?id=" + HashUtil.EncodeID(request.HSID.ToString()));
                }

                if (result.X != null && result.X != 0 && result.Y != null && result.Y != 0)
                {
                    string ctk = _config.GetValue<string>("ArcGisToken");
                    int layer = 11;
                    List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd>();
                    List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhEdit>();

                    var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + result.Id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
                    check.Timeout = -1;
                    var chk = new RestRequest(Method.GET);
                    //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
                    chk.AddHeader("Cookie", ctk);
                    IRestResponse res = check.Execute(chk);
                    var chkinfo = JsonConvert.DeserializeObject<CheckResponse>(res.Content);

                    if (chkinfo.objectIds == null)
                    {
                        TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhDiemVeSinhAdd
                        {
                            geometry = new TechLife.Model.HueCIT.ToaDo
                            {
                                x = result.Y,
                                y = result.X,
                                spatialReference = new TechLife.Model.HueCIT.SpatialReference
                                {
                                    wkid = 4326
                                }
                            },
                            attributes = new TechLife.Model.HueCIT.DiemVeSinhDongBoAdd
                            {
                                id = result.Id,
                                ten = result.Ten,
                                vitri = result.ViTri,
                                hientrang = result.HienTrang,
                                mota = result.MoTa,
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
                                x = result.Y,
                                y = result.X,
                                spatialReference = new TechLife.Model.HueCIT.SpatialReference
                                {
                                    wkid = 4326
                                }
                            },
                            attributes = new TechLife.Model.HueCIT.DiemVeSinhDongBoEdit
                            {
                                objectid = chkinfo.objectIds.First(),
                                id = result.Id,
                                ten = result.Ten,
                                vitri = result.ViTri,
                                hientrang = result.HienTrang,
                                mota = result.MoTa,
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

                await Tracking("Cập nhật bản đồ " + request.Ten);

                TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật thành công" });
                //return Redirect("/HueCIT/BanDo/EditDiemVeSinh?id=" + HashUtil.EncodeID(request.HSID.ToString()));
                return Redirect(request.Origin);
            }
            catch (Exception ex)
            {

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/BanDo/EditDiemVeSinh?id=" + HashUtil.EncodeID(request.HSID.ToString()));
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
    }
}