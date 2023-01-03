using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients.HueCIT;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class LeHoiController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly ILeHoiRepository respository;
        private readonly IFileRepository respositoryFile;
        private readonly IFileUploaderApiClient fileApiClient;
        private readonly ILeHoiScheduleRepositoty leHoiScheduleRepositoty;
        private readonly IDanhMucRepository danhMucRepositoty;
        private readonly IConfiguration _config;

        public LeHoiController(IConfiguration config,
                               ILeHoiRepository _respository,
                               IFileRepository _respositoryFile,
                               IFileUploaderApiClient _fileApiClient,
                               IUserService userService,
                               IConfiguration configuration,
                               IDiaPhuongService diaPhuongService,
                               ITrackingService trackingService,
                               IDanhMucRepository _danhMucRepository,
                               ILeHoiScheduleRepositoty _leHoiScheduleRepositoty)
            : base(userService, diaPhuongService, configuration, trackingService)
        {
            _config = config;
            respository = _respository;
            respositoryFile = _respositoryFile;
            fileApiClient = _fileApiClient;
            leHoiScheduleRepositoty = _leHoiScheduleRepositoty;
            danhMucRepositoty = _danhMucRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách lễ hội";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loaihinh = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1;
            int cap = !String.IsNullOrEmpty(Request.Query["cap"]) ? Convert.ToInt32(Request.Query["cap"]) : -1;
            string diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Request.Query["diadiem"].ToString() : "";
            int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;
            int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

            ViewBag.LoaiHinh = loaihinh;
            ViewBag.Cap = cap;
            ViewBag.DiaDiem = diadiem;
            ViewBag.Huyen = huyen;
            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;
            ViewBag.Nguon = nguon;

            await OptionLoai(loaihinh);
            await OptionCapQuanLy(cap);
            await OptionHuyen(1, huyen);
            await OptionNguonDongBo(nguon);

            LeHoiRequest request = new LeHoiRequest
            {
                Loai = loaihinh,
                Cap = cap,
                DiaDiem = diadiem,
                BatDau = tungay,
                KetThuc = denngay,
                QuanHuyenId = huyen,
                NguonDongBo = nguon
            };

            List<LeHoiTrinhDien> obj = new List<LeHoiTrinhDien>();
            obj = (await respository.Gets(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> Detail(string id)
        {
            string Id = HashUtil.DecodeID(id);
            var model = await respository.Get(Id);

            if (model != null)
            {
                ViewData["Title"] = "Thông tin lễ hội " + model.TenLeHoi;
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Thêm mới lễ hội";

            var lhModel = new LeHoi();
            lhModel.BatDau = DateTime.Now;
            lhModel.KetThuc = DateTime.Now;

            await OptionLoai();
            await OptionCapQuanLy();
            await OptionHuyen();
            await OptionXa(-1);

            var model = new LeHoiRequestMod()
            {
                DuLieuLeHoi = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Cập nhật lễ hội";

            string Id = HashUtil.DecodeID(id);

            var model = await respository.Get(Id);

            var lhModel = new LeHoi
            {
                ID = model.ID,
                TenLeHoi = model.TenLeHoi,
                Loai = (int)model.Loai,
                Cap = (int)model.Cap,
                NoiDung = model.NoiDung,
                BatDau = model.BatDau,
                KetThuc = model.KetThuc,
                DiaDiem = model.DiaDiem,
                X = model.X,
                Y = model.Y,
                PhuongXaId = model.PhuongXaId,
                QuanHuyenId = model.QuanHuyenId,
                Files = model.Files
            };

            if (lhModel != null)
            {
                await OptionLoai((int)lhModel.Loai);
                await OptionCapQuanLy((int)lhModel.Cap);
                await OptionHuyen();
                await OptionXa(lhModel.QuanHuyenId);
            }

            var modelEdit = new LeHoiRequestMod()
            {
                DuLieuLeHoi = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(modelEdit);
        }

        [HttpPost]
        [Authorize(Roles = "create_lehoi,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(LeHoiRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuLeHoi.Id");

                request.DuLieuLeHoi.NguonDongBo = 0;

                var result = await respository.Add(request.DuLieuLeHoi);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    {
                        LoaiDoiTuong = "CN_LeHoi",
                        ID = result.ID.ToString()
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }
                await Tracking("Thêm lễ hội " + request.DuLieuLeHoi.TenLeHoi);

                if (result != null && request.DuLieuLeHoi.X != null && request.DuLieuLeHoi.X != 0 && request.DuLieuLeHoi.Y != null && request.DuLieuLeHoi.Y != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.DuLieuLeHoi.Y,
                        y = request.DuLieuLeHoi.X
                    };

                    TechLife.Model.HueCIT.LeHoiDongBoAdd data = new Model.HueCIT.LeHoiDongBoAdd
                    {
                        id = result.ID.ToString(),
                        tenlehoi = request.DuLieuLeHoi.TenLeHoi,
                        diadiem = request.DuLieuLeHoi.DiaDiem,
                        noidung = request.DuLieuLeHoi.NoiDung
                    };

                    AddEditGIS(data, geo);
                }

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm lễ hội thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm lễ hội thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/LeHoi/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/LeHoi/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/LeHoi/Add");
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.DuLieuLeHoi.Loai);
                await OptionCapQuanLy(request.DuLieuLeHoi.Cap);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/HueCIT/LeHoi/Add");
        }

        [HttpPost]
        [Authorize(Roles = "edit_lehoi,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeHoiRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuLeHoi.Id");

                var result = await respository.Edit(request.DuLieuLeHoi);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    {
                        LoaiDoiTuong = "CN_LeHoi",
                        ID = request.DuLieuLeHoi.ID.ToString(),
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }

                if (result != null && request.DuLieuLeHoi.X != null && request.DuLieuLeHoi.X != 0 && request.DuLieuLeHoi.Y != null && request.DuLieuLeHoi.Y != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.DuLieuLeHoi.Y,
                        y = request.DuLieuLeHoi.X
                    };

                    TechLife.Model.HueCIT.LeHoiDongBoAdd data = new Model.HueCIT.LeHoiDongBoAdd
                    {
                        id = request.DuLieuLeHoi.ID.ToString(),
                        tenlehoi = request.DuLieuLeHoi.TenLeHoi,
                        diadiem = request.DuLieuLeHoi.DiaDiem,
                        noidung = request.DuLieuLeHoi.NoiDung
                    };

                    AddEditGIS(data, geo);
                }

                await Tracking("Cập nhật lễ hội " + request.DuLieuLeHoi.TenLeHoi);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật lễ hội thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật lễ hội thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/LeHoi/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/LeHoi/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/LeHoi/Edit?id=" + HashUtil.EncodeID(request.DuLieuLeHoi.ID.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.DuLieuLeHoi.Loai);
                await OptionCapQuanLy(request.DuLieuLeHoi.Cap);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/LeHoi/Edit?id=" + HashUtil.EncodeID(request.DuLieuLeHoi.ID.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_lehoi,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            string Id = HashUtil.DecodeID(id);

            var result = await respository.Delete(Id);

            RemoveGIS(Id, (int)LinhVucKinhDoanh.NhaHang);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa lễ hội thành công",
            });
            await Tracking("Xóa lễ hội thành công");
            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Authorize(Roles = "edit_lehoi,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFiles(int id)
        {
            var result = await respositoryFile.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa tệp thành công",
            });

            return Redirect(Request.GetBackUrl());
        }

        public async Task OptionLoai(int seletedId = 0)
        {
            var list = (await danhMucRepositoty.GetsLoaiLeHoi()).ToList()
            .Select(x => new SelectListItem
            {
                Text = x.Ten,
                Value = x.Id.ToString(),
                Selected = x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiLeHoi = list.OrderBy(v => v.Value);
        }

        public async Task OptionCapQuanLy(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(CapQuanLyLeHoi)).Cast<CapQuanLyLeHoi>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listCapQuanLyLeHoi = list.OrderBy(v => v.Value);
            });
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await leHoiScheduleRepositoty.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ lễ hội thành công" });
                return Redirect("/HueCIT/LeHoi/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/LeHoi/Index");
            }
        }

        private void AddEditGIS(TechLife.Model.HueCIT.LeHoiDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            int layer = 12;
            string ctk = _config.GetValue<string>("ArcGisToken");
            List<TechLife.Model.HueCIT.DongBoDieuHanhLeHoiAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhLeHoiAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhLeHoiEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhLeHoiEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo.objectIds == null)
            {
                TechLife.Model.HueCIT.DongBoDieuHanhLeHoiAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhLeHoiAdd
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
                    attributes = new TechLife.Model.HueCIT.LeHoiDongBoAdd
                    {
                        id = data.id,
                        tenlehoi = data.tenlehoi,
                        diadiem = data.diadiem,
                        noidung = data.noidung,
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
                TechLife.Model.HueCIT.DongBoDieuHanhLeHoiEdit dt = new TechLife.Model.HueCIT.DongBoDieuHanhLeHoiEdit
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
                    attributes = new TechLife.Model.HueCIT.LeHoiDongBoEdit
                    {
                        objectid = chkinfo.objectIds.First(),
                        id = data.id,
                        tenlehoi = data.tenlehoi,
                        diadiem = data.diadiem,
                        noidung = data.noidung,
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
            int layer = 12;
            string ctk = _config.GetValue<string>("ArcGisToken");
            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D%27" + Id + "%27&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
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
                request.AddParameter("where", "objectid=" + chkinfo.objectIds.First());
                request.AddParameter("f", "json");
                IRestResponse response = client.Execute(request);
            }
        }
    }
}