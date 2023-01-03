using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Areas.HueCIT.Repository;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class ThoiTietController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IThoiTietRepository _thoiTietRepository;
        private readonly IThoiTietSymbolRepository _thoiTietSymbolRepository;

        public ThoiTietController(IConfiguration configuration,
                                  IUserService userService,
                                  IThoiTietRepository thoiTietRepository,
                                  IThoiTietSymbolRepository thoiTietSymbolRepository) : base(userService, configuration)
        {
            _thoiTietRepository = thoiTietRepository;
            _thoiTietSymbolRepository = thoiTietSymbolRepository;
        }
        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Dự báo thời tiết";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;

            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;

            ThoiTietRequest request = new ThoiTietRequest
            {
                TuNgay = tungay,
                DenNgay = denngay
            };

            List<ThoiTietTrinhDien> obj = (await _thoiTietRepository.GetsThoiTietTrinhDien(request)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _thoiTietSymbolRepository.GetDataSymbol();

                await _thoiTietRepository.GetData();

                TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Đồng bộ thời tiết thành công" });

                return Redirect("/HueCIT/ThoiTiet/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/ThoiTiet/Index");
            }
        }
    }
}
