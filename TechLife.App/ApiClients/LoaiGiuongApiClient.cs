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
    public interface ILoaiGiuongApiClient
    {
        Task<PagedResult<LoaiGiuongModel>> GetPagings(GetPagingRequest request);

        Task<List<LoaiGiuongModel>> GetAll();

        Task<ApiResult<LoaiGiuongModel>> Create(LoaiGiuongModel request);

        Task<ApiResult<bool>> Update(int id, LoaiGiuongModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<LoaiGiuongModel> GetById(int id);
    }
    public class LoaiGiuongApiClient : BaseApiClient, ILoaiGiuongApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoaiGiuongApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) 
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<LoaiGiuongModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<LoaiGiuongModel>>(
              $"/LoaiGiuong/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}");
        }

        public async Task<ApiResult<LoaiGiuongModel>> Create(LoaiGiuongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/LoaiGiuong", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<LoaiGiuongModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<LoaiGiuongModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/LoaiGiuong/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<LoaiGiuongModel>> GetAll()
        {
            return await GetAsync<List<LoaiGiuongModel>>($"/api/v1.0/LoaiGiuong/");
        }

        public async Task<LoaiGiuongModel> GetById(int id)
        {
            return await GetAsync<LoaiGiuongModel>($"/api/v1.0/LoaiGiuong/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, LoaiGiuongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/LoaiGiuong/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
