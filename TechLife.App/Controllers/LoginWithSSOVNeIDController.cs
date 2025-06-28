using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Common.Extension;
using TechLife.Model.Tracking;
using TechLife.Service;

namespace TechLife.App.Controllers
{
    public class LoginWithSSOVNeIDController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly ILoginWithSsoApiClient loginWithSsoApiClient;
        private readonly ILogger<LoginWithSSOVNeIDController> logger;

        public LoginWithSSOVNeIDController(IUserService userService,
            IConfiguration configuration,
            ITrackingService trackingService,
            ILoginWithSsoApiClient loginWithSsoApiClient,
            ILogger<LoginWithSSOVNeIDController> logger)
            : base(userService, configuration, trackingService)
        {
            this.configuration = configuration;
            this.loginWithSsoApiClient = loginWithSsoApiClient;
            this.logger = logger;
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string code, string returnUrl)
        {
            string urlCallBack = Request.GetRawUrlSSOVNeID();

            if (!User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(code))
                {
                    string returnUrlCallBack = Request.GetCookie("ReturnUrl");

                    returnUrl = !String.IsNullOrWhiteSpace(returnUrlCallBack) ? returnUrlCallBack : "/";

                    Response.Cookies.Delete("ReturnUrl");

                    var token = await loginWithSsoApiClient.AuthenticateSSOVNeID(urlCallBack, code);

                    if (token != null)
                    {
                        var info = await loginWithSsoApiClient.GetUserInfo(token.access_token);
                        if (info.IsSuccessed)
                        {
                            if (String.IsNullOrWhiteSpace(info.ResultObj.citizenId))
                                return Redirect("/AccessDenied?type=3");

                            info.ResultObj.TokenDetail = token;
                          
                            var result = await _userService.AuthencateByCitizen(info.ResultObj.citizenId, info.ResultObj.TokenDetail?.id_token ?? "", info.ResultObj.avatar);

                            if (!result.IsSuccessed || String.IsNullOrEmpty(result.ResultObj))
                                return Redirect("/AccessDenied?type=3");

                            var userPrincipal = this.ValidateToken(result.ResultObj);

                            var authProperties = new AuthenticationProperties
                            {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(SystemConstants.AppSettings.ExpireMinutes),
                                IsPersistent = true
                            };
                            var user = userPrincipal.GetUser();

                            await _trackingService.Create(new TrackingCreateRequets()
                            {
                                Action = "Xác thực qua hệ thống SSO Hue-S",
                                FullName = user.FullName,
                                UserName = user.UserName,
                                Time = DateTime.Now
                            });

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);

                            return Redirect(returnUrl);
                        }
                    }

                    return Redirect("/AccessDenied?type=3");
                }

                Response.AddCookie("ReturnUrl", String.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);

                return Redirect($"https://sso.huecity.vn/auth/realms/hues/protocol/openid-connect/auth?response_type=code&client_id=to8OUqrGhQyJJ7O&redirect_uri={urlCallBack}");
            }

            return Redirect(String.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
        }
    }
}