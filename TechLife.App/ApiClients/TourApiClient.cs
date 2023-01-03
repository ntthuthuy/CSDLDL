using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;

namespace TechLife.App.ApiClients
{
    public interface ITourApiClient
    {
        Task<PagedResult<TourModel>> GetPagings(TourRequets request);

        Task<List<TourModel>> GetAll();

        Task<TourModel> GetById(int id);

        Task<ApiResult<TourModel>> Create(TourModel request);

        Task<ApiResult<HanhTrinhModel>> AddItem(HanhTrinhModel request);

        Task<HanhTrinhModel> GetItemById(int id);

        Task<ApiResult<bool>> Update(int id, TourModel request);

        Task<ApiResult<bool>> UpdateItem(int id, HanhTrinhModel request);

        Task<ApiResult<bool>> Delete(int id);
        Task<ApiResult<bool>> UploadImage(int tourId, ImageUploadRequest request);
    }

    public class TourApiClient : BaseApiClient, ITourApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TourApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ApiResult<HanhTrinhModel>> AddItem(HanhTrinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/tour/hanhtrinh", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<HanhTrinhModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<HanhTrinhModel>>(result);
        }

        public async Task<ApiResult<TourModel>> Create(TourModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/Tour", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<TourModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<TourModel>>(result);
        }

        public Task<ApiResult<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TourModel>> GetAll()
        {
            return await GetAsync<List<TourModel>>($"/api/v1.0/tour");
        }

        public async Task<TourModel> GetById(int id)
        {
            return await GetAsync<TourModel>($"/api/v1.0/tour/{id}");
        }

        public async Task<HanhTrinhModel> GetItemById(int id)
        {
            return await GetAsync<HanhTrinhModel>($"/api/v1.0/tour/hanhtrinh/{id}");
        }

        public async Task<PagedResult<TourModel>> GetPagings(TourRequets request)
        {
            var data = await GetAsync<PagedResult<TourModel>>(
             $"/tour/paging/?luhanh={request.luhanh}&pageIndex={request.PageIndex}" +
             $"&pageSize={request.PageSize}" +
             $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<ApiResult<bool>> Update(int id, TourModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/tour/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> UpdateItem(int id, HanhTrinhModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/tour/hanhtrinh/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
        public async Task<ApiResult<bool>> UploadImage(int tourId, ImageUploadRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request != null && request.Images != null && request.Images.Count > 0)
            {
                foreach (var img in request.Images)
                {
                    byte[] data;
                    using (var br = new BinaryReader(img.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)img.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    requestContent.Add(bytes, "Images", img.FileName);
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>("Không có file mới");
            }
            var response = await client.PostAsync($"/api/v1.0/Tour/{tourId}/upload", requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

    }
}