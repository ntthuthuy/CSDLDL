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
    public class TyGiaController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly ITyGiaRepository respository;

        public TyGiaController(ITyGiaRepository _respository, IUserService userService, 
                               IConfiguration configuration, ITrackingService trackingService)
            : base(userService, configuration, trackingService)
        {
            respository = _respository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách tỷ giá";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            string ngay = !String.IsNullOrEmpty(Request.Query["ngay"]) ? Request.Query["ngay"].ToString() : null;

            ViewBag.Ngay = ngay;

            TyGiaRequest request = new TyGiaRequest
            {
                Ngay = ngay
            };

            List<TyGia> obj = new List<TyGia>();
            obj = (await respository.Gets(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "view_tygia,root")]
        public async Task<TyGia> Get(int id)
        {
            try
            {
                var result = await respository.Get(id);

                return result;
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return null;
        }

        [HttpPost]
        [Authorize(Roles = "create_tygia,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TyGia request)
        {
            try
            {
                ModelState.Remove("request.ID");

                var result = await respository.Add(request);

                await Tracking("Thêm tỷ giá " + request.TenNgoaiTe);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm tỷ giá thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm tỷ giá thất bại." });
                }

                return Redirect("/HueCIT/TyGia/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }

            return Redirect("/HueCIT/TyGia/Index");
        }

        [HttpPost]
        [Authorize(Roles = "edit_tygia,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TyGia request, string type_sumit)
        {
            try
            {
                var result = await respository.Edit(request);

                await Tracking("Cập nhật tỷ giá " + request.TenNgoaiTe);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật tỷ giá thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật tỷ giá thất bại." });
                }

                return Redirect("/HueCIT/TyGia/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/TyGia/Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_tygia,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var result = await respository.Delete(ID);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa tỷ giá thành công",
            });
            await Tracking("Xóa tỷ giá thành công");
            return Redirect(Request.GetBackUrl());
        }

    }
}