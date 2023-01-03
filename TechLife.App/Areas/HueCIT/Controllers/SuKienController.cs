using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.ApiClients.HueCIT;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.App.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
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
    public class SuKienController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly ISuKienRepository respository;
        private readonly IDanhMucRepository respositoryChuDe;
        private readonly IFileRepository respositoryFile;
        private readonly IFileUploaderApiClient fileApiClient;

        public SuKienController(ISuKienRepository _respository, IDanhMucRepository _respositoryChuDe, IFileRepository _respositoryFile, 
                               IFileUploaderApiClient _fileApiClient, IUserService userService, IDiaPhuongService diaPhuongService,
                               IConfiguration configuration, ITrackingService trackingService)
            : base(userService, diaPhuongService, configuration, trackingService)
        {
            respository = _respository;
            respositoryChuDe = _respositoryChuDe;
            respositoryFile = _respositoryFile;
            fileApiClient = _fileApiClient;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách sự kiện";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int chude = !String.IsNullOrEmpty(Request.Query["chude"]) ? Convert.ToInt32(Request.Query["chude"]) : -1;
            string diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Request.Query["diadiem"].ToString() : "";
            int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;

            ViewBag.ChuDe = chude;
            ViewBag.DiaDiem = diadiem;
            ViewBag.Huyen = huyen;
            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;

            await OptionChuDe(chude);
            await OptionHuyen(1, huyen);

            SuKienRequest request = new SuKienRequest
            { 
                MaChuDe = chude,
                DiaDiem = diadiem,
                BatDau = tungay,
                KetThuc = denngay,
                QuanHuyenId = huyen,
            };

            List<SuKienTrinhDien> obj = new List<SuKienTrinhDien>();
            obj = (await respository.Gets(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> Detail(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));
            var model = await respository.Get(Id);

            if (model != null)
            {
                ViewData["Title"] = "Thông tin sự kiện " + model.TieuDe;
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Thêm mới sự kiện";

            var lhModel = new SuKien();
            lhModel.BatDau = DateTime.Now;
            lhModel.KetThuc = DateTime.Now;

            await OptionChuDe();
            await OptionHuyen();
            await OptionXa(-1);

            var model = new SuKienRequestMod()
            {
                DuLieuSuKien = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Cập nhật sự kiện";

            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var model = await respository.Get(Id);

            var lhModel = new SuKien
            { 
                ID = model.ID,
                TieuDe = model.TieuDe,
                MaChuDe = model.MaChuDe == null ? -1 : (int)model.MaChuDe,
                TrangThai = model.TrangThai,
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
                await OptionChuDe((int)lhModel.MaChuDe);
                await OptionHuyen();
                await OptionXa(lhModel.QuanHuyenId);
            }

            var modelEdit = new SuKienRequestMod()
            {
                DuLieuSuKien = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(modelEdit);
        }

        [HttpPost]
        [Authorize(Roles = "create_sukien,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SuKienRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuSuKien.Id");
                if (request.DuLieuSuKien.MaChuDe == 0)
                {
                    request.DuLieuSuKien.MaChuDe = null;
                }

                var result = await respository.Add(request.DuLieuSuKien);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    { 
                        LoaiDoiTuong = "DL_SuKien",
                        ID = result.ID.ToString()
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }
                await Tracking("Thêm sự kiện " + request.DuLieuSuKien.TieuDe);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm sự kiện thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm sự kiện thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/SuKien/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/SuKien/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/SuKien/Add");
                }
            }
            catch (Exception ex)
            {
                await OptionChuDe(request.DuLieuSuKien.MaChuDe != null ? (int)request.DuLieuSuKien.MaChuDe : 0);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/HueCIT/SuKien/Add");
        }

        [HttpPost]
        [Authorize(Roles = "edit_sukien,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SuKienRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuSuKien.Id");

                if (request.DuLieuSuKien.MaChuDe == 0)
                {
                    request.DuLieuSuKien.MaChuDe = null;
                }

                var result = await respository.Edit(request.DuLieuSuKien);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    {
                        LoaiDoiTuong = "DL_SuKien",
                        ID = request.DuLieuSuKien.ID.ToString(),
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }

                await Tracking("Cập nhật lễ hội " + request.DuLieuSuKien.TieuDe);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật sự kiện thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật sự kiện thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/SuKien/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/SuKien/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/SuKien/Edit?id=" + HashUtil.EncodeID(request.DuLieuSuKien.ID.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionChuDe(request.DuLieuSuKien.MaChuDe != null ? (int)request.DuLieuSuKien.MaChuDe : 0);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/SuKien/Edit?id=" + HashUtil.EncodeID(request.DuLieuSuKien.ID.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_sukien,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            int Id = Convert.ToInt32(HashUtil.DecodeID(id));

            var result = await respository.Delete(Id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa sự kiện thành công",
            });
            await Tracking("Xóa sự kiện thành công");
            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Authorize(Roles = "edit_sukien,root")]
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

        public async Task OptionChuDe(int seletedId = 0)
        {
            var amthuc = (await respositoryChuDe.GetsChuDeSuKien());
            var list = amthuc.Select(x => new SelectListItem
            {
                Text = x.ChuDe.ToString(),
                Value = x.ID.ToString(),
                Selected = (int)x.ID == seletedId ? true : false
            });

            ViewBag.listChuDe = list;
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ lễ hội thành công" });
                return Redirect("/HueCIT/SuKien/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/SuKien/Index");
            }
        }
    }
}