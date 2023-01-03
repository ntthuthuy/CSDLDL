using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;

namespace TechLife.App.ApiClients
{
    public interface IDiemVeSinhApiClient
    {
        Task<PagedResult<DiemVeSinhModel>> GetPagings(GetPagingRequest request);

        Task<List<DiemVeSinhModel>> GetAll();
        Task<DiemVeSinhModel> GetById(int id);

        Task<ApiResult<DiemVeSinhModel>> Create(DiemVeSinhModel request);

        Task<ApiResult<bool>> Update(int id, DiemVeSinhModel request);

        Task<ApiResult<bool>> Delete(int id);
       
    }
    public class DiemVeSinhApiClient : BaseApiClient, IDiemVeSinhApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DiemVeSinhApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) 
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<DiemVeSinhModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<DiemVeSinhModel>>($"/api/v1.0/DiemVeSinh/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}&NguonDongBo={request.NguonDongBo ?? -1}");
        }

        public async Task<ApiResult<DiemVeSinhModel>> Create(DiemVeSinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/DiemVeSinh", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<DiemVeSinhModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<DiemVeSinhModel>>(result);

        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/DiemVeSinh/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<DiemVeSinhModel>> GetAll()
        {
            return await GetAsync<List<DiemVeSinhModel>>($"/api/v1.0/DiemVeSinh");
        }
        public async Task<DiemVeSinhModel> GetById(int id)
        {
            return await GetAsync<DiemVeSinhModel>($"/api/v1.0/DiemVeSinh/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, DiemVeSinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/DiemVeSinh/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>("Đã có lỗi xãy ra!");
        }
    }
}
