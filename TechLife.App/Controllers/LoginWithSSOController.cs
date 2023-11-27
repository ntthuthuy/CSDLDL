using TechLife.App.ApiClients;
using TechLife.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TechLife.Service;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RestSharp;

namespace TechLife.App.Controllers
{
    public class LoginWithSSOController : Controller
    {
        private readonly ILoginWithSsoApiClient _loginWithSsoApiClient;
        private readonly IUserService _userService;
        public readonly IConfiguration _configuration;
        public readonly ITrackingService _trackingService;

        public LoginWithSSOController(IUserService userService, IConfiguration configuration, ITrackingService trackingService, ILoginWithSsoApiClient loginWithSsoApiClient)
        {
            _userService = userService;
            _configuration = configuration;
            _trackingService = trackingService;
            _loginWithSsoApiClient = loginWithSsoApiClient;
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
       
        public async Task<IActionResult> Index(string token)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");

            if (string.IsNullOrEmpty(token))
                return Redirect(_configuration[SystemConstants.AppSettings.SsoAddress]);
            var ssoRessult = await _loginWithSsoApiClient.Authenticate(token);
            if (ssoRessult == null || !ssoRessult.IsSuccess)
            {
                return Redirect(_configuration[SystemConstants.AppSettings.SsoAddress]);
            }
            var result = await _userService.Authencate(ssoRessult.UserObj.TaiKhoan);

            if (String.IsNullOrEmpty(result.ResultObj))
            {
                return Redirect("/AccessDenied?type=3");
            }
           
            var userPrincipal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddMinutes(Int32.MaxValue),
                IsPersistent = true,
            };
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);

            var user = await _userService.GetByUserName(ssoRessult.UserObj.TaiKhoan);

            string jsonUser = JsonConvert.SerializeObject(user.ResultObj);

            HttpContext.Session.SetString(SystemConstants.AppSettings.UserInfo, jsonUser);

            await _trackingService.Create(new TechLife.Model.Tracking.TrackingCreateRequets() { Action = "Đăng nhập hệ thống bằng tài khoản SSO", UserName = ssoRessult.UserObj.TaiKhoan });

            return Redirect("/");
        }
    }
}
