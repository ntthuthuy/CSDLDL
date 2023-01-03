using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class VeController : BaseController
    {
        private readonly int _pageSize = 10;
        private readonly IVeDiTichRepository _veDiTichRepository;
        private readonly IVeDiTichLoaiRepository _veDiTichLoaiRepository;
        private readonly IVeDiTichDiaDiemRepository _veDiTichDiaDiemRepository;
        public VeController(IUserService userService, 
                            IConfiguration configuration,
                            IVeDiTichRepository veDiTichRepository,
                            IVeDiTichLoaiRepository veDiTichLoaiRepository,
                            IVeDiTichDiaDiemRepository veDiTichDiaDiemRepository)
            : base(userService, configuration)
        {
            _veDiTichRepository = veDiTichRepository;
            _veDiTichLoaiRepository = veDiTichLoaiRepository;
            _veDiTichDiaDiemRepository = veDiTichDiaDiemRepository;
        }

        public async Task<IActionResult> Index(int? trang)
        {
            ViewData["Title"] = "Vé điện tử tham quan di tích";

            if (trang == null) trang = 1;
            int pageNumber = (trang ?? 1);

            int loaikhach = !String.IsNullOrEmpty(Request.Query["loaikhach"]) ? Convert.ToInt32(Request.Query["loaikhach"]) : -1;
            int diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Convert.ToInt32(Request.Query["diadiem"]) : -1;
            string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
            string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;

            ViewBag.LoaiKhach = loaikhach;
            ViewBag.DiaDiem = diadiem;
            ViewBag.TuNgay = tungay;
            ViewBag.DenNgay = denngay;
            VeDiTichRequest request = new VeDiTichRequest()
            {
                LoaiKhach = loaikhach,
                DiaDiem = diadiem,
                TuNgay = tungay,
                DenNgay = denngay
            };

            List<VeDiTichTrinhDien> obj = (await _veDiTichRepository.Gets(request)).ToList();

            await OptionLoaiKhach(loaikhach);
            await OptionDiaDiem(diadiem);

            return View(obj.ToPagedList(pageNumber, _pageSize));
        }
        public async Task<IActionResult> DongBo()
        {
            try
            {
                await _veDiTichLoaiRepository.GetDataLoaiVe();

                await _veDiTichDiaDiemRepository.GetDataDiaDiem();

                await _veDiTichRepository.GetDataVeDiTich();

                TempData.AddAlert(new Result<string> { IsSuccessed = true, Message = "Đồng bộ vé di tích thành công" });
                return Redirect("/HueCIT/Ve/Index");
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/Ve/Index");
            }
        }
        public async Task<IActionResult> VeExcel()
        {
            try
            {
                int loaikhach = !String.IsNullOrEmpty(Request.Query["loaikhach"]) ? Convert.ToInt32(Request.Query["loaikhach"]) : -1;
                int diadiem = !String.IsNullOrEmpty(Request.Query["diadiem"]) ? Convert.ToInt32(Request.Query["diadiem"]) : -1;
                string tungay = !String.IsNullOrEmpty(Request.Query["tungay"]) ? Request.Query["tungay"].ToString() : null;
                string denngay = !String.IsNullOrEmpty(Request.Query["denngay"]) ? Request.Query["denngay"].ToString() : null;

                VeDiTichRequest veReq = new VeDiTichRequest()
                {
                    LoaiKhach = loaikhach,
                    DiaDiem = diadiem,
                    TuNgay = tungay,
                    DenNgay = denngay,
                };

                var data = await _veDiTichRepository.Gets(veReq);
                if (data.Any())
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("VeDiTich");
                        var currentRow = 1;
                        worksheet.Cell(currentRow, 1).Value = "ID";
                        worksheet.Cell(currentRow, 2).Value = "LOẠI VÉ";
                        worksheet.Cell(currentRow, 3).Value = "NGÀY BÁN";
                        worksheet.Cell(currentRow, 4).Value = "ĐỊA ĐIỂM";
                        worksheet.Cell(currentRow, 5).Value = "SỐ LƯỢNG";
                        worksheet.Cell(currentRow, 6).Value = "TỔNG TIỀN";

                        foreach (var ve in data.ToList())
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = ve.Id;
                            worksheet.Cell(currentRow, 2).Value = ve.TenLoaiKhach;
                            worksheet.Cell(currentRow, 3).Value = ve.NgayBan;
                            worksheet.Cell(currentRow, 4).Value = ve.DiaDiem;
                            worksheet.Cell(currentRow, 5).Value = ve.SoLuong;
                            worksheet.Cell(currentRow, 6).Value = ve.TongTien;
                        }

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, 
                                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                                        "thongke_veditich(" 
                                        + (String.IsNullOrEmpty(tungay) ? data.Select(x => x.NgayBan).Min().ToString("dd-MM-yyyy") : DateTime.Parse(tungay).ToString("dd-MM-yyyy"))
                                        + (String.IsNullOrEmpty(denngay) ? "_Den_" + data.Select(x => x.NgayBan).Max().ToString("dd-MM-yyyy") + ")" : "_Den_" + DateTime.Parse(denngay).ToString("dd-MM-yyyy") + ")")
                                        + ".xlsx");
                        }
                    }
                }
                else
                {
                    TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = "Dữ liệu rỗng" });
                    return Redirect("/HueCIT/Ve/Index");
                }
            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string> { IsSuccessed = false, Message = ex.Message });
                return Redirect("/HueCIT/Ve/Index");
            }
        }
        private async Task OptionDiaDiem(int seletedId = 0)
        {
            var list = (await _veDiTichDiaDiemRepository.Gets()).ToList().Select(x => new SelectListItem
            {
                Text = x.DiaDiem.ToString(),
                Value = x.Id.ToString(),
                Selected = x.Id == seletedId ? true : false
            });
            ViewBag.listdiadiem = list;
        }
        private async Task OptionLoaiKhach(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LoaiKhachVeDiTich)).Cast<LoaiKhachVeDiTich>()
                    .Select(x => new SelectListItem()
                    {
                        Text = StringEnum.GetStringValue(x),
                        Value = Convert.ToInt32(x).ToString(),
                        Selected = (int)x == seletedId ? true : false
                    });
                ViewBag.listloaikhach = list.OrderBy(v => v.Value);
            });
        }
    }
}