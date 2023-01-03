using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Areas.HueCIT.Schedules;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Common.Enums.HueCIT;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class DoanhNghiepController : BaseController
    {
        private const int _pageSize = 10;
        private const int _nguondongbo = (int)NguonDongBo.HTTTDN;

        private readonly IDoanhNghiepScheduleRepository _doanhNghiepScheduleRepository;
        private readonly IDoanhNghiepRepository _doanhNghiepRepository;
        private readonly IDoanhNghiepLoaiHinhRepository _doanhNghiepLoaiHinhRepository;
        private readonly IDoanhNghiepNganhNgheRepository _doanhNghiepNganhNgheRepository;
        private readonly IDoanhNghiepVanBanRepository _doanhNghiepVanBanRepository;
        private readonly IQuanHuyenRepository _quanHuyenRepository;
        private readonly IDoanhNghiepTrangThaiRepository _doanhNghiepTrangThaiRepository;
        public DoanhNghiepController(IUserService userService
                                    , IConfiguration configuration
                                    , IDoanhNghiepScheduleRepository doanhNghiepScheduleRepository
                                    , IDoanhNghiepRepository doanhNghiepRepository
                                    , IDoanhNghiepLoaiHinhRepository doanhNghiepLoaiHinhRepository
                                    , IDoanhNghiepNganhNgheRepository doanhNghiepNganhNgheRepository
                                    , IDoanhNghiepVanBanRepository doanhNghiepVanBanRepository
                                    , IQuanHuyenRepository quanHuyenRepository
                                    , IDoanhNghiepTrangThaiRepository doanhNghiepTrangThaiRepository)
                                    : base(userService
                                         , configuration)
        {
            _doanhNghiepScheduleRepository = doanhNghiepScheduleRepository;
            _doanhNghiepRepository = doanhNghiepRepository;
            _doanhNghiepLoaiHinhRepository = doanhNghiepLoaiHinhRepository;
            _doanhNghiepNganhNgheRepository = doanhNghiepNganhNgheRepository;
            _doanhNghiepVanBanRepository = doanhNghiepVanBanRepository;
            _quanHuyenRepository = quanHuyenRepository;
            _doanhNghiepTrangThaiRepository = doanhNghiepTrangThaiRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Doanh nghiệp";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            string tukhoa = !String.IsNullOrEmpty(Request.Query["tukhoa"]) ? Request.Query["tukhoa"].ToString() : "";
            int loai = !String.IsNullOrEmpty(Request.Query["loai"]) ? Convert.ToInt32(Request.Query["loai"]) : -1;
            int huyen = !String.IsNullOrEmpty(Request.Query["huyen"]) ? Convert.ToInt32(Request.Query["huyen"]) : -1;
            int nganhnghe = !String.IsNullOrEmpty(Request.Query["nganhnghe"]) ? Convert.ToInt32(Request.Query["nganhnghe"]) : -1;
            int trangthai = !String.IsNullOrEmpty(Request.Query["trangthai"]) ? Convert.ToInt32(Request.Query["trangthai"]) : -1;

            ViewBag.TuKhoa = tukhoa;
            ViewBag.Loai = loai;
            ViewBag.Huyen = huyen;
            ViewBag.NganhNghe = nganhnghe;
            ViewBag.TrangThai = trangthai;

            await OptionLoai(loai);
            await OptionNganhNghe(nganhnghe);
            await OptionHuyen(huyen);

            DoanhNghiepRequest req = new DoanhNghiepRequest
            {
                tukhoa = tukhoa,
                loai = loai,
                diachi = huyen,
                nganhnghe = nganhnghe,
                huyen = huyen,
                trangthai = trangthai
            };

            List<DoanhNghiepTrinhDien> obj = new List<DoanhNghiepTrinhDien>();
            obj = (await _doanhNghiepRepository.Gets(req)).ToList();

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _doanhNghiepRepository.Get(id);

            if (model != null)
            {
                ViewData["Title"] = "Doanh nghiệp";
            }

            model.DSVanBan = (await _doanhNghiepVanBanRepository.GetsByMaDoanhNghiep(model.MaSoDoanhNghiep)).ToList();

            return View(model);
        }
        
        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _doanhNghiepScheduleRepository.GetDataDoanhNghiep();

                await _doanhNghiepScheduleRepository.GetDataVanBan();

                TempData.AddAlert(new Result<string>() { IsSuccessed = true, Message = "Đồng bộ doanh nghiệp thành công" });
                return Redirect("/HueCIT/DoanhNghiep/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>() { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DoanhNghiep/Index");
            }
        }

        private async Task OptionNganhNghe(int seletedId = 0)
        {
            var nganhnghe = (await _doanhNghiepNganhNgheRepository.Gets()).ToList();
            var list = nganhnghe.Select(x => new SelectListItem
            {
                Text = x.TenNganhNghe.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.ListNganhNghe = list;
        }

        private async Task OptionLoai(int seletedId = 0)
        {
            var loai = (await _doanhNghiepLoaiHinhRepository.Gets()).ToList();
            var list = loai.Select(x => new SelectListItem
            {
                Text = x.LoaiHinh.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false,
            });

            ViewBag.ListLoai = list;
        }

        private async Task OptionHuyen(int seletedId = 0)
        {
            var huyen = (await _quanHuyenRepository.Gets()).ToList();
            var list = huyen.Select(x => new SelectListItem
            {
                Text = x.TenQuanHuyen,
                Value = x.MaQuanHuyen.ToString(),
                Selected = x.MaQuanHuyen == seletedId ? true : false,
            });

            ViewBag.ListHuyen = list;
        }
        
        private async Task optionTrangThai()
        {
            var list = (await _doanhNghiepTrangThaiRepository.Gets()).ToList();
            ViewBag.ListTrangThai = list;
        }
    }
}
