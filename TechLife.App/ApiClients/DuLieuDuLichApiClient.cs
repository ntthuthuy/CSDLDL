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
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoVanBan;

namespace TechLife.App.ApiClients
{
    public interface IDuLieuDuLichApiClient
    {
       
        Task<List<DuLieuDuLichModel>> GetAll(int linhvucId = 0);

        Task<DuLieuDuLichModel> GetById(int id);

        Task<ApiResult<DuLieuDuLichModel>> Create(DuLieuDuLichModel request);

        Task<ApiResult<bool>> Update(int id, DuLieuDuLichModel request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<bool>> UploadImage(int hosoId, ImageUploadRequest request);

        Task<ApiResult<bool>> UploadFile(int hosoId, DocumentUploadRequest request);

        Task UploadDoc(int hosoId, List<HoSoVanBanCreateRequets> request);

        Task<ApiResult<bool>> UploadImageExt(int hosoId, ImageUploadExtRequest request);


    }

    public class DuLieuDuLichApiClient : BaseApiClient, IDuLieuDuLichApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DuLieuDuLichApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

     
        public async Task<ApiResult<DuLieuDuLichModel>> Create(DuLieuDuLichModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1.0/DuLieuDuLich", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<DuLieuDuLichModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<DuLieuDuLichModel>>(result);
        }

        public async Task<ApiResult<bool>> UploadImage(int hosoId, ImageUploadRequest request)
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
            var response = await client.PostAsync($"/api/v1.0/DuLieuDuLich/{hosoId}/upload", requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task UploadDoc(int hosoId, List<HoSoVanBanCreateRequets> request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            if (request != null && request.Count > 0)
            {
                for (int i = 0; i < request.Count; i++)
                {
                    var requestContent = new MultipartFormDataContent();
                    if (request[i].File != null)
                    {
                        byte[] data;
                        using (var br = new BinaryReader(request[i].File.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)request[i].File.OpenReadStream().Length);
                        }
                        ByteArrayContent bytes = new ByteArrayContent(data);
                        requestContent.Add(bytes, "file", request[i].File.FileName);
                    }

                    requestContent.Add(new StringContent(string.IsNullOrEmpty(request[i].TenGoi) ? "" : request[i].TenGoi.ToString()), "tenGoi");
                    requestContent.Add(new StringContent(string.IsNullOrEmpty(request[i].MaSo) ? "" : request[i].MaSo.ToString()), "maSo");
                    requestContent.Add(new StringContent(request[i].NgayCap == null ? "" : request[i].NgayCap.ToString()), "ngayCap");
                    requestContent.Add(new StringContent(request[i].NgayHetHan == null ? "" : request[i].NgayHetHan.ToString()), "ngayHetHan");
                    requestContent.Add(new StringContent(string.IsNullOrEmpty(request[i].NoiCap) ? "" : request[i].NoiCap.ToString()), "noiCap");
                    await client.PostAsync($"/api/v1.0/DuLieuDuLich/{hosoId}/vanban", requestContent);
                }
            }
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/v1.0/DuLieuDuLich/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<DuLieuDuLichModel>> GetAll(int linhvucId = 0)
        {
            return await GetAsync<List<DuLieuDuLichModel>>($"/api/v1.0/dulieudulich/{linhvucId}");
        }

        public async Task<DuLieuDuLichModel> GetById(int id)
        {
            return await GetAsync<DuLieuDuLichModel>($"/api/v1.0/DuLieuDuLich/detail/{id}");
        }

        public async Task<ApiResult<bool>> Update(int id, DuLieuDuLichModel request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/v1.0/DuLieuDuLich/{id}", httpContent);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

                return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
            }
            catch (Exception ex)
            {
                return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> UploadFile(int hosoId, DocumentUploadRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request != null && request.DocumentFiles != null && request.DocumentFiles.Count > 0)
            {
                foreach (var img in request.DocumentFiles)
                {
                    byte[] data;
                    using (var br = new BinaryReader(img.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)img.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    requestContent.Add(bytes, "DocumentFiles", img.FileName);
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>("Không có file mới");
            }
            var response = await client.PostAsync($"/api/v1.0/DuLieuDuLich/{hosoId}/uploadfile", requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        //HueCIT
        public async Task<ApiResult<bool>> UploadImageExt(int hosoId, ImageUploadExtRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request != null && request.Images != null && request.Images.Count > 0)
            {
                int i = 0;
                foreach (var img in request.Images)
                {
                    if (img.Images != null) 
                    {
                        byte[] data;
                        using (var br = new BinaryReader(img.Images.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)img.Images.OpenReadStream().Length);
                        }
                        ByteArrayContent bytes = new ByteArrayContent(data);
                        requestContent.Add(bytes, "Images[" + i + "].Images", img.Images.FileName);
                        requestContent.Add(new StringContent(img.MoTa == null ? "" : img.MoTa), "Images[" + i + "].MoTa");
                        i++;
                    }
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>("Không có file mới");
            }
            var response = await client.PostAsync($"/api/v1.0/DuLieuDuLich/{hosoId}/uploadext", requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

    }
}