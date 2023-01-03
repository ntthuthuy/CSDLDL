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
using TechLife.App.ApiClients;
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
    public class DiemGiaoDichController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IDiemGiaoDichRepository _repository;
        private readonly IDiemGiaoDichScheduleRepository _diemGiaoDichScheduleRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly IConfiguration _config;
        public DiemGiaoDichController(IConfiguration config, 
                                      IUserService userService,
                                      IDiaPhuongService diaPhuongService,
                                      IConfiguration configuration,
                                      ITrackingService trackingService,
                                      IDiemGiaoDichRepository repository,
                                      IDiemGiaoDichScheduleRepository diemGiaoDichScheduleRepository,
                                      IDanhMucRepository danhMucRepository)
            : base(userService, diaPhuongService, configuration, trackingService)
        {
            _config = config;
            _repository = repository;
            _diemGiaoDichScheduleRepository = diemGiaoDichScheduleRepository;
            _danhMucRepository = danhMucRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách điểm giao dịch";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loai = !String.IsNullOrEmpty(Request.Query["loaikhach"]) ? Convert.ToInt32(Request.Query["loaikhach"]) : -1;
            int diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Convert.ToInt32(Request.Query["diadiem"]) : -1;
            int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
            int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

            ViewBag.LoaiKhach = loai;
            ViewBag.DiaDiem = diadiem;
            ViewBag.Huyen = huyen;
            ViewBag.Nguon = nguon;

            DiemGiaoDichRequest request = new DiemGiaoDichRequest()
            {
                Loai = loai,
                DiaDiem = diadiem,
                QuanHuyenId = huyen,
                NguonDongBo = nguon,
            };

            List<DiemGiaoDichTrinhDien> obj = (await _repository.Gets(request)).ToList();

            await OptionLoai(loai);
            await OptionDiemGiaoDich(diadiem);
            await OptionHuyen(1, huyen);
            OptionDongBoDiem(nguon);

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> Detail(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);
            var model = await _repository.Get(ID);

            if (model != null)
            {
                ViewData["Title"] = "Thông tin điểm giao dịch " + model.TenDiaDiem;
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Thêm mới điểm giao dịch";

            var lhModel = new DiemGiaoDich();

            await OptionLoai();
            await OptionHuyen();
            await OptionXa(-1);

            return View(lhModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Cập nhật điểm giao dịch";

            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var model = await _repository.Get(ID);

            var lhModel = new DiemGiaoDich
            {
                ID = (int)model.ID,
                TenDiaDiem = model.TenDiaDiem,
                Loai = model.Loai,
                DienThoai = model.DienThoai,
                GioPhucVu = model.GioPhucVu,
                DiaChi = model.DiaChi,
                X = model.X,
                Y = model.Y,
                PhuongXaId = model.PhuongXaId,
                QuanHuyenId = model.QuanHuyenId,
            };

            if (lhModel != null)
            {
                await OptionHuyen();
                await OptionXa(lhModel.QuanHuyenId);
                await OptionLoai((int)lhModel.Loai);
            }

            return View(lhModel);
        }

        [HttpPost]
        [Authorize(Roles = "create_diemgiaodich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DiemGiaoDich request, string type_sumit)
        {
            try
            {
                ModelState.Remove("request.ID");

                var result = await _repository.Add(request);

                await Tracking("Thêm điểm giao dịch " + request.TenDiaDiem);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm điểm giao dịch thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm điểm giao dịch thất bại." });
                }

                if (result != null && request.X != null && request.X != 0 && request.Y != null && request.Y != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.Y,
                        y = request.X
                    };

                    TechLife.Model.HueCIT.DiemGiaoDichDongBoAdd data = new Model.HueCIT.DiemGiaoDichDongBoAdd
                    {
                        id = request.ID,
                        tendiadiem = request.TenDiaDiem,
                        diachi = request.DiaChi,
                        dienthoai = request.DienThoai,
                        giophucvu = request.GioPhucVu,
                    };

                    AddEditGIS(data, geo);
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/DiemGiaoDich/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/DiemGiaoDich/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/DiemGiaoDich/Add");
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.Loai);
                await OptionHuyen();
                await OptionXa(request.QuanHuyenId);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/HueCIT/DiemGiaoDich/Add");
        }

        [HttpPost]
        [Authorize(Roles = "edit_diemgiaodich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiemGiaoDich request, string type_sumit)
        {
            try
            {
                var result = await _repository.Edit(request);

                await Tracking("Cập nhật điểm giao dịch " + request.TenDiaDiem);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật điểm giao dịch thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật điểm giao dịch thất bại." });
                }

                if (result != null && request.X != null && request.X != 0 && request.Y != null && request.Y != 0)
                {
                    TechLife.Model.HueCIT.ToaDo geo = new TechLife.Model.HueCIT.ToaDo
                    {
                        x = request.Y,
                        y = request.X
                    };

                    TechLife.Model.HueCIT.DiemGiaoDichDongBoAdd data = new Model.HueCIT.DiemGiaoDichDongBoAdd
                    {
                        id = request.ID,
                        tendiadiem = request.TenDiaDiem,
                        diachi = request.DiaChi,
                        dienthoai = request.DienThoai,
                        giophucvu = request.GioPhucVu,
                    };

                    AddEditGIS(data, geo);
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/DiemGiaoDich/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/DiemGiaoDich/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/DiemGiaoDich/Edit?id=" + HashUtil.EncodeID(request.ID.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.Loai);
                await OptionHuyen();
                await OptionXa(request.QuanHuyenId);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/DiemGiaoDich/Edit?id=" + HashUtil.EncodeID(request.ID.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_diemgiaodich,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var result = await _repository.Delete(ID);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa điểm giao dịch thành công",
            });

            RemoveGIS(id, (int)LinhVucKinhDoanh.NhaHang);

            await Tracking("Xóa điểm giao dịch thành công");
            return Redirect(Request.GetBackUrl());
        }

        private async Task OptionLoai(int seletedId = 0)
        {
            var list = (await _danhMucRepository.GetsLoaiDiemGiaoDich())
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.Id.ToString(),
                    Selected = x.Id == seletedId ? true : false
                });
            ViewBag.listLoai = list.OrderBy(v => v.Value);

        }

        private async Task OptionDiemGiaoDich(int seletedId = 0)
        {
            var diadiem = (await _repository.GetsSearch());
            var list = diadiem.Select(x => new SelectListItem
            {
                Text = x.DiaDiem.ToString(),
                Value = x.ID.ToString(),
                Selected = (int)x.ID == seletedId ? true : false
            });

            ViewBag.listDiaDiem = list;
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _diemGiaoDichScheduleRepository.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ điểm giao dịch thành công" });
                return Redirect("/HueCIT/DiemGiaoDich/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DiemGiaoDich/Index");
            }
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

        private void AddEditGIS(TechLife.Model.HueCIT.DiemGiaoDichDongBoAdd data, TechLife.Model.HueCIT.ToaDo geo)
        {
            int layer = 10;
            string ctk = _config.GetValue<string>("ArcGisToken");
            List<TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichAdd> dta = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichAdd>();
            List<TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichEdit> dte = new List<TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichEdit>();

            var check = new RestClient("https://gishue.thuathienhue.gov.vn/server/rest/services/BanDoDuLich_HueCIT/DuLich_DichVu/FeatureServer/" + layer + "/query?where=id%3D" + data.id + "&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Foot&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&havingClause=&gdbVersion=&historicMoment=&returnDistinctValues=false&returnIdsOnly=true&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&multipatchOption=xyFootprint&resultOffset=&resultRecordCount=&returnTrueCurves=false&returnExceededLimitFeatures=false&quantizationParameters=&returnCentroid=false&sqlFormat=none&resultType=&featureEncoding=esriDefault&datumTransformation=&f=json");
            check.Timeout = -1;
            var chk = new RestRequest(Method.GET);
            //chk.AddHeader("Cookie", "AGS_ROLES=\"419jqfa+uOZgYod4xPOQ8Q==\"");
            chk.AddHeader("Cookie", ctk);
            IRestResponse res = check.Execute(chk);
            var chkinfo = JsonConvert.DeserializeObject<TechLife.Model.HueCIT.CheckResponse>(res.Content);

            if (chkinfo.objectIds == null)
            {
                TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichAdd dt = new TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichAdd
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
                    attributes = new TechLife.Model.HueCIT.DiemGiaoDichDongBoAdd
                    {
                        id = data.id,
                        tendiadiem = data.tendiadiem,
                        diachi = data.diachi,
                        dienthoai = data.dienthoai,
                        giophucvu = data.giophucvu,
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
                TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichEdit dt = new TechLife.Model.HueCIT.DongBoDieuHanhDiemGiaoDichEdit
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
                    attributes = new TechLife.Model.HueCIT.DiemGiaoDichDongBoEdit
                    {
                        objectid = chkinfo.objectIds.First(),
                        id = data.id,
                        tendiadiem = data.tendiadiem,
                        diachi = data.diachi,
                        dienthoai = data.dienthoai,
                        giophucvu = data.giophucvu,
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

            int layer = 10;
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
    }
}