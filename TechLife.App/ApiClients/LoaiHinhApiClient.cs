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
    public interface ILoaiHinhApiClient
    {
        Task<PagedResult<LoaiHinhModel>> GetPagings(GetPagingRequest request);

        Task<List<LoaiHinhModel>> GetAll();

        Task<ApiResult<LoaiHinhModel>> Create(LoaiHinhModel request);

        Task<ApiResult<bool>> Update(int id, LoaiHinhModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<LoaiHinhModel> GetById(int id);
    }
    public class LoaiHinhApiClient : BaseApiClient, ILoaiHinhApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoaiHinhApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) :
            base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<LoaiHinhModel>> GetPagings(GetPagingRequest request)
        {

            return await GetAsync<PagedResult<LoaiHinhModel>>(
             $"/LoaiHinh/paging?pageIndex={request.PageIndex}" +
             $"&pageSize={request.PageSize}" +
             $"&keyword={request.Keyword}");

        }

        public async Task<ApiResult<LoaiHinhModel>> Create(LoaiHinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/LoaiHinh", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<LoaiHinhModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<LoaiHinhModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/LoaiHinh/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<LoaiHinhModel>> GetAll()
        {
            return await GetAsync<List<LoaiHinhModel>>($"/api/v1.0/LoaiHinh");
        }

        public async Task<LoaiHinhModel> GetById(int id)
        {
            return await GetAsync<LoaiHinhModel>($"/api/v1.0/LoaiHinh/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, LoaiHinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/LoaiHinh/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
