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
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoVanBan;
using TechLife.Model.HueCIT;

namespace TechLife.App.ApiClients.HueCIT
{
    public interface IFileUploaderApiClient
    {
        Task<ApiResult<bool>> UploadFiles(FileUploadTargetRequestAdd target, FilesUploadRequest request);
    }

    public class FileUploaderApiClient : BaseApiClient, IFileUploaderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public FileUploaderApiClient(IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ApiResult<bool>> UploadFiles(FileUploadTargetRequestAdd target, FilesUploadRequest request)
         {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            client.DefaultRequestHeaders.Add("accept", "application/octet-stream");

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
                    requestContent.Add(bytes, "Files", img.FileName);
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>("Không có file mới");
            }

            string uri = "/api/v1.0/FileUploader/upload/" + target.LoaiDoiTuong + "/" + target.ID;

            var response = await client.PostAsync(uri, requestContent);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

    }
}