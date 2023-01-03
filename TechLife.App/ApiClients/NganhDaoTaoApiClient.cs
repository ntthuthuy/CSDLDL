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
    public interface INganhDaoTaoApiClient
    {
        Task<PagedResult<NganhDaoTaoModel>> GetPagings(GetPagingRequest request);

        Task<List<NganhDaoTaoModel>> GetAll();

        Task<ApiResult<NganhDaoTaoModel>> Create(NganhDaoTaoModel request);

        Task<ApiResult<bool>> Update(int id, NganhDaoTaoModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<NganhDaoTaoModel> GetById(int id);
    }
    public class NganhDaoTaoApiClient : BaseApiClient, INganhDaoTaoApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public NganhDaoTaoApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) :
            base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<NganhDaoTaoModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<NganhDaoTaoModel>>(
            $"/NganhDaoTao/paging?pageIndex={request.PageIndex}" +
            $"&pageSize={request.PageSize}" +
            $"&keyword={request.Keyword}");
        }

        public async Task<ApiResult<NganhDaoTaoModel>> Create(NganhDaoTaoModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/NganhDaoTao", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<NganhDaoTaoModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<NganhDaoTaoModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/NganhDaoTao/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<NganhDaoTaoModel>> GetAll()
        {
            return await GetAsync<List<NganhDaoTaoModel>>($"/api/v1.0/NganhDaoTao");
           
        }

        public async Task<NganhDaoTaoModel> GetById(int id)
        {
            return await GetAsync<NganhDaoTaoModel>($"/api/v1.0/NganhDaoTao/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, NganhDaoTaoModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/NganhDaoTao/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
