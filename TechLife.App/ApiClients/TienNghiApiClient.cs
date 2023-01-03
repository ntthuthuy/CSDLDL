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
    public interface ITienNghiApiClient
    {
        Task<PagedResult<TienNghiModel>> GetPagings(GetPagingRequest request);

        Task<List<TienNghiModel>> GetAll();

        Task<ApiResult<TienNghiModel>> Create(TienNghiModel request);

        Task<ApiResult<bool>> Update(int id, TienNghiModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<TienNghiModel> GetById(int id);
    }
    public class TienNghiApiClient : BaseApiClient, ITienNghiApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TienNghiApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) :
              base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<TienNghiModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<TienNghiModel>>(
            $"/TienNghi/paging?pageIndex={request.PageIndex}" +
            $"&pageSize={request.PageSize}" +
            $"&keyword={request.Keyword}");

        }

        public async Task<ApiResult<TienNghiModel>> Create(TienNghiModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/TienNghi", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<TienNghiModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<TienNghiModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/TienNghi/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<TienNghiModel>> GetAll()
        {
            return await GetAsync<List<TienNghiModel>>($"/api/v1.0/TienNghi");
        }

        public async Task<TienNghiModel> GetById(int id)
        {
            return await GetAsync<TienNghiModel>($"/api/v1.0/TienNghi/{id}");
         
        }

        public async Task<ApiResult<bool>> Update(int id, TienNghiModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/TienNghi/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
