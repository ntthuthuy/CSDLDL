using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
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
    public class DuongDayNongController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IDuongDayNongRepository _repository;
        public DuongDayNongController(IUserService userService, IConfiguration configuration, ITrackingService trackingService,
                            IDuongDayNongRepository repository)
            : base(userService, configuration, trackingService)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách đường dây nóng";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int nhomdonvi = !String.IsNullOrEmpty(Request.Query["nhomdonvi"]) ? Convert.ToInt32(Request.Query["nhomdonvi"]) : -1;
            int donvi = !String.IsNullOrEmpty(Request.Query["donvi"]) ? Convert.ToInt32(Request.Query["donvi"]) : -1;

            ViewBag.NhomDonVi = nhomdonvi;
            ViewBag.DonVi = donvi;

            DuongDayNongRequest request = new DuongDayNongRequest()
            {
                NhomDonVi = nhomdonvi,
                DonVi = donvi
            };

            List<DuongDayNongTrinhDien> obj = (await _repository.Gets(request)).ToList();

            await OptionNhom(nhomdonvi);
            await OptionDonVi(donvi);

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "view_duongdaynong,root")]
        public async Task<DuongDayNongTrinhDien> Get(int id)
        {
            try
            {
                var result = await _repository.Get(id);

                return result;
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }
            return null;
        }

        [HttpPost]
        [Authorize(Roles = "create_duongdaynong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DuongDayNongTrinhDien request)
        {
            try
            {
                ModelState.Remove("request.ID");

                DuongDayNong item = new DuongDayNong
                { 
                    DonViTiepNhan = request.DonViTiepNhan,
                    NhomDonVi = request.Nhom,
                    DienThoai = request.DienThoai,
                    DiaChi = request.DiaChi
                };

                var result = await _repository.Add(item);

                await Tracking("Thêm đường dây nóng " + request.DonViTiepNhan);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Thêm đường dây nóng thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Thêm đường dây nóng thất bại." });
                }

                return Redirect("/HueCIT/DuongDayNong/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
            }

            return Redirect("/HueCIT/DuongDayNong/Index");
        }

        [HttpPost]
        [Authorize(Roles = "edit_duongdaynong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DuongDayNongTrinhDien request, string type_sumit)
        {
            try
            {
                DuongDayNong item = new DuongDayNong
                {
                    ID = request.ID,
                    DonViTiepNhan = request.DonViTiepNhan,
                    NhomDonVi = request.Nhom,
                    DienThoai = request.DienThoai,
                    DiaChi = request.DiaChi
                };

                var result = await _repository.Edit(item);

                await Tracking("Cập nhật đường dây nóng " + request.DonViTiepNhan);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật đường dây nóng thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật đường dây nóng thất bại." });
                }

                return Redirect("/HueCIT/DuongDayNong/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/DuongDayNong/Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "delete_duongdaynong,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            string Id = HashUtil.DecodeID(id);
            int ID = Convert.ToInt32(Id);

            var result = await _repository.Delete(ID);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = true,
                Message = "Xóa đường dây nóng thành công",
            });
            await Tracking("Xóa đường dây nóng thành công");
            return Redirect(Request.GetBackUrl());
        }

        private async Task OptionNhom(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(NhomDuongDayNong)).Cast<NhomDuongDayNong>()
                    .Select(x => new SelectListItem()
                    {
                        Text = StringEnum.GetStringValue(x),
                        Value = Convert.ToInt32(x).ToString(),
                        Selected = (int)x == seletedId ? true : false
                    });
                ViewBag.listLoai = list.OrderBy(v => v.Value);
            });
        }

        private async Task OptionDonVi(int seletedId = 0)
        {
            var diadiem = (await _repository.GetsSearch());
            var list = diadiem.Select(x => new SelectListItem
            {
                Text = x.DonViTiepNhan.ToString(),
                Value = x.ID.ToString(),
                Selected = (int)x.ID == seletedId ? true : false
            });

            ViewBag.listDonVi = list;
        }
    }
}