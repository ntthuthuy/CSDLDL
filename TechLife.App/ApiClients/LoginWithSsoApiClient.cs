using TechLife.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TechLife.App.ApiClients
{
    public interface ILoginWithSsoApiClient
    {
        Task<KetQuaXacThuc> Authenticate(string token);
    }
    public class LoginWithSsoApiClient : BaseApiClient, ILoginWithSsoApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public LoginWithSsoApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<KetQuaXacThuc> Authenticate(string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync($"{_configuration[SystemConstants.AppSettings.SsoAddress]}/Authenticate/?token={token}", null);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<KetQuaXacThuc>(result);

                return JsonConvert.DeserializeObject<KetQuaXacThuc>(result);
            }
            catch
            {
                return new KetQuaXacThuc()
                {
                    IsSuccess = false,
                    Message = "Xác thực không thành công",
                    UserObj = null
                };
            }
        }
    }
}
