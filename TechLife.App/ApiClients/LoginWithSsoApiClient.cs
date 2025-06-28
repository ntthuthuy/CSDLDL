using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;

namespace TechLife.App.ApiClients
{
    public interface ILoginWithSsoApiClient
    {
        Task<TokenDetails> AuthenticateSSOVNeID(string urlSSO, string code);

        Task<TokenDetails> AuthenticateSSOOrgan(string urlSSO, string code);

        Task<ApiResult<UserDetail>> GetUserInfo(string access_token);

        Task<ApiResult<UserDetail>> GetOrganInfo(string access_token);

        Task<ApiResult<TokenDetails>> GetTokenDetails(string access_token);
    }

    public class LoginWithSsoApiClient : BaseApiClient, ILoginWithSsoApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginWithSsoApiClient> _logger;

        public LoginWithSsoApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<LoginWithSsoApiClient> logger)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<TokenDetails> AuthenticateSSOVNeID(string urlSSO, string code)
        {
            try
            {
                string endpointHost = "https://sso.huecity.vn";
                string currentSiteCallbackUri = urlSSO;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(endpointHost);

                string contentString = string.Format("grant_type={0}" +
                       "&redirect_uri={1}" +
                       "&client_id={2}" +
                       "&client_secret={3}" +
                       "&code={4}",
                       "authorization_code",
                       urlSSO,
                       "to8OUqrGhQyJJ7O",
                       "g3UlhAXnobEw4ZsxuEW4vUBfnDLDtYo",
                       code);

                StringContent contentParams = new StringContent(contentString, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync($"/auth/realms/hues/protocol/openid-connect/token", contentParams);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Token: {0}", result);
                    return JsonConvert.DeserializeObject<TokenDetails>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực tài khoản {0}", code);
                return null;
            }
        }

        public async Task<TokenDetails> AuthenticateSSOOrgan(string urlSSO, string code)
        {
            try
            {
                string endpointHost = "https://sso.huecity.vn";
                string currentSiteCallbackUri = urlSSO;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(endpointHost);

                string contentString = string.Format("grant_type={0}" +
                       "&redirect_uri={1}" +
                       "&client_id={2}" +
                       "&client_secret={3}" +
                       "&code={4}",
                       "authorization_code",
                       urlSSO,
                       "to8OUqrGhQyJJ7O",
                       "g3UlhAXnobEw4ZsxuEW4vUBfnDLDtYo",
                       code);

                StringContent contentParams = new StringContent(contentString, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync($"/auth/realms/org/protocol/openid-connect/token", contentParams);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Token: {0}", result);
                    return JsonConvert.DeserializeObject<TokenDetails>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực tài khoản {0}", code);
                return null;
            }
        }

        public async Task<ApiResult<UserDetail>> GetUserInfo(string access_token)
        {
            try
            {
                UserDetail userDetails = null;

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://sso.huecity.vn");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var response = await client.PostAsync($"/auth/realms/hues/protocol/openid-connect/userinfo", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    userDetails = JsonConvert.DeserializeObject<UserDetail>(result);

                    return new ApiSuccessResult<UserDetail>(userDetails, "Xác thực thành công");
                }
                return new ApiErrorResult<UserDetail>("Xác thực không thành công. Lỗi: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực tài khoản {0}", access_token);
                return new ApiErrorResult<UserDetail>("Xác thực không thành công. Lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResult<UserDetail>> GetOrganInfo(string access_token)
        {
            try
            {
                UserDetail userDetails = null;

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://sso.huecity.vn");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var response = await client.PostAsync($"/auth/realms/org/protocol/openid-connect/userinfo", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    userDetails = JsonConvert.DeserializeObject<UserDetail>(result);

                    return new ApiSuccessResult<UserDetail>(userDetails, "Xác thực thành công");
                }
                return new ApiErrorResult<UserDetail>("Xác thực không thành công. Lỗi: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực tài khoản {0}", access_token);
                return new ApiErrorResult<UserDetail>("Xác thực không thành công. Lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResult<TokenDetails>> GetTokenDetails(string access_token)
        {
            try
            {
                TokenDetails userDetails = null;

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://sso.huecity.vn");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var response = await client.PostAsync($"/auth/realms/org/protocol/openid-connect/token", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    userDetails = JsonConvert.DeserializeObject<TokenDetails>(result);

                    return new ApiSuccessResult<TokenDetails>(userDetails, "Xác thực thành công");
                }
                return new ApiErrorResult<TokenDetails>("Xác thực không thành công. Lỗi: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực tài khoản {0}", access_token);
                return new ApiErrorResult<TokenDetails>("Xác thực không thành công. Lỗi: " + ex.Message);
            }
        }
    }

    public class ContentParams
    {
        public string grant_type { get; set; }
        public string redirect_uri { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string code { get; set; }
    }

    public class UserDetail
    {
        public string sub { get; set; }
        public string birthdate { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }
        public string preferred_username { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string citizenId { get; set; }
        public string identityCardId { get; set; }
        public List<IdentityPapers> identityPapers { get; set; }
        public TokenDetails TokenDetail { get; set; }
    }

    public class IdentityPapers
    {
        public string type { get; set; }
        public string idNumber { get; set; }
    }

    public class TokenDetails
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }
    }
}