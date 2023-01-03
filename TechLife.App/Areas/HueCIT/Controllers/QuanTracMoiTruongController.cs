using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
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
using TechLife.Service;
using X.PagedList;
using static TechLife.Common.Enums.HueCIT.QuanTracMoiTruong;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class QuanTracMoiTruongController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IQuanTracMoiTruongRepository _quanTracMoiTruongRepository;
        public QuanTracMoiTruongController(IUserService userService, 
                                           IDiaPhuongApiClient diaPhuongApiClient, 
                                           IDonViTinhApiClient donViTinhApiClient, 
                                           ILoaiHinhApiClient loaiHinhApiClient, 
                                           IDichVuApiClient dichVuApiClient, 
                                           INgoaiNguApiClient ngoaiNguApiClient, 
                                           ITrinhDoApiClient trinhDoApiClient, 
                                           IBoPhanApiClient boPhanApiClient, 
                                           ILoaiPhongApiClient loaiPhongApiClient, 
                                           IMucDoThongThaoNgoaiNguApiClient mucDoThongThaoNgoaiNguApiClient, 
                                           ITienNghiApiClient tienNghiApiClient, 
                                           IHuongDanVienApiClient huongDanVienApiClient, 
                                           IDiemVeSinhApiClient diemVeSinhApiClient, 
                                           ILoaiGiuongApiClient loaiGiuongApiClient, 
                                           IDuLieuDuLichApiClient csdlDuLichApiClient, 
                                           IQuocTichApiClient quocTichApiClient, 
                                           ILoaiDichVuApiClient loaiDichVuApiClient, 
                                           IDanhMucApiClient danhMucApiClient, 
                                           ILoaiHinhLaoDongApiClient loaiHinhLaoDongApiClient, 
                                           ITinhChatLaoDongApiClient tinhChatLaoDongApiClient, 
                                           IDiaPhuongService diaPhuongService, 
                                           IConfiguration configuration, 
                                           IFileUploadService fileUploadService, 
                                           INhaCungCapService nhaCungCapService, 
                                           ITrackingService trackingService,
                                           IQuanTracMoiTruongRepository quanTracMoiTruongRepository) 
            : base(userService, 
                  diaPhuongApiClient, 
                  donViTinhApiClient, 
                  loaiHinhApiClient, 
                  dichVuApiClient, 
                  ngoaiNguApiClient, 
                  trinhDoApiClient, 
                  boPhanApiClient, 
                  loaiPhongApiClient, 
                  mucDoThongThaoNgoaiNguApiClient, 
                  tienNghiApiClient, 
                  huongDanVienApiClient, 
                  diemVeSinhApiClient, 
                  loaiGiuongApiClient, 
                  csdlDuLichApiClient, 
                  quocTichApiClient, 
                  loaiDichVuApiClient, 
                  danhMucApiClient, 
                  loaiHinhLaoDongApiClient, 
                  tinhChatLaoDongApiClient, 
                  diaPhuongService, 
                  configuration, 
                  fileUploadService, 
                  nhaCungCapService, 
                  trackingService)
        {
            _quanTracMoiTruongRepository = quanTracMoiTruongRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Quan trắc môi trường";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;
            string diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Request.Query["diadiem"].ToString() : "BO09_BO09";

            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;
            ViewBag.DiaDiem = diadiem;

            QuanTracMoiTruongRequest request = new QuanTracMoiTruongRequest
            {
                TuNgay = tungay,
                DenNgay = denngay,
                DiaDiem = diadiem
            };

            await OptionDiaDiemQuanTrac(diadiem);
            await DanhSachTrangThai();

            List<DanhSachQuanTracMoiTruong> obj = await _quanTracMoiTruongRepository.DanhSachTheoTenThongSo(request);

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }

        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _quanTracMoiTruongRepository.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ quan trắc môi trường thành công" });
                return Redirect("/HueCIT/QuanTracMoiTruong/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/QuanTracMoiTruong/Index");
            }
        }

        private async Task OptionDiaDiemQuanTrac(string seletedId = "")
        {
            var quantrac = (await _quanTracMoiTruongRepository.Gets()).ToList();
            var list = quantrac.GroupBy(x => new { x.Node, x.TenNode }).Select(x => new SelectListItem
            {
                Text = x.Key.TenNode,
                Value = x.Key.Node,
                Selected = x.Key.Node == seletedId ? true : false
            }).ToList();

            ViewBag.ListDiaDiemQuanTrac = list;
        }

        private async Task DanhSachTrangThai()
        {
            await Task.Run(() =>
            {
                var trangthai = Enum.GetValues(typeof(TrangThaiQuanTrac))
                    .Cast<TrangThaiQuanTrac>()
                    .Select(x => new QuanTracMoiTruongTrangThai { Id = x.ToString(), TenTrangThai = StringEnum.GetStringValue(x) })
                    .ToList();

                ViewBag.ListTrangThaiQuanTrac = trangthai;
            });
        }
    }
}
