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
using TechLife.Model.HuongDanVien;

namespace TechLife.App.ApiClients
{
    public interface IHuongDanVienApiClient
    {
        Task<PagedResult<HuongDanVienModel>> GetPagings(GetPagingRequest request);

        Task<List<HuongDanVienModel>> GetAll();

        Task<HuongDanVienModel> GetById(int id);

        Task<ApiResult<HuongDanVienModel>> Create(HuongDanVienModel request);

        Task<ApiResult<bool>> Update(int id, HuongDanVienModel request);

        Task<ApiResult<bool>> Delete(int id);
        Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request);
    }

    public class HuongDanVienApiClient : BaseApiClient, IHuongDanVienApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public HuongDanVienApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<PagedResult<HuongDanVienModel>> GetPagings(GetPagingRequest request)
        {
            var data = await GetAsync<PagedResult<HuongDanVienModel>>(
              $"/huongdanvien/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<List<HuongDanVienModel>> GetAll()
        {
            return await GetListAsync<HuongDanVienModel>($"/api/v1.0/huongdanvien");
        }

        public async Task<HuongDanVienModel> GetById(int id)
        {
            return await GetAsync<HuongDanVienModel>($"/api/v1.0/huongdanvien/{id}");
        }

        public async Task<ApiResult<HuongDanVienModel>> Create(HuongDanVienModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/huongdanvien", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<HuongDanVienModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<HuongDanVienModel>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/huongdanvien/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> Update(int id, HuongDanVienModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1.0/huongdanvien/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> UploadImage(int id, ImageUploadRequest request)
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
            var response = await client.PostAsync($"/api/v1.0/huongdanvien/{id}/upload", requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}