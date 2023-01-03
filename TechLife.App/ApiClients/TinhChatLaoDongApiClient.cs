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
    public interface ITinhChatLaoDongApiClient
    {
        Task<PagedResult<TinhChatLaoDongModel>> GetPagings(GetPagingRequest request);

        Task<List<TinhChatLaoDongModel>> GetAll();

        Task<ApiResult<TinhChatLaoDongModel>> Create(TinhChatLaoDongModel request);

        Task<ApiResult<bool>> Update(int id, TinhChatLaoDongModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<TinhChatLaoDongModel> GetById(int id);
    }
    public class TinhChatLaoDongApiClient : BaseApiClient, ITinhChatLaoDongApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TinhChatLaoDongApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) :
            base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<TinhChatLaoDongModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<TinhChatLaoDongModel>>(
           $"/TinhChatLaoDong/paging?pageIndex={request.PageIndex}" +
           $"&pageSize={request.PageSize}" +
           $"&keyword={request.Keyword}");

        }

        public async Task<ApiResult<TinhChatLaoDongModel>> Create(TinhChatLaoDongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/TinhChatLaoDong", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<TinhChatLaoDongModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<TinhChatLaoDongModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/TinhChatLaoDong/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<TinhChatLaoDongModel>> GetAll()
        {
            return await GetAsync<List<TinhChatLaoDongModel>>($"/api/v1.0/TinhChatLaoDong");
        }

        public async Task<TinhChatLaoDongModel> GetById(int id)
        {
            return await GetAsync<TinhChatLaoDongModel>($"/api/v1.0/TinhChatLaoDong/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, TinhChatLaoDongModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/TinhChatLaoDong/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
