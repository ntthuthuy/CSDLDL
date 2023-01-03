using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class HienTruongController : BaseController
    {
        private readonly int _pageSize = 10;

        private readonly IPhanAnhHienTruongRepository phanAnhHienTruongRepository;
        private readonly IPhanAnhHienTruongLinhVucRepository phanAnhHienTruongLinhVucRepository;
        private readonly IPhanAnhHienTruongHinhAnhRepository phanAnhHienTruongHinhAnhRepository;
        private readonly IPhanAnhHienTruongScheduleRepository phanAnhHienTruongScheduleRepository;
        public HienTruongController(IUserService userService, IConfiguration configuration,
                                    IPhanAnhHienTruongRepository _phanAnhHienTruongRepository,
                                    IPhanAnhHienTruongLinhVucRepository _phanAnhHienTruongLinhVucRepository, 
                                    IPhanAnhHienTruongHinhAnhRepository _phanAnhHienTruongHinhAnhRepository,
                                    IPhanAnhHienTruongScheduleRepository _phanAnhHienTruongScheduleRepository)
            : base(userService, configuration)
        {
            phanAnhHienTruongRepository = _phanAnhHienTruongRepository;
            phanAnhHienTruongLinhVucRepository = _phanAnhHienTruongLinhVucRepository;
            phanAnhHienTruongHinhAnhRepository = _phanAnhHienTruongHinhAnhRepository;
            phanAnhHienTruongScheduleRepository = _phanAnhHienTruongScheduleRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Phản ánh hiện trường Hue-S";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int linhvuc = !String.IsNullOrEmpty(Request.Query["linhvuc"]) ? Convert.ToInt32(Request.Query["linhvuc"]) : -1;
            string keyword = !String.IsNullOrEmpty(Request.Query["keyword"]) ? Request.Query["keyword"].ToString() : "";
            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;
            int loaixuly = !String.IsNullOrEmpty(Request.Query["loaixuly"]) ? Convert.ToInt32(Request.Query["loaixuly"]) : -1;

            ViewBag.LinhVuc = linhvuc;
            ViewBag.Keyword = keyword;
            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;
            ViewBag.LoaiXuLy = loaixuly;

            PhanAnhHienTruongTrinhDienRequest request = new PhanAnhHienTruongTrinhDienRequest
            {
                Keywork = keyword,
                LinhVuc = linhvuc,
                TuNgay = tungay,
                DenNgay = denngay,
                LoaiXuLy = loaixuly,
            };

            await OptionLinhVuc(linhvuc);
            OptionLoaiXuLy(loaixuly);

            List<PhanAnhHienTruongTrinhDien> obj = (await phanAnhHienTruongRepository.GetsPhanAnhHienTruongTrinhDien(request)).ToList();
           
            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<PhanAnhHienTruongTrinhDien> Get(int id) 
        {
            return await phanAnhHienTruongRepository.GetPhanAnhHienTruongTrinhDien(id);
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await phanAnhHienTruongScheduleRepository.GetData();

                await phanAnhHienTruongScheduleRepository.GetDataWait();

                TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Đồng bộ phản ánh thành công" });
                return Redirect("/HueCIT/HienTruong/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/HienTruong/Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMap(PhanAnhHienTruongEditRequest request)
        {
            try
            {
                var result = await phanAnhHienTruongRepository.Edit(request);

                await Tracking("Cập nhật phản ánh " + result.TieuDe);

                if (result != null)
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Cập nhật phản ánh thành công" });
                }
                else
                {
                    TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = "Cập nhật phản ánh thất bại." });
                }

                return Redirect("/HueCIT/HienTruong/Detail?id=" + request.ID);
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });

                return Redirect("/HueCIT/HienTruong/Detail?id=" + request.ID);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = "Chi tiết phản ánh hiện trường";
            var phananh = await phanAnhHienTruongRepository.GetPhanAnhHienTruongTrinhDien(id);
            var filedinhkems = (await phanAnhHienTruongHinhAnhRepository.GetsPhanAnhHienTruongHinhAnhByPhanAnhId(id)).ToList();

            PhanAnhHienTruongTrinhDienMod model = new PhanAnhHienTruongTrinhDienMod
            {
                PhanAnhHienTruongTrinhDien = phananh,
                FileDinhKemTrinhDien = filedinhkems.Select(x => new FileDinhKemTrinhDien {
                    Filename = x.HinhAnh,
                    IsKetQua = x.IsKetQua
                }).ToList(),
            };

            return View(model);
        }

        public async Task OptionLinhVuc(int seletedId = 0)
        {
            var list = (await phanAnhHienTruongLinhVucRepository.GetsLinhVucPhanAnhHienTruong())
                                                                .Select(x => new SelectListItem
                                                                {
                                                                    Text = x.LinhVuc,
                                                                    Value = x.Id.ToString(),
                                                                    Selected = x.Id == seletedId ? true : false
                                                                });
            ViewBag.listLinhVuc = list;
        }

        public void OptionLoaiXuLy(int seletedId = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem
            {
                Text = "Đã xử lý",
                Value = "1",
                Selected = seletedId == 1 ? true : false 
            });

            list.Add(new SelectListItem
            {
                Text = "Đang xử lý",
                Value = "0",
                Selected = seletedId == 0 ? true : false
            });

            ViewBag.listLoaiXuLy = list;
        }
    }
}