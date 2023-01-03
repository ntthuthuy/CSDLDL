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
    public interface IDonViTinhApiClient
    {
        Task<PagedResult<DonViTinhModel>> GetPagings(GetPagingRequest request);

        Task<List<DonViTinhModel>> GetAll();

        Task<ApiResult<DonViTinhModel>> Create(DonViTinhModel request);

        Task<ApiResult<bool>> Update(int id, DonViTinhModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<DonViTinhModel> GetById(int id);
    }
    public class DonViTinhApiClient : BaseApiClient, IDonViTinhApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DonViTinhApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) 
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<DonViTinhModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<DonViTinhModel>>($"/api/v1.0/DonViTinh/paging?pageIndex=" +
               $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
        }

        public async Task<ApiResult<DonViTinhModel>> Create(DonViTinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/DonViTinh", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<DonViTinhModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<DonViTinhModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/DonViTinh/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<DonViTinhModel>> GetAll()
        {
            return await GetAsync<List<DonViTinhModel>>($"/api/v1.0/DonViTinh");
        }

        public async Task<DonViTinhModel> GetById(int id)
        {
            return await GetAsync<DonViTinhModel>($"/api/v1.0/DonViTinh/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, DonViTinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/DonViTinh/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
