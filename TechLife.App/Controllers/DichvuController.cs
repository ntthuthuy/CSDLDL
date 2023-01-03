using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model;
using TechLife.Model.Tour;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class DichvuController : BaseController
    {
        readonly private ITourApiClient _tourApiClient;
        readonly private ITourService _tourService;
        private readonly IFileApiClient _fileApiClient;
        public DichvuController(IUserService userService
            , IConfiguration configuration
            , IDiaPhuongApiClient diaPhuongApiClient
            , IDichVuApiClient dichVuApiClient
            , IDonViTinhApiClient donViTinhApiClient
            , ILoaiHinhApiClient loaiHinhApiClient
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
            , IDanhMucApiClient danhMucApiClient
            , ILoaiHinhLaoDongApiClient loaiHinhLaoDongApiClient
            , ITinhChatLaoDongApiClient tinhChatLaoDongApiClient
            , IDuLieuDuLichApiClient csdlDuLichApiClient
            , IDiaPhuongService diaPhuongService
            , IFileUploadService fileUploadService
            , INhaCungCapService nhaCungCapService
            , ITourApiClient tourApiClient
            , ITrackingService trackingService
            , ITourService tourService
            , IFileApiClient fileApiClient)
            : base(userService, diaPhuongApiClient
                  , donViTinhApiClient, loaiHinhApiClient
                  , dichVuApiClient, ngoaiNguApiClient
                  , trinhDoApiClient, boPhanApiClient
                  , loaiPhongApiClient, mucDoThongThaoNgoaiNguApiClient
                  , tienNghiApiClient, huongDanVienApiClient
                  , diemVeSinhApiClient, loaiGiuongApiClient
                  , csdlDuLichApiClient
                  , quocTichApiClient
                  , loaiDichVuApiClient
                  , danhMucApiClient
                  , loaiHinhLaoDongApiClient
                  , tinhChatLaoDongApiClient
                  , diaPhuongService
                  , configuration, fileUploadService, nhaCungCapService, trackingService)
        {
            _tourApiClient = tourApiClient;
            _tourService = tourService;
            _fileApiClient = fileApiClient;
        }
        public async Task<IActionResult> Tour_chitiet(int tourId)
        {
            ViewData["Title"] = "Thông tin tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            var data = await _tourService.GetById(tourId);

            return View(data);
        }
        public async Task<IActionResult> Tour(string id)
        {
            ViewData["Title"] = "Tour du lịch";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            var pageRequest = new TourRequets()
            {
                Keyword = !String.IsNullOrEmpty(Request.Query["search"]) ? Request.Query["search"].ToString() : "",
                PageIndex = !String.IsNullOrEmpty(Request.Query["page"]) ? Convert.ToInt32(Request.Query["page"]) : SystemConstants.pageIndex,
                PageSize = !String.IsNullOrEmpty(Request.Query["page_size"]) ? Convert.ToInt32(Request.Query["page_size"]) : SystemConstants.pageSize,
                luhanh = !String.IsNullOrEmpty(id) ? Convert.ToInt32(HashUtil.DecodeID(id)) : 0
            };

            var data = await _tourService.GetPaging(pageRequest);

            return View(data);
        }
        public async Task<IActionResult> Themtour()
        {
            ViewData["Title"] = "Thêm Tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            int id = !String.IsNullOrEmpty(Request.Query["id"]) ? Convert.ToInt32(HashUtil.DecodeID(Request.Query["id"])) : 0;
            await OptionCongTyLuHanh(id);
            await OptionHinhThucTour();
            await OptionLoaiDiemDuLich();

            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Themtour(TourCreateRequest request, IFormCollection fc)
        {
            ViewData["Title"] = "Thêm Tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            try
            {
                var result = await _tourService.Create(request.Tour);
                if (result.IsSuccessed)
                {
                    if (request.Images != null)
                    {
                        var upload = await _tourService.UploadImage(result.ResultObj.Id, request.Images);
                    }

                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Thêm mới thành công",
                    });

                    if (fc["submit_type"] == "save_add")
                    {
                        return Redirect("/Dichvu/Hanhtrinh/?id=" + result.ResultObj.Id);
                    }
                    else
                    {
                        return Redirect("/Dichvu/Tour");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        public async Task<IActionResult> Suatour(int id)
        {
            ViewData["Title"] = "Sửa Tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            await OptionCongTyLuHanh();
            await OptionHinhThucTour();
            await OptionLoaiDiemDuLich();

            var data = await _tourService.GetById(id);
            var model = new TourUpdateRequest()
            {
                Tour = data,
                Images = new ImageUploadRequest(),
            };
            return View(model);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Suatour(TourUpdateRequest request, IFormCollection fc)
        {
            ViewData["Title"] = "Thêm Tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            try
            {
                var result = await _tourService.Update(request.Tour.Id, request.Tour);
                if (result.IsSuccessed)
                {
                    if (request.Images != null)
                    {
                        foreach (var file in request.Images.Images)
                        {
                            var upload = await _fileApiClient.Upload(file);

                            var fileObj = new FileUploadModel()
                            {
                                FileName = file.FileName,
                                FileUrl = upload,
                                IsImage = true,
                                NgayTao = DateTime.Now,
                                IsStatus = true,
                                Type = LoaiFile.tour.ToString(),
                                Id = request.Tour.Id
                            };
                            await _fileUploadService.CrateFile(fileObj);
                        }

                    }

                    TempData.AddAlert(new Result<string>()
                    {
                        IsSuccessed = result.IsSuccessed,
                        Message = "Cập nhật thành công",
                    });

                    if (fc["submit_type"] == "save_add")
                    {
                        return Redirect("/Dichvu/Hanhtrinh/?id=" + request.Tour.Id);
                    }
                    else
                    {
                        return Redirect("/Dichvu/Tour");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public async Task<IActionResult> Hanhtrinh(int tourId, int id = 0)
        {
            ViewData["Title"] = "Hành trình tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            await OptionDuLieuDuLich();

            if (id != 0)
            {
                ViewData["Title_chird"] = "Cập nhật thông tin hành trình";
                var hanhtrinh = await _tourService.GetItemById(id);
                return View(hanhtrinh);
            }
            else
            {
                ViewData["Title_chird"] = "Thêm mới thông tin hành trình";
                var tour = await _tourService.GetById(tourId);

                var hanhtrinh = new HanhTrinhModel()
                {
                    Ngay = tour.SoNgay,
                    TourId = tourId,
                    Tour = tour,
                    Images = new ImageUploadRequest()
                };
                return View(hanhtrinh);
            }

        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Hanhtrinh(HanhTrinhModel request, string ReturnUrl)
        {
            ViewData["Title"] = "Thêm Tour";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            try
            {
                if (request.Id != 0)
                {
                    var result = await _tourService.UpdateItem(request.Id, request);
                    if (!result.IsSuccessed)
                    {
                        TempData.AddAlert(new Result<string>()
                        {
                            IsSuccessed = result.IsSuccessed,
                            Message = result.Message,
                        });
                        return View(request);
                    }
                }
                else
                {
                    var result = await _tourService.AddItem(request);
                    if (!result.IsSuccessed)
                    {
                        TempData.AddAlert(new Result<string>()
                        {
                            IsSuccessed = result.IsSuccessed,
                            Message = result.Message,
                        });
                        return View(request);
                    }
                }

                if (request.Images != null)
                {
                    foreach (var file in request.Images.Images)
                    {
                        var upload = await _fileApiClient.Upload(file);

                        var fileObj = new FileUploadModel()
                        {
                            FileName = file.FileName,
                            FileUrl = upload,
                            IsImage = true,
                            NgayTao = DateTime.Now,
                            IsStatus = true,
                            Type = LoaiFile.hanhtrinhtour.ToString(),
                            Id = request.Id
                        };
                        await _fileUploadService.CrateFile(fileObj);
                    }

                }

                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = true,
                    Message = "Cập nhật thành công",
                });
                return Redirect("/Dichvu/Hanhtrinh/?tourId=" + request.TourId + "&ReturnUrl=" + ReturnUrl + "");

            }
            catch (Exception ex)
            {
                TempData.AddAlert(new Result<string>()
                {
                    IsSuccessed = false,
                    Message = ex.Message,
                });
                return View(request);
            }

        }
        public async Task<IActionResult> Phong()
        {
            ViewData["Title"] = "Phòng";
            ViewData["Title_parent"] = "Dịch vụ du lịch";

            await OptionCongTyLuHanh();

            return View();
        }


        public async Task<IActionResult> Xoatour(string id)
        {

            var result = await _tourService.Delete(Convert.ToInt32(HashUtil.DecodeID(id)));
            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message
            });
            return Redirect(Request.GetBackUrl());
        }

    }
}
