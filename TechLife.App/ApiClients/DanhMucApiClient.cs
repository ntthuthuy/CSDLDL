﻿using Microsoft.AspNetCore.Http;
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
    public interface IDanhMucApiClient
    {
        Task<PagedResult<DanhMucModel>> GetPagings(int loaiId,GetPagingRequest request);

        Task<List<DanhMucModel>> GetAll(int loaiId);
        Task<List<DanhMucModel>> GetAll();

        Task<ApiResult<DanhMucModel>> Create(DanhMucModel request);

        Task<ApiResult<bool>> Update(int id, DanhMucModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<DanhMucModel> GetById(int id);
    }
    public class DanhMucApiClient : BaseApiClient, IDanhMucApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DanhMucApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration) :
            base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<DanhMucModel>> GetPagings(int loaiId, GetPagingRequest request)
        {

            return await GetAsync<PagedResult<DanhMucModel>>(
             $"/DanhMuc/paging/{loaiId}?pageIndex={request.PageIndex}" +
             $"&pageSize={request.PageSize}" +
             $"&keyword={request.Keyword}");

        }

        public async Task<ApiResult<DanhMucModel>> Create(DanhMucModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/DanhMuc", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<DanhMucModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<DanhMucModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/DanhMuc/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<DanhMucModel>> GetAll(int loaiId)
        {
            return await GetAsync<List<DanhMucModel>>($"/api/v1.0/DanhMuc/getall/{loaiId}");
        }
        public async Task<List<DanhMucModel>> GetAll()
        {
            return await GetAsync<List<DanhMucModel>>($"/api/v1.0/DanhMuc/getall/");
        }

        public async Task<DanhMucModel> GetById(int id)
        {
            return await GetAsync<DanhMucModel>($"/api/v1.0/DanhMuc/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, DanhMucModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/DanhMuc/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
