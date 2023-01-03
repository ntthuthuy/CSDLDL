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
    public interface ILoaiHinhLaoDongApiClient
    {
        Task<PagedResult<LoaiHinhLaoDongModel>> GetPagings(GetPagingRequest request);

        Task<List<LoaiHinhLaoDongModel>> GetAll();

        Task<ApiResult<LoaiHinhLaoDongModel>> Create(LoaiHinhLaoDongModel request);

        Task<ApiResult<bool>> Update(int id, LoaiHinhLaoDongModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<LoaiHinhLaoDongModel> GetById(int id);
    }
    public class LoaiHinhLaoDongApiClient : BaseApiClient, ILoaiHinhLaoDongApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoaiHinhLaoDongApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<LoaiHinhLaoDongModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<LoaiHinhLaoDongModel>>(
             $"/LoaiHinhLaoDong/paging?pageIndex={request.PageIndex}" +
             $"&pageSize={request.PageSize}" +
             $"&keyword={request.Keyword}");
        
        }

        public async Task<ApiResult<LoaiHinhLaoDongModel>> Create(LoaiHinhLaoDongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/LoaiHinhLaoDong", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<LoaiHinhLaoDongModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<LoaiHinhLaoDongModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/LoaiHinhLaoDong/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<LoaiHinhLaoDongModel>> GetAll()
        {
           return await GetAsync<List<LoaiHinhLaoDongModel>>($"/api/v1.0/LoaiHinhLaoDong");

        }

        public async Task<LoaiHinhLaoDongModel> GetById(int id)
        {
            return await GetAsync<LoaiHinhLaoDongModel>($"/api/v1.0/LoaiHinhLaoDong/{id}");
            
        }

        public async Task<ApiResult<bool>> Update(int id, LoaiHinhLaoDongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/LoaiHinhLaoDong/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
