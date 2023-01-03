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
    public interface IMucDoThongThaoNgoaiNguApiClient
    {
        Task<PagedResult<MucDoThongThaoNgoaiNguModel>> GetPagings(GetPagingRequest request);

        Task<List<MucDoThongThaoNgoaiNguModel>> GetAll();

        Task<ApiResult<MucDoThongThaoNgoaiNguModel>> Create(MucDoThongThaoNgoaiNguModel request);

        Task<ApiResult<bool>> Update(int id, MucDoThongThaoNgoaiNguModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<MucDoThongThaoNgoaiNguModel> GetById(int id);
    }
    public class MucDoThongThaoNgoaiNguApiClient :BaseApiClient, IMucDoThongThaoNgoaiNguApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public MucDoThongThaoNgoaiNguApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) 
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<MucDoThongThaoNgoaiNguModel>> GetPagings(GetPagingRequest request)
        {
            return await GetAsync<PagedResult<MucDoThongThaoNgoaiNguModel>>(
            $"/MucDoThongThaoNgoaiNgu/paging?pageIndex={request.PageIndex}" +
            $"&pageSize={request.PageSize}" +
            $"&keyword={request.Keyword}");

        }

        public async Task<ApiResult<MucDoThongThaoNgoaiNguModel>> Create(MucDoThongThaoNgoaiNguModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/MucDoThongThaoNgoaiNgu", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<MucDoThongThaoNgoaiNguModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<MucDoThongThaoNgoaiNguModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/MucDoThongThaoNgoaiNgu/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<MucDoThongThaoNgoaiNguModel>> GetAll()
        {
            return await GetAsync<List<MucDoThongThaoNgoaiNguModel>>($"/api/v1.0/MucDoThongThaoNgoaiNgu");
        }

        public async Task<MucDoThongThaoNgoaiNguModel> GetById(int id)
        {
            return await GetAsync<MucDoThongThaoNgoaiNguModel>($"/api/v1.0/MucDoThongThaoNgoaiNgu/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, MucDoThongThaoNgoaiNguModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/MucDoThongThaoNgoaiNgu/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
