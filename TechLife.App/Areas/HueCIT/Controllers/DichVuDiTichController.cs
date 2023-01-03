using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.App.Controllers;
using TechLife.Common;
using TechLife.Service;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Controllers
{
    [Area("HueCIT")]
    public class DichVuDiTichController : BaseController
    {
        private readonly int PERPAGE = 10;

        private readonly IDichVuDiTichRepository _dichVuDiTichRepository;
        public DichVuDiTichController(IUserService userService,
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
                                      IDichVuDiTichRepository dichVuDiTichRepository)
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
            _dichVuDiTichRepository = dichVuDiTichRepository;
        }

        #region CRUD
        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Danh sách dịch vụ di tích";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            List<DichVuDiTichTrinhDien> list = new List<DichVuDiTichTrinhDien>();
            list = (await _dichVuDiTichRepository.Gets()).ToList();

            return View(list.ToPagedList(pageNumber, PERPAGE));
        }
        #endregion


        #region ĐỒNG BỘ
        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _dichVuDiTichRepository.GetData();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ thành công" });
                return Redirect("/HueCIT/DichVuDiTich/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/DichVuDiTich/Index");
            }
        }
        #endregion
    }
}
