using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Common.Extension;
using TechLife.Service;
using TechLife.App.ApiClients;

namespace TechLife.App.Controllers
{
    [AllowAnonymous]
    public class LoginWithSSOController : BaseController
    {
        private readonly ILoginWithSsoApiClient _loginWithSsoApiClient;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginWithSSOController> _logger;

        public LoginWithSSOController(IUserService userService,
            IConfiguration configuration,
            ITrackingService trackingService,
            ILoginWithSsoApiClient loginWithSsoApiClient,
            IMemoryCache memoryCache,
            ILogger<LoginWithSSOController> logger)
              : base(userService, configuration, trackingService)
        {
            _userService = userService;
            _configuration = configuration;
            _loginWithSsoApiClient = loginWithSsoApiClient;
            _logger = logger;
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

        public async Task<IActionResult> Index(string token, string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated) return Redirect(String.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);

                if (string.IsNullOrEmpty(token))
                    return Redirect(Request.GetRawUrlSSO());

                var ssoRessult = await _loginWithSsoApiClient.Authenticate(Request.GetRawUrlSSO(), token);

                if (ssoRessult == null || !ssoRessult.IsSuccess)
                    return Redirect(Request.GetRawUrlSSO());

                var result = await _userService.Authencate(ssoRessult.UserObj.TaiKhoan);

                if (!result.IsSuccessed)
                    return Redirect("/AccessDenied?type=3");

                var userPrincipal = this.ValidateToken(result.ResultObj);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(SystemConstants.AppSettings.ExpireMinutes),
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);

                return Redirect("/");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xác thực qua SSO TTH");
                return Redirect("/AccessDenied?type=3");
            }
        }
    }
}