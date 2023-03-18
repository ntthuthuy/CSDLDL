using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.HSCV;
using TechLife.Model.VanBanDen;

namespace TechLife.App.ApiClients
{
    public interface IHscvApiClient
    {
        Task<List<PhongBanVm>> DSPhongBan(string madinhdanh);
        Task<List<TrungTamVm>> DSPTrungTam(string madinhdanh);
        Task<List<TaiKhoanVm>> DSTaiKhoan(string madinhdanh);
        Task<PagedResult<VanBanDen>> DSVanBanDen(string madinhdanh, VanBanDenRequest request);
    }

    public class HscvApiClient : BaseApiClient, IHscvApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public HscvApiClient(IHttpClientFactory httpClientFactory
           , IHttpContextAccessor httpContextAccessor
           , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<List<PhongBanVm>> DSPhongBan(string madinhdanh)
        {
            return await GetHSCVAsync<List<PhongBanVm>>($"/API/AdministrationOrganization/Dep/getAllDepartmentOfOrganization?organId={madinhdanh}");
        }
        public async Task<List<TrungTamVm>> DSPTrungTam(string madinhdanh)
        {
            return await GetHSCVAsync<List<TrungTamVm>>($"/API/AdministrationOrganization/Org/getAllSubOrganization?organId={madinhdanh}");
        }
        public async Task<List<TaiKhoanVm>> DSTaiKhoan(string madinhdanh)
        {
            return await GetHSCVAsync<List<TaiKhoanVm>>($"/API/AdministrationOrganization/Emp/_getListEmployeeInOrganizationByUniquecode?uniqueCode={madinhdanh}");
        }

        public async Task<PagedResult<VanBanDen>> DSVanBanDen(string madinhdanh, VanBanDenRequest request)
        {
            var data = await GetHSCVAsync<VanBanDenVm>($"/DesktopModules/PMHSCV/Api/VBDen/GetDSVanBanDen?trangThu={request.PageIndex}&soBangGhiTrenMoiTrang={request.PageSize}&maDinhDanhCoQuanNhan={madinhdanh}&ngayDenTu={request.NgayBanHanh.Split('-')[0].Trim()}&ngayDenDen={request.NgayBanHanh.Split('-')[1].Trim()}&tuKhoa={request.Keyword}");
            
            return new PagedResult<VanBanDen>()
            {
                 PageIndex = data.ThongTinTrangDuLieu.TrangHienTai,
                 PageSize =data.ThongTinTrangDuLieu.SoBangGhiTraVe,
                 TotalRecords =data.ThongTinTrangDuLieu.TongSoBangGhi,
                 Items = data.DSVanBanDen,
            };
        }
    }
}
