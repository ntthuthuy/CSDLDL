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
    public interface ILoaiDichVuApiClient
    {
        Task<PagedResult<LoaiDichVuModel>> GetPagings(GetPagingRequest request);

        Task<List<LoaiDichVuModel>> GetAll();

        Task<ApiResult<LoaiDichVuModel>> Create(LoaiDichVuModel request);

        Task<ApiResult<bool>> Update(int id, LoaiDichVuModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<LoaiDichVuModel> GetById(int id);
    }
    public class LoaiDichVuApiClient : BaseApiClient, ILoaiDichVuApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoaiDichVuApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<LoaiDichVuModel>> GetPagings(GetPagingRequest request)
        {
            var data = await GetAsync<PagedResult<LoaiDichVuModel>>(
             $"/LoaiDichVu/paging?pageIndex={request.PageIndex}" +
             $"&pageSize={request.PageSize}" +
             $"&keyword={request.Keyword}");

            return data;

        }

        public async Task<ApiResult<LoaiDichVuModel>> Create(LoaiDichVuModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/LoaiDichVu", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<LoaiDichVuModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<LoaiDichVuModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/LoaiDichVu/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<LoaiDichVuModel>> GetAll()
        {
            return await GetAsync<List<LoaiDichVuModel>>($"/api/v1.0/LoaiDichVu");
        }

        public async Task<LoaiDichVuModel> GetById(int id)
        {
            return await GetAsync<LoaiDichVuModel>($"/api/v1.0/LoaiDichVu/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, LoaiDichVuModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/LoaiDichVu/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
