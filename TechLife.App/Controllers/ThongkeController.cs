using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.ThongKe;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    [Authorize(Roles = "report,root")]
    public class ThongkeController : BaseController
    {
        private readonly IDichVuService dichVuService;
        private readonly IDonViTinhService donViTinhService;
        private readonly ILoaiHinhService loaiHinhService;
        private readonly ILoaiDichVuService loaiDichVuService;
        private readonly IDanhMucService danhMucService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly ITienNghiService _tienNghiService;
        private readonly IHoSoThanhTraService _hoSoThanhTraService;
        private readonly IFileApiClient _fileApiClient;
        public ThongkeController(IUserService userService
            , IConfiguration configuration
            , IDiaPhuongApiClient diaPhuongApiClient
            , IDichVuApiClient dichVuApiClient
            , IDichVuService dichVuService
            , IDonViTinhApiClient donViTinhApiClient
            , IDonViTinhService donViTinhService
            , ILoaiHinhApiClient loaiHinhApiClient
            , ILoaiHinhService loaiHinhService
            , INgoaiNguApiClient ngoaiNguApiClient
            , ITrinhDoApiClient trinhDoApiClient
            , IBoPhanApiClient boPhanApiClient
            , ILoaiPhongApiClient loaiPhongApiClient
            , IMucDoThongThaoNgoaiNguApiClient mucDoThongThaoNgoaiNguApiClient
            , ITienNghiApiClient tienNghiApiClient
            , IHuongDanVienApiClient huongDanVienApiClient
            , IDiemVeSinhApiClient diemVeSinhApiClient
            , ILoaiGiuongApiClient loaiGiuongApiClient
            , IQuocTichApiClient quocTichApiClient
            , ILoaiDichVuApiClient loaiDichVuApiClient
            , ILoaiDichVuService loaiDichVuService
            , IDanhMucApiClient danhMucApiClient
            , IDanhMucService danhMucService
            , ILoaiHinhLaoDongApiClient loaiHinhLaoDongApiClient
            , ITinhChatLaoDongApiClient tinhChatLaoDongApiClient
            , IDuLieuDuLichApiClient csdlDuLichApiClient
            , IDiaPhuongService diaPhuongService
            , IFileUploadService fileUploadService
            , INhaCungCapService nhaCungCapService
            , ITrackingService trackingService
            , IDanhGiaService danhGiaService
            , IDuLieuDuLichService duLieuDuLichService
            , ITienNghiService tienNghiService
            , IHoSoThanhTraService hoSoThanhTraService)
            : base(userService, diaPhuongApiClient
                  , donViTinhApiClient, loaiHinhApiClient
                  , dichVuApiClient, ngoaiNguApiClient
                  , trinhDoApiClient, boPhanApiClient
                  , loaiPhongApiClient, mucDoThongThaoNgoaiNguApiClient
                  , tienNghiApiClient, huongDanVienApiClient
                  , diemVeSinhApiClient, loaiGiuongApiClient
                  , csdlDuLichApiClient, quocTichApiClient
                  , loaiDichVuApiClient, danhMucApiClient
                  , loaiHinhLaoDongApiClient, tinhChatLaoDongApiClient
                  , diaPhuongService
                  , configuration
                  , fileUploadService
                  , nhaCungCapService, trackingService)
        {
            this.dichVuService = dichVuService;
            this.donViTinhService = donViTinhService;
            this.loaiHinhService = loaiHinhService;
            this.loaiDichVuService = loaiDichVuService;
            this.danhMucService = danhMucService;
            _duLieuDuLichService = duLieuDuLichService;
            _tienNghiService = tienNghiService;
            _hoSoThanhTraService = hoSoThanhTraService;
        }

        public async Task<IActionResult> Index(LoaiThongKeRequest request, string search, string type_submit)
        {
            ViewData["Title"] = "Thống kê, báo cáo, tổng hợp số liệu";
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(LoaiBaoCao)).Cast<LoaiBaoCao>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = x.ToString(),
                     Selected = x.ToString() == request.Id ? true : false
                 });

                ViewBag.ListLoaiThongKe = list;
            });

            var luutru = await _duLieuDuLichService.LuuTruTheoLoaiHinh();
            ViewBag.LuuTru = luutru;

            var khachsan = await _duLieuDuLichService.KhachSanTheoHangSao();
            ViewBag.KhachSan = khachsan;

            var nhahang = await _duLieuDuLichService.NhaHangTheoLoaiHinh();
            ViewBag.NhaHang = nhahang;

            var muasam = await _duLieuDuLichService.MuaSamTheoLoaiHinh();
            ViewBag.MuaSam = muasam;

            var hdv = await _duLieuDuLichService.HDVTheoLoaiThe();
            ViewBag.HDV = hdv;

            var tour = await _duLieuDuLichService.TourTheoLoaiHinh();
            ViewBag.Tour = tour;

            var diemdulich = await _duLieuDuLichService.DiemDuLichTheoLoaiHinh();
            ViewBag.DiemDuLich = diemdulich;

            var luhanh = await _duLieuDuLichService.LuHanhTheoLoaiHinh();
            ViewBag.LuHanh = luhanh;

            //var huongdanvien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
            //ViewBag.HuongDanVien = huongdanvien;

            if (request.Id == LoaiBaoCao.bieudocosoluutru.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Cơ sở lưu trú";

                var loaihinh = await _duLieuDuLichService.LuuTruTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = loaihinh;

                var sao = await _duLieuDuLichService.KhachSanTheoHangSao();
                ViewBag.SaoRpt = sao;

                var diaban = await _duLieuDuLichService.LuuTruTheoDiaBan();
                ViewBag.DiaBanRpt = diaban;

                return View("Cosoluutru");
            }
            else if (request.Id == LoaiBaoCao.bieudonhahang.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Nhà hàng";

                var data = await _duLieuDuLichService.NhaHangTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = data;

                var diaban = await _duLieuDuLichService.NhaHangTheoDiaBan();
                ViewBag.DiaBanRpt = diaban;

                return View("Nhahang");
            }
            else if (request.Id == LoaiBaoCao.bieudocosomuasam.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Cơ sở mua sắm";

                var data = await _duLieuDuLichService.MuaSamTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = data;

                var diaban = await _duLieuDuLichService.MuaSamTheoDiaBan();
                ViewBag.DiaBanRpt = diaban;

                return View("Cosomuasam");
            }
            else if (request.Id == LoaiBaoCao.bieudohuongdanvien.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Hướng dẫn viên";

                var data = await _duLieuDuLichService.HDVTheoLoaiThe();
                ViewBag.LoaiHinhRpt = data;

                var diaban = await _duLieuDuLichService.HDVTheoNgonNgu();
                ViewBag.NgonNguRpt = diaban;


                return View("Huongdanvien");
            }
            else if (request.Id == LoaiBaoCao.bieudocongtyluhanh.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Công ty lữ hành";

                var data = await _duLieuDuLichService.LuHanhTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = data;

                return View("Luhanh");
            }
            else if (request.Id == LoaiBaoCao.bieudotourdulich.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Tour du lịch";

                var data = await _duLieuDuLichService.TourTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = data;

                return View("Tour");
            }
            else if (request.Id == LoaiBaoCao.bieudodiemdulich.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Điểm du lịch";

                var data = await _duLieuDuLichService.DiemDuLichTheoLoaiHinh();
                ViewBag.LoaiHinhRpt = data;

                return View("Diemdulich");
            }
            else if (request.Id == LoaiBaoCao.thongkehuongdanvien.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Hướng dẫn viên";
                var request_hdv = new DuLieuDuLichSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiTheId = !String.IsNullOrEmpty(Request.Query["LoaiTheId"]) ? Convert.ToInt32(Request.Query["LoaiTheId"]) : 0,
                    NgonNguId = !String.IsNullOrEmpty(Request.Query["NgonNguId"]) ? Convert.ToInt32(Request.Query["NgonNguId"]) : 0,
                    TinhTrang = !String.IsNullOrEmpty(Request.Query["TinhTrang"]) ? Request.Query["TinhTrang"].ToString() : ""
                };

                await OptionLoaiTheHDV();

                //ViewBag.HuongDanVien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
                var data = await _duLieuDuLichService.TimKiemDuLieuHDV(request_hdv);
                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"HuongDanVien.xlsx";


                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Số điện thoại";
                        worksheet.Cell(1, 4).Value = "Loại thẻ";
                        worksheet.Cell(1, 5).Value = "Loại ngôn ngữ";
                        worksheet.Cell(1, 6).Value = "Ngày cấp thẻ";
                        worksheet.Cell(1, 7).Value = "Ngày hết hạn";
                        worksheet.Cell(1, 8).Value = "Địa chỉ";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 30;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 8; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;
                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 8; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].HoVaTen;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].LoaiThe;
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].NgonNgu;
                            worksheet.Cell(index + 1, 6).Value = data.Items[index - 1].NgayCapThe.Date.ToString("dd/MM/yyyy");
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].NgayHetHan.Date.ToString("dd/MM/yyyy");
                            worksheet.Cell(index + 1, 8).Value = data.Items[index - 1].DiaChi;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuHDV", data);
                }

            }
            else if (request.Id == LoaiBaoCao.thongkecosoluutru.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Cơ sở lưu trú";
                var request_cslt = new DuLieuDuLichCSLTSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiHinhId = !String.IsNullOrEmpty(Request.Query["LoaiHinhId"]) ? Convert.ToInt32(Request.Query["LoaiHinhId"]) : 0,
                    HangSao = !String.IsNullOrEmpty(Request.Query["HangSao"]) ? Convert.ToInt32(Request.Query["HangSao"]) : 0,
                };
                var data = await _duLieuDuLichService.TimKiemDuLieuCSLT(request_cslt);
                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"CoSoLuuTru.xlsx";

                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Số điện thoại";
                        worksheet.Cell(1, 4).Value = "Hạng Sao";
                        worksheet.Cell(1, 5).Value = "Diện tích";
                        worksheet.Cell(1, 6).Value = "Họ tên người đại diện";
                        worksheet.Cell(1, 7).Value = "Số điện thoại người đại diện";
                        worksheet.Cell(1, 8).Value = "Ngày công nhận hạng sao";
                        worksheet.Cell(1, 9).Value = "Ngày hết hạn sao";
                        worksheet.Cell(1, 10).Value = "Địa chỉ";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 30;
                        worksheet.Column(9).Width = 30;
                        worksheet.Column(10).Width = 30;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 10; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 10; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].Ten;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].HangSao;
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].DienTichMatBang;
                            worksheet.Cell(index + 1, 6).Value = data.Items[index - 1].HoTenNguoiDaiDien;
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].SoDienThoaiNguoiDaiDien;
                            worksheet.Cell(index + 1, 8).Value = Functions.GetDatetimeToVn(data.Items[index - 1].NgayQuyetDinh);
                            worksheet.Cell(index + 1, 9).Value = Functions.GetDatetimeToVn(data.Items[index - 1].NgayHetHan);
                            worksheet.Cell(index + 1, 10).Value = data.Items[index - 1].DuongPho;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuCSLT", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongkenhahang.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Nhà hàng";
                var request_cslt = new DuLieuDuLichNhaHangSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiId = !String.IsNullOrEmpty(Request.Query["LoaiId"]) ? Convert.ToInt32(Request.Query["LoaiId"]) : 0,

                };
                var data = await _duLieuDuLichService.TimKiemDuLieuNhaHang(request_cslt);

                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"NhaHang.xlsx";
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Số điện thoại";
                        worksheet.Cell(1, 4).Value = "Thời điểm bắt đầu kinh doanh";
                        worksheet.Cell(1, 5).Value = "Diện tích";
                        worksheet.Cell(1, 6).Value = "Ngày đạt chuẩn";
                        worksheet.Cell(1, 7).Value = "Dịa chỉ";

                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;

                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 7; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 7; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].Ten;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 4).Value = Functions.GetDatetimeToVn(data.Items[index - 1].ThoiDiemBatDauKinhDoanh);
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].DienTichMatBang;
                            worksheet.Cell(index + 1, 6).Value = Functions.GetDatetimeToVn(data.Items[index - 1].NgayCVDatChuan);
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].DuongPho;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuNhaHang", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongkedulieuthanhtra.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Dữ liệu thanh tra";
                var pageRequest = new ThanhTraPagingRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                    PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                    PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                    HoSoId = !String.IsNullOrEmpty(Request.Query["hoso"]) ? Convert.ToInt32(Request.Query["hoso"]) : -1,
                    KetLuanId = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1,
                };
                var data = await _hoSoThanhTraService.GetPaging(pageRequest);

                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"ThanhTra.xlsx";
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Thời gian";
                        worksheet.Cell(1, 3).Value = "Tên";
                        worksheet.Cell(1, 4).Value = "Số điện thoại";
                        worksheet.Cell(1, 5).Value = "Địa chỉ";
                        worksheet.Cell(1, 6).Value = "Nội dung";
                        worksheet.Cell(1, 7).Value = "Kết quả";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 15;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 7; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 6; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = Functions.GetDatetimeToVn(data.Items[index - 1].ThoiGian);
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].HoSo.Ten;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].HoSo.SoDienThoai;
                            worksheet.Cell(index + 1, 5).Value = (Functions.GetFullDiaPhuong(data.Items[index - 1].HoSo.SoNha, data.Items[index - 1].HoSo.DuongPho, data.Items[index - 1].HoSo.PhuongXa, data.Items[index - 1].HoSo.QuanHuyen));
                            worksheet.Cell(index + 1, 6).Value = data.Items[index - 1].NoiDung;
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].KetQuaThanhTra;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuThanhTra", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongkechitietluhanh.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Dữ liệu lữ hành";
                var request_cslt = new DuLieuDuLichLuHanhSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiHinhId = !String.IsNullOrEmpty(Request.Query["LoaiHinhId"]) ? Convert.ToInt32(Request.Query["LoaiHinhId"]) : 0

                };
                var data = await _duLieuDuLichService.TimKiemDuLieuLuHanh(request_cslt);

                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"LuHanh.xlsx";
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Số điện thoại";
                        worksheet.Cell(1, 4).Value = "Loại công ty";
                        worksheet.Cell(1, 5).Value = "Số lao động";
                        worksheet.Cell(1, 6).Value = "Diện tích";
                        worksheet.Cell(1, 7).Value = "Ngày hoạt động";
                        worksheet.Cell(1, 8).Value = "Địa chỉ";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 30;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 8; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 8; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].Ten;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].TenDanhMuc;
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].SoLuongLaoDong;
                            worksheet.Cell(index + 1, 6).Value = data.Items[index - 1].DienTichMatbang;
                            worksheet.Cell(index + 1, 7).Value = Functions.GetDatetimeToVn(data.Items[index - 1].ThoiDiembatDauKinhDoanh);
                            worksheet.Cell(index + 1, 8).Value = data.Items[index - 1].DuongPho;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuLuHanh", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongkediemdulich.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Dữ liệu điểm du lịch";
                var request_cslt = new DuLieuDuLichDiemDuLichSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiHinhId = !String.IsNullOrEmpty(Request.Query["LoaiHinhId"]) ? Convert.ToInt32(Request.Query["LoaiHinhId"]) : 0

                };
                var data = await _duLieuDuLichService.TimKiemDuLieuDiemDuLich(request_cslt);

                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"DiemDuLich.xlsx";
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Loại hình";
                        worksheet.Cell(1, 4).Value = "Số điện thoại";
                        worksheet.Cell(1, 5).Value = "Số giấp phép";
                        worksheet.Cell(1, 6).Value = "Thời điểm bắt đầu kinh doanh";
                        worksheet.Cell(1, 7).Value = "Diện tích";
                        worksheet.Cell(1, 8).Value = "Địa chỉ";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 30;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 8; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 8; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].Ten;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].TenDanhMuc;
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].SoGiayPhep;
                            worksheet.Cell(index + 1, 6).Value = Functions.GetDatetimeToVn(data.Items[index - 1].ThoiDiemBatDauKinhDoanh);
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].DienTichMatBang;
                            worksheet.Cell(index + 1, 8).Value = data.Items[index - 1].DiaChi;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuDiemDuLich", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongkecosomuasam.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Dữ liệu cơ sở mua sắm";
                var request_cslt = new DuLieuDuLichCoSoMuaSamSearchRequest()
                {
                    Keyword = !String.IsNullOrEmpty(Request.Query["Keyword"]) ? Request.Query["Keyword"].ToString() : "",
                    LoaiHinhId = !String.IsNullOrEmpty(Request.Query["LoaiHinhId"]) ? Convert.ToInt32(Request.Query["LoaiHinhId"]) : 0

                };
                var data = await _duLieuDuLichService.TimKiemDuLieuCoSoMuaSam(request_cslt);

                if (type_submit == "download")
                {
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = $"CoSoMuaSam.xlsx";
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("ThongKe");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Cell(1, 2).Value = "Tên";
                        worksheet.Cell(1, 3).Value = "Loại hình kinh doanh";
                        worksheet.Cell(1, 4).Value = "Số điện thoại";
                        worksheet.Cell(1, 5).Value = "Số giấp phép";
                        worksheet.Cell(1, 6).Value = "Thời điểm bắt đầu kinh doanh";
                        worksheet.Cell(1, 7).Value = "Diện tích";
                        worksheet.Cell(1, 8).Value = "Địa chỉ";
                        worksheet.Column(1).Width = 5;
                        worksheet.Column(2).Width = 15;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 30;
                        worksheet.Column(5).Width = 30;
                        worksheet.Column(6).Width = 15;
                        worksheet.Column(7).Width = 30;
                        worksheet.Column(8).Width = 30;
                        worksheet.Row(1).Height = 35;

                        for (int index = 1; index <= 8; index++)
                        {
                            worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(1, index).Style.Font.Bold = true;
                            worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                        }

                        for (int index = 1; index <= data.Items.Count(); index++)
                        {
                            for (int idx = 1; idx <= 8; idx++)
                            {
                                worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            }

                            worksheet.Row(index + 1).Height = 30;
                            worksheet.Cell(index + 1, 1).Value = index;
                            worksheet.Cell(index + 1, 2).Value = data.Items[index - 1].Ten;
                            worksheet.Cell(index + 1, 3).Value = data.Items[index - 1].TenLoaiHinhKinhDoanh;
                            worksheet.Cell(index + 1, 4).Value = data.Items[index - 1].SoDienThoai;
                            worksheet.Cell(index + 1, 5).Value = data.Items[index - 1].SoGiayPhep;
                            worksheet.Cell(index + 1, 6).Value = Functions.GetDatetimeToVn(data.Items[index - 1].ThoiDiemBatDauKinhDoanh);
                            worksheet.Cell(index + 1, 7).Value = data.Items[index - 1].DienTichMatBang;
                            worksheet.Cell(index + 1, 8).Value = data.Items[index - 1].DiaChi;

                        }
                        using (var stream = new MemoryStream())
                        {

                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }

                    }
                }
                else
                {
                    return View("TimKiemDuLieuCoSoMuaSam", data);
                }
            }
            else if (request.Id == LoaiBaoCao.thongketuychon.ToString())
            {
                ViewData["Title_parent"] = "Thống kê, báo cáo, tổng hợp số liệu";
                ViewData["Title"] = "Tùy chọn báo cáo";

                await OptionLinhVucKinhDoanh();
                await OptionTieuChuanCoSo();
                await OptionLoaiTheHDV();

                await OptionLoaiNhaHang();
                await OptionLoaiCoSoMuaSam();
                await OptionLoaiDiemDuLich();
                await OptionLoaiKhuDuLich();
                await OptionLoaiKhuVuiChoi();
                await OptionLoaiCTLuHanh();
                await OptionLoaiHinhKinhDoanh();

                ViewBag.TienNghi = await _tienNghiService.GetAll(0);

                return View("Tuychon");
            }
            return View();
        }
        public async Task<IActionResult> Cosoluutru()
        {
            ViewData["Title"] = "Cơ sở lưu trú";
            ViewData["Title_parent"] = "Thống kê";

            var loaihinh = await _duLieuDuLichService.LuuTruTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = loaihinh;

            var sao = await _duLieuDuLichService.KhachSanTheoHangSao();
            ViewBag.SaoRpt = sao;

            var diaban = await _duLieuDuLichService.LuuTruTheoDiaBan();
            ViewBag.DiaBanRpt = diaban;
            return View();
        }

        public async Task<IActionResult> Nhahang()
        {
            ViewData["Title"] = "Cơ sở nhà hàng";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.NhaHangTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = data;

            var diaban = await _duLieuDuLichService.NhaHangTheoDiaBan();
            ViewBag.DiaBanRpt = diaban;

            return View();
        }

        public async Task<IActionResult> Cosomuasam()
        {
            ViewData["Title"] = "Cơ sở mua sắm";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.MuaSamTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = data;

            var diaban = await _duLieuDuLichService.MuaSamTheoDiaBan();
            ViewBag.DiaBanRpt = diaban;

            return View();
        }

        public async Task<IActionResult> Luhanh()
        {
            ViewData["Title"] = "Công ty lữ hành";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.LuHanhTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = data;

            return View();
        }

        public async Task<IActionResult> Tour()
        {
            ViewData["Title"] = "Tour du lịch";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.TourTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = data;

            return View();
        }
        public async Task<IActionResult> Diemdulich()
        {
            ViewData["Title"] = "Điểm du lịch";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.DiemDuLichTheoLoaiHinh();
            ViewBag.LoaiHinhRpt = data;

            return View();
        }

        public async Task<IActionResult> Huongdanvien()
        {
            ViewData["Title"] = "Hướng dẫn viên";
            ViewData["Title_parent"] = "Thống kê";

            var data = await _duLieuDuLichService.HDVTheoLoaiThe();
            ViewBag.LoaiHinhRpt = data;

            var diaban = await _duLieuDuLichService.HDVTheoNgonNgu();
            ViewBag.NgonNguRpt = diaban;

            return View();
        }

        public async Task<IActionResult> Tuychon()
        {
            ViewData["Title"] = "Tùy chọn";
            ViewData["Title_parent"] = "Thống kê";

            await OptionLinhVucKinhDoanh();
            await OptionTieuChuanCoSo();
            await OptionLoaiTheHDV();

            await this.OptionLoaiHinhKinhDoanh();
            await this.OptionLoaiNhaHang();
            await this.OptionLoaiCoSoMuaSam();
            await this.OptionLoaiDiemDuLich();
            await this.OptionLoaiKhuDuLich();
            await this.OptionLoaiKhuVuiChoi();
            await this.OptionLoaiCTLuHanh();

            ViewBag.TienNghi = await _tienNghiService.GetAll(0);

            return View();
        }
        private async Task OptionLoaiHinhKinhDoanh(int seletedId = 0)
        {
            var loaihinhkinhdoanh = await loaiHinhService.GetAll();

            var list = loaihinhkinhdoanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiHinhKD = list;
        }
        private async Task OptionLoaiNhaHang(int seletedId = 0)
        {
            var luhanh = await dichVuService.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenDichVu.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiNhaHang = list;
        }
        private async Task OptionLoaiCoSoMuaSam(int seletedId = 0)
        {
            var luhanh = await loaiDichVuService.GetAll();

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.TenLoai.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDichVu = list;
        }
        private async Task OptionLoaiDiemDuLich(int seletedId = 0)
        {
            var luhanh = await danhMucService.GetAll((int)LinhVucKinhDoanh.DiemDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiDDL = list;
        }
        private async Task OptionLoaiKhuDuLich(int seletedId = 0)
        {
            var luhanh = await danhMucService.GetAll((int)LinhVucKinhDoanh.KhuDuLich);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuDL = list;
        }
        private async Task OptionLoaiKhuVuiChoi(int seletedId = 0)
        {
            var luhanh = await danhMucService.GetAll((int)LinhVucKinhDoanh.KhuVuiChoi);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiKhuVuiChoi = list;
        }
        private async Task OptionLoaiCTLuHanh(int seletedId = 0)
        {
            var luhanh = await danhMucService.GetAll((int)LinhVucKinhDoanh.LuHanh);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listLoaiCTLH = list;
        }

        [HttpPost]
        public async Task<IActionResult> Tuychon_ketqua(int linhvuc, string type_submit
            , int[] loaihinh, int[] hangsao, int[] tiennghi, string[] thongtin)
        {
            ViewData["Title"] = "Kết quả thống kê";
            ViewData["Title_parent"] = "Thống kê";

            var request = new RptFromRequets()
            {
                Keyword = "",
                hangsao = string.Join(',', hangsao),
                loaihinh = string.Join(',', loaihinh),
                tiennghi = string.Join(',', tiennghi),
            };
            request.PageSize = 10000;
            var data = await _duLieuDuLichService.DuLieuDuLich(Request.GetLanguageId(), linhvuc, request);
            #region Tải file
            if (type_submit == "download")
            {

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = $"DuLieuDuLich.xlsx";

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("ThongKe");

                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range(1, 1, 1, 1 + thongtin.Length).Style.Font.Bold = true;
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(1, 1, 1 + data.TotalRecords, 1 + thongtin.Length).Style.Alignment.WrapText = true;
                    int col = 1;
                    worksheet.Cell(1, col).Value = "STT";
                    if (thongtin != null && thongtin.Contains("ten"))
                    {
                        worksheet.Cell(1, ++col).Value = "Tên";
                        worksheet.Column(col).Width = 30;
                    }
                    if (thongtin != null && thongtin.Contains("loaihinh"))
                    {
                        worksheet.Cell(1, ++col).Value = "Loại hình";
                        worksheet.Column(col).Width = 15;
                    }
                    if (thongtin != null && thongtin.Contains("hangsao"))
                    {
                        worksheet.Cell(1, ++col).Value = "Hạng sao";
                        worksheet.Column(col).Width = 15;
                    }
                    if (thongtin != null && thongtin.Contains("diachi"))
                    {
                        worksheet.Cell(1, ++col).Value = "Địa chỉ";
                        worksheet.Column(col).Width = 40;
                    }
                    if (thongtin != null && thongtin.Contains("sodienthoai"))
                    {
                        worksheet.Cell(1, ++col).Value = "Số điện thoại";
                        worksheet.Column(col).Width = 15;
                    }
                    if (thongtin != null && thongtin.Contains("email"))
                    {
                        worksheet.Cell(1, ++col).Value = "Email";
                        worksheet.Column(col).Width = 15;
                    }
                    if (thongtin != null && thongtin.Contains("nguoidaidien"))
                    {
                        worksheet.Cell(1, ++col).Value = "Người đại diện";
                        worksheet.Column(col).Width = 15;
                    }
                    if (thongtin != null && thongtin.Contains("giayphep"))
                    {
                        worksheet.Cell(1, ++col).Value = "Giấy phép kinh doanh";
                        worksheet.Column(col).Width = 15;
                    }
                    worksheet.Row(1).Height = 35;

                    for (int index = 1; index <= data.Items.Count; index++)
                    {
                        col = 1;
                        worksheet.Cell(index + 1, col).Value = index;
                        if (thongtin != null && thongtin.Contains("ten"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].Ten;
                        }
                        if (thongtin != null && thongtin.Contains("loaihinh"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].LoaiHinh;
                        }
                        if (thongtin != null && thongtin.Contains("hangsao"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].HangSao;
                        }
                        if (thongtin != null && thongtin.Contains("diachi"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].DiaChi;
                        }
                        if (thongtin != null && thongtin.Contains("sodienthoai"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].SoDienThoai;
                        }
                        if (thongtin != null && thongtin.Contains("email"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].Email;
                        }
                        if (thongtin != null && thongtin.Contains("nguoidaidien"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].NguoiDaiDien;
                        }
                        if (thongtin != null && thongtin.Contains("giayphep"))
                        {
                            worksheet.Cell(index + 1, ++col).Value = data.Items[index - 1].SoGiayPhep;
                        }
                    }
                    using (var stream = new MemoryStream())
                    {

                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            #endregion
            ViewBag.ThongTin = thongtin;

            return View(data);
        }
        public IActionResult Dulieudulich()
        {
            ViewData["Title"] = "Dữ liệu du lịch theo địa bàn";
            ViewData["Title_parent"] = "Thống kê";
            return View();
        }
        public async Task<IActionResult> TimKiemDuLieu(GetPagingRequest request, string search = "")
        {
            ViewData["Title"] = "Tìm kiếm dữ liệu";
            ViewData["Title_parent"] = "Thống kê";
            await OptionLinhVucKinhDoanh();
            await OptionLoaiHinhKinhDoanh();
            await OptionTieuChuanCoSo();
            await OptionLoaiNhaHang();
            await OptionLoaiCoSoMuaSam();
            await OptionLoaiDiemDuLich();
            await OptionLoaiKhuDuLich();
            await OptionLoaiKhuVuiChoi();
            await OptionLoaiTheHDV();
            await OptionLoaiCTLuHanh();

            ViewBag.TienNghi = await _tienNghiService.GetAll(0);
            request.PageSize = 1000;
            request.Keyword = search;
            var data = await _duLieuDuLichService.TimKiemDuLieu(request);

            return View(data);
        }
        public async Task<IActionResult> TimKiemDuLieuHDV(DuLieuDuLichSearchRequest request, string search = "")
        {
            ViewData["Title"] = "Tìm kiếm dữ liệu";
            ViewData["Title_parent"] = "Thống kê";
            await OptionLoaiTheHDV();
            request.PageSize = 100;
            request.Keyword = search;
            //ViewBag.HuongDanVien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
            var data = await _duLieuDuLichService.TimKiemDuLieuHDV(request);

            return View(data);
        }
        public async Task<IActionResult> TimKiemDuLieuCSLT(DuLieuDuLichCSLTSearchRequest request, string search = "")
        {
            ViewData["Title"] = "Tìm kiếm dữ liệu";
            ViewData["Title_parent"] = "Thống kê";
            await OptionCoSoLuuTru();
            //request.PageSize = 100;
            request.Keyword = search;
            //ViewBag.HuongDanVien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
            var data = await _duLieuDuLichService.TimKiemDuLieuCSLT(request);

            return View(data);
        }
        public async Task<IActionResult> TimKiemDuLieuNhaHang(DuLieuDuLichNhaHangSearchRequest request, string search = "")
        {
            ViewData["Title"] = "Tìm kiếm dữ liệu";
            ViewData["Title_parent"] = "Thống kê";

            //request.PageSize = 100;
            request.Keyword = search;
            //ViewBag.HuongDanVien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
            var data = await _duLieuDuLichService.TimKiemDuLieuNhaHang(request);

            return View(data);
        }
        public async Task<IActionResult> TimKiemDuLieuThanhTra()
        {
            ViewData["Title"] = "Danh sách hồ sơ các cơ sở đã thanh tra, kiểm tra";
            ViewData["Title_parent"] = "Thanh tra";

            var pageRequest = new ThanhTraPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                HoSoId = !String.IsNullOrEmpty(Request.Query["hoso"]) ? Convert.ToInt32(Request.Query["hoso"]) : -1,
                KetLuanId = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1,
            };

            await OptionHoSoCoSo(pageRequest.HoSoId);
            await OptionKetQuaThanhTra(pageRequest.KetLuanId);

            var data = await _hoSoThanhTraService.GetPaging(pageRequest);

            return View(data);
        }
        public async Task<IActionResult> TimKiemDuLieuLuHanh(DuLieuDuLichLuHanhSearchRequest request, string search = "")
        {
            ViewData["Title"] = "Tìm kiếm dữ liệu";
            ViewData["Title_parent"] = "Thống kê";

            //request.PageSize = 100;
            request.Keyword = search;
            //ViewBag.HuongDanVien = await _duLieuDuLichService.TimKiemDuLieuHDV(search);
            var data = await _duLieuDuLichService.TimKiemDuLieuLuHanh(request);

            return View(data);
        }
        public async Task<IActionResult> DownloadExcelDocument(string search)
        {
            try
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = $"DuLieuDuLich.xlsx";

                GetPagingRequest request = new GetPagingRequest()
                {
                    Keyword = search,
                    PageSize = 10000000
                };
                var dulich = await _duLieuDuLichService.TimKiemDuLieu(request);

                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("ThongKe");
                    worksheet.Cell(1, 1).Value = "STT";
                    worksheet.Cell(1, 2).Value = "Tên";
                    worksheet.Cell(1, 3).Value = "Loại";
                    worksheet.Cell(1, 4).Value = "Số điện thoại";
                    worksheet.Cell(1, 5).Value = "Số quyết định";
                    worksheet.Cell(1, 6).Value = "Số giấy phép";
                    worksheet.Cell(1, 7).Value = "Hạng sao";
                    worksheet.Cell(1, 8).Value = "Email";
                    worksheet.Cell(1, 9).Value = "Họ tên người đại diện";
                    worksheet.Cell(1, 10).Value = "Chức vụ";
                    worksheet.Cell(1, 11).Value = "Địa chỉ";
                    worksheet.Column(1).Width = 5;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 30;
                    worksheet.Column(5).Width = 30;
                    worksheet.Column(6).Width = 15;
                    worksheet.Column(7).Width = 30;
                    worksheet.Column(8).Width = 30;
                    worksheet.Column(9).Width = 30;
                    worksheet.Column(10).Width = 30;
                    worksheet.Column(11).Width = 30;
                    worksheet.Row(1).Height = 35;

                    for (int index = 1; index <= 11; index++)
                    {
                        worksheet.Cell(1, index).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        worksheet.Cell(1, index).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(1, index).Style.Font.Bold = true;
                        worksheet.Cell(1, index).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Cell(1, index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(1, index).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet.Cell(1, index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(1, index).Style.Border.TopBorderColor = XLColor.Black;
                        worksheet.Cell(1, index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(1, index).Style.Border.LeftBorderColor = XLColor.Black;
                        worksheet.Cell(1, index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(1, index).Style.Border.RightBorderColor = XLColor.Black;
                        worksheet.Cell(1, index).Style.Alignment.WrapText = true;

                    }

                    for (int index = 1; index <= dulich.Items.Count(); index++)
                    {
                        for (int idx = 1; idx <= 11; idx++)
                        {
                            worksheet.Cell(index + 1, idx).Style.Alignment.WrapText = true;
                            worksheet.Cell(index + 1, idx).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(index + 1, idx).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(index + 1, idx).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(index + 1, idx).Style.Border.LeftBorderColor = XLColor.Black;
                            worksheet.Cell(index + 1, idx).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(index + 1, idx).Style.Border.RightBorderColor = XLColor.Black;
                            worksheet.Cell(index + 1, idx).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            worksheet.Cell(index + 1, idx).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        }

                        worksheet.Row(index + 1).Height = 30;
                        worksheet.Cell(index + 1, 1).Value = index;
                        worksheet.Cell(index + 1, 2).Value = dulich.Items[index - 1].Ten;
                        worksheet.Cell(index + 1, 3).Value = dulich.Items[index - 1].LoaiHinhName;
                        worksheet.Cell(index + 1, 4).Value = dulich.Items[index - 1].SoDienThoai;
                        worksheet.Cell(index + 1, 5).Value = dulich.Items[index - 1].SoQuyetDinh;
                        worksheet.Cell(index + 1, 6).Value = dulich.Items[index - 1].SoGiayPhep;
                        worksheet.Cell(index + 1, 7).Value = dulich.Items[index - 1].HangSao;
                        worksheet.Cell(index + 1, 8).Value = dulich.Items[index - 1].Email;
                        worksheet.Cell(index + 1, 9).Value = dulich.Items[index - 1].HoTenNguoiDaiDien;
                        worksheet.Cell(index + 1, 10).Value = dulich.Items[index - 1].ChucVuNguoiDaiDien;
                        worksheet.Cell(index + 1, 11).Value = dulich.Items[index - 1].DiaChi;
                    }
                    using (var stream = new MemoryStream())
                    {

                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Search(DuLieuDuLichSearchRequest request)
        {

            //var loaithe = await _huongDanVienApiClient.GetAll();
            var ngonngu = await _ngoaiNguApiClient.GetAll();
            request.NgonNguItems = ngonngu.Select(v => new SelectListItem()
            {
                Text = v.TenNgoaiNgu,
                Value = v.Id.ToString()
            }).ToList();
            return PartialView(request);
        }
        [HttpGet]
        public async Task<IActionResult> SearchCSLT(DuLieuDuLichCSLTSearchRequest request)
        {

            var loaihinh = await loaiHinhService.GetAll();
            request.LoaiHinhItems = loaihinh.Select(v => new SelectListItem()
            {
                Text = v.TenLoai,
                Value = v.Id.ToString()
            }).ToList();
            return PartialView(request);
        }
        [HttpGet]
        public async Task<IActionResult> SearchNhaHang(DuLieuDuLichNhaHangSearchRequest request)
        {

            try
            {
                ViewData["Title"] = "Danh sách nhà hàng";
                ViewData["Title_parent"] = "Nhà hàng";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["LoaiHinh"]) ? Convert.ToInt32(Request.Query["LoaiHinh"]) : 0;

                await OptionLoaiNhaHang(loaihinh);

                return PartialView(request);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchThanhTra(DuLieuDuLichThanhTraSearchRequest request)
        {

            ViewData["Title"] = "Danh sách hồ sơ các cơ sở đã thanh tra, kiểm tra";
            ViewData["Title_parent"] = "Thanh tra";
            var pageRequest = new ThanhTraPagingRequest()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                HoSoId = !String.IsNullOrEmpty(Request.Query["hoso"]) ? Convert.ToInt32(Request.Query["hoso"]) : -1,
                KetLuanId = !String.IsNullOrEmpty(Request.Query["loaihinh"]) ? Convert.ToInt32(Request.Query["loaihinh"]) : -1,
            };
            await OptionHoSoCoSo(pageRequest.HoSoId);
            await OptionKetQuaThanhTra(pageRequest.KetLuanId);
            var data = await _hoSoThanhTraService.GetPaging(pageRequest);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> SearchLuhanh(DuLieuDuLichLuHanhSearchRequest request)
        {
            try
            {
                ViewData["Title"] = "Danh sách điểm du lịch";
                ViewData["Title_parent"] = "Điểm du lịch";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["LoaiHinh"]) ? Convert.ToInt32(Request.Query["LoaiHinh"]) : 0;

                await OptionLoaiCTLuHanh(loaihinh);

                return PartialView(request);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchDiemDuLich(DuLieuDuLichDiemDuLichSearchRequest request)
        {
            try
            {
                ViewData["Title"] = "Danh sách điểm du lịch";
                ViewData["Title_parent"] = "Điểm du lịch";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["LoaiHinh"]) ? Convert.ToInt32(Request.Query["LoaiHinh"]) : 0;

                await OptionLoaiDiemDuLich(loaihinh);

                return PartialView(request);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchCoSoMuaSam(DuLieuDuLichCoSoMuaSamSearchRequest request)
        {
            try
            {
                ViewData["Title"] = "Danh sách cơ sở mua sắm";
                ViewData["Title_parent"] = "Cơ sở mua sắm";

                int loaihinh = !String.IsNullOrEmpty(Request.Query["LoaiHinhId"]) ? Convert.ToInt32(Request.Query["LoaiHinhId"]) : 0;
                request.LoaiHinhId = loaihinh;
                request.Keyword = Request.Query["Keyword"];
                await OptionLoaiCoSoMuaSam(loaihinh);

                return PartialView(request);
            }
            catch (Exception ex)
            {
                return View(pageError404);
            }
        }
        async Task OptionHoSoCoSo(int seletedId = 0)
        {
            var luhanh = await _duLieuDuLichService.GetAll(0);

            var list = luhanh.Select(x => new SelectListItem
            {
                Text = x.Ten.ToString(),
                Value = x.Id.ToString(),
                Selected = (int)x.Id == seletedId ? true : false
            });

            ViewBag.listHoSo = list;
        }
        async Task OptionKetQuaThanhTra(int seletedId = 0)
        {
            await Task.Run(() =>
            {
                var list = Enum.GetValues(typeof(KetQuaThanhTra)).Cast<KetQuaThanhTra>()
                 .Select(x => new SelectListItem
                 {
                     Text = StringEnum.GetStringValue(x),
                     Value = Convert.ToInt32(x).ToString(),
                     Selected = (int)x == seletedId ? true : false
                 });

                ViewBag.listKetQua = list;
            });
        }
    }
}


