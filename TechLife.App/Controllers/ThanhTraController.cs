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
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Model.HoSoThanhTra;
using TechLife.Service;

namespace TechLife.App.Controllers
{
   
    public class ThanhTraController : BaseController
    {
        private readonly IHoSoThanhTraService _hoSoThanhTraService;
        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IFileApiClient _fileApiClient;
        public ThanhTraController(IUserService userService
          , IConfiguration configuration
          , IHoSoThanhTraService hoSoThanhTraService
          , IDuLieuDuLichService duLieuDuLichService
          , IFileApiClient fileApiClient
          , ITrackingService trackingService)
          : base(userService, configuration, trackingService)
        {
            _hoSoThanhTraService = hoSoThanhTraService;
            _duLieuDuLichService = duLieuDuLichService;
            _fileApiClient = fileApiClient;
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
        [Authorize(Roles = "create_thanhtra,root")]
        public async Task<IActionResult> Themmoi()
        {
            ViewData["Title"] = "Thêm hồ sơ đã thanh tra, kiểm tra";
            ViewData["Title_parent"] = "Thanh tra";

            await OptionHoSoCoSo();
            await OptionKetQuaThanhTra();

            var vb = new VanBanHoSoThanhTraCreateRequest()
            {
                FileName = "",
                FilePath = "",
                NgayKy = DateTime.Now,
                SoHieu = "",
                TenVanBan = "",
            };

            var lst = new List<VanBanHoSoThanhTraCreateRequest>();
            lst.Add(vb);

            var model = new HoSoThanhTraCreateRequest()
            {
                DSVanBan = lst
            };

            return View(model);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "create_thanhtra,root")]
        public async Task<IActionResult> Themmoi(HoSoThanhTraCreateRequest request, string type_sumit)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Thêm hồ sơ đã thanh tra, kiểm tra";
                ViewData["Title_parent"] = "Thanh tra";

                await OptionHoSoCoSo();
            }
            var listVanBan = new List<VanBanHoSoThanhTraCreateRequest>();
            if (request.DSVanBan != null && request.DSVanBan.Count > 0)
            {
                foreach (var v in request.DSVanBan)
                {
                    if (v.File != null)
                    {
                        v.FileName = v.File.FileName;
                        v.FilePath = await _fileApiClient.Upload(v.File);
                        v.File = null;
                    }
                    listVanBan.Add(v);
                }
            }
            request.DSVanBan = listVanBan;

            var result = await _hoSoThanhTraService.Create(request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            await Tracking("Thêm hồ sơ thanh tra cơ sở " + result.ResultObj.HoSo.Ten);
            if (type_sumit == "save")
            {
                return Redirect("/Thanhtra/Danhsach/");
            }
            else
            {
                return Redirect("/Thanhtra/Themmoi/");
            }
        }
        [Authorize(Roles = "edit_thanhtra,root")]
        public async Task<IActionResult> Suathongtin(string id)
        {
            ViewData["Title"] = "Sửa thông tin hồ sơ đã thanh tra, kiểm tra";
            ViewData["Title_parent"] = "Thanh tra";

            await OptionHoSoCoSo();
            await OptionKetQuaThanhTra();

            var resultObj = await _hoSoThanhTraService.GetById(Convert.ToInt32(HashUtil.DecodeID(id)));

            var model = new HoSoThanhTraUpdateRequest()
            {
                HoSoId = resultObj.HoSoId,
                Id = id,
                KetLuan = resultObj.KetLuan,
                NoiDung = resultObj.NoiDung,
                ThoiGian = resultObj.ThoiGian,
                TruongDoan = resultObj.TruongDoan,
                DSVanBanDaLuu = resultObj.DSVanBan,
                KetQua = resultObj.KetQua,
            };


            return View(model);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "edit_thanhtra,root")]
        public async Task<IActionResult> Suathongtin(HoSoThanhTraUpdateRequest request, string type_sumit)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Sửa thông tin hồ sơ đã thanh tra, kiểm tra";
                ViewData["Title_parent"] = "Thanh tra";

                await OptionHoSoCoSo();
            }
            var listVanBan = new List<VanBanHoSoThanhTraCreateRequest>();
            if (request.DSVanBan != null && request.DSVanBan.Count > 0)
            {
                foreach (var v in request.DSVanBan)
                {
                    if (v.File != null)
                    {
                        v.FileName = v.File.FileName;
                        v.FilePath = await _fileApiClient.Upload(v.File);
                        v.File = null;
                    }
                    listVanBan.Add(v);
                }
            }
            request.DSVanBan = listVanBan;

            var result = await _hoSoThanhTraService.Update(Convert.ToInt32(HashUtil.DecodeID(request.Id)), request);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });

            await Tracking("Sửa thông tin hồ sơ thanh tra cơ sở " + request.HoSoId);

            return Redirect("/Thanhtra/Danhsach/");
        }
        [Authorize(Roles = "view_thanhtra,root")]
        public async Task<IActionResult> Danhsach()
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
        [HttpPost]
        [Authorize(Roles = "delete_thanhtra,root")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xoathanhtra(string Id)
        {
            int id = Convert.ToInt32(HashUtil.DecodeID(Id));

            var result = await _hoSoThanhTraService.Delete(id);

            TempData.AddAlert(new Result<string>()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
            });
            await Tracking(result.Message);
            return Redirect(Request.GetBackUrl());
        }

    }
}
