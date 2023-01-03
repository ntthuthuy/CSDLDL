using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients.HueCIT;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class AmThucController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IAmThucRepository respository;
        private readonly IDanhMucRepository respositoryLoai;
        private readonly IFileRepository respositoryFile;
        private readonly IFileUploaderApiClient fileApiClient;

        public AmThucController(IAmThucRepository _respository,
                                IDanhMucRepository _respositoryLoai, 
                                IFileRepository _respositoryFile, 
                                IFileUploaderApiClient _fileApiClient, 
                                IUserService userService, 
                                IConfiguration configuration, 
                                ITrackingService trackingService)
            : base(userService, configuration, trackingService)
        {
            respository = _respository;
            respositoryLoai = _respositoryLoai;
            respositoryFile = _respositoryFile;
            fileApiClient = _fileApiClient;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách món ăn thức uống";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loai = !String.IsNullOrEmpty(Request.Query["loai"]) ? Convert.ToInt32(Request.Query["loai"]) : -1;
            int amthuc = !String.IsNullOrEmpty(Request.Query["amthuc"]) ? Convert.ToInt32(Request.Query["amthuc"]) : -1;
            int nguon = !String.IsNullOrEmpty(Request.Query["nguon"]) ? Convert.ToInt32(Request.Query["nguon"]) : -1;

            ViewBag.Loai = loai;
            ViewBag.AmThuc = amthuc;
            ViewBag.Nguon = nguon;

            await OptionLoai(loai);
            await OptionAmThuc(amthuc);
            await OptionNguonDongBo(nguon);

            AmThucRequest request = new AmThucRequest
            { 
                Loai = loai,
                AmThuc = amthuc,
                NguonDongBo = nguon
            };

            List<AmThucTrinhDien> obj = new List<AmThucTrinhDien>();
            obj = (await respository.Gets(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> Detail(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);
            var model = await respository.Get(ID);

            if (model != null)
            {
                ViewData["Title"] = "Thông tin món ăn thức uống " + model.TenMon;
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Thêm mới món ăn thức uống";

            var lhModel = new AmThuc();

            await OptionLoai();

            var model = new AmThucRequestMod()
            {
                DuLieuAmThuc = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Cập nhật món ăn thức uống";

            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var model = await respository.Get(ID);

            var lhModel = new AmThuc
            {
                ID = model.ID,
                TenMon = model.TenMon,
                MaLoai = (int)model.MaLoai,
                Kieu = (int)model.Kieu,
                MoTa = model.MoTa,
                ThucUong = model.ThucUong,
                Files = model.Files
            };

            if (lhModel != null)
            {
                await OptionLoai((int)lhModel.MaLoai);
            }

            var modelEdit = new AmThucRequestMod()
            {
                DuLieuAmThuc = lhModel,
                Files = new FilesUploadRequest()
            };

            return View(modelEdit);
        }

        [HttpPost]
        [Authorize(Roles = "create_amthuc,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AmThucRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuAmThuc.ID");

                if (request.DuLieuAmThuc.MaLoai == 0)
                {
                    request.DuLieuAmThuc.MaLoai = null;
                }

                request.DuLieuAmThuc.NguonDongBo = 0;

                var result = await respository.Add(request.DuLieuAmThuc);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    {
                        LoaiDoiTuong = "DL_MonAnThucUong",
                        ID = result.ID.ToString()
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }
                await Tracking("Thêm món ăn thức uống " + request.DuLieuAmThuc.TenMon);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm món ăn thức uống thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm món ăn thức uống thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/AmThuc/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/AmThuc/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/AmThuc/Add");
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.DuLieuAmThuc.MaLoai != null ? (int)request.DuLieuAmThuc.MaLoai : 0);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return Redirect("/HueCIT/AmThuc/Add");
        }

        [HttpPost]
        [Authorize(Roles = "edit_amthuc,root")]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AmThucRequestMod request, string type_sumit)
        {
            try
            {
                ModelState.Remove("DuLieuAmThuc.ID");

                if (request.DuLieuAmThuc.MaLoai == 0)
                {
                    request.DuLieuAmThuc.MaLoai = null;
                }

                var result = await respository.Edit(request.DuLieuAmThuc);

                if (result != null)
                {
                    FileUploadTargetRequestAdd target = new FileUploadTargetRequestAdd
                    {
                        LoaiDoiTuong = "DL_MonAnThucUong",
                        ID = request.DuLieuAmThuc.ID.ToString(),
                    };

                    if (request.Files != null)
                    {
                        var upload = await fileApiClient.UploadFiles(target, request.Files);
                    }
                }
                await Tracking("Cập nhật món ăn thức uống " + request.DuLieuAmThuc.TenMon);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật món ăn thức uống thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật món ăn thức uống thất bại." });
                }

                if (result != null)
                {
                    if (type_sumit == "save")
                    {
                        return Redirect("/HueCIT/AmThuc/Index");
                    }
                    else
                    {
                        return Redirect("/HueCIT/AmThuc/Add");
                    }
                }
                else
                {
                    return Redirect("/HueCIT/AmThuc/Edit?id=" + HashUtil.EncodeID(request.DuLieuAmThuc.ID.ToString()));
                }
            }
            catch (Exception ex)
            {
                await OptionLoai(request.DuLieuAmThuc.MaLoai != null ? (int)request.DuLieuAmThuc.MaLoai : 0);

                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/AmThuc/Edit?id=" + HashUtil.EncodeID(request.DuLieuAmThuc.ID.ToString()));
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_amthuc,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var result = await respository.Delete(ID);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa món ăn thức uống thành công",
            });
            await Tracking("Xóa món ăn thức uống thành công");
            return Redirect(Request.GetBackUrl());
        }

        [HttpPost]
        [Authorize(Roles = "edit_amthuc,root")]
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
            var loai = (await respositoryLoai.GetsLoaiAmThuc());
            var list = loai.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.ID.ToString(),
                Selected = (int)x.ID == seletedId ? true : false
            });

            ViewBag.listLoai = list;
        }

        public async Task OptionAmThuc(int seletedId = 0)
        {
            var amthuc = (await respository.GetsSearch());
            var list = amthuc.Select(x => new SelectListItem
            {
                Text = x.TenMon.ToString(),
                Value = x.ID.ToString(),
                Selected = (int)x.ID == seletedId ? true : false
            });

            ViewBag.listAmThuc = list;
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await respository.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ ẩm thực thành công" });
                return Redirect("/HueCIT/AmThuc/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/AmThuc/Index");
            }
        }
    }
}