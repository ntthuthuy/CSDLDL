using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using TechLife.App.ApiClients;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.Tracking;
using TechLife.Service;
using System.Web;
using Newtonsoft.Json;

namespace TechLife.App.Controllers
{
    public class LoginController : Controller
    {
        IConfiguration _configuration;
        ITrackingService _trackingService;
        private readonly IUserService _userService;
        public LoginController(IConfiguration configuration
            , ITrackingService trackingService
            , IUserService userService)
        {
            _configuration = configuration;
            _trackingService = trackingService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Authenticate(string token, int count = 0, string ReturnUrl = "/")
        {
            try
            {
                if (count > 3)
                {
                    return Redirect(ReturnUrl);
                }
                if (String.IsNullOrEmpty(token))
                {
                    return Redirect("/AccessDenied?type=1");
                }
                if (String.IsNullOrEmpty(Request.Cookies["EsbUsersServicesToken"]))
                {
                    return Redirect("/AccessDenied?type=2");
                }
                if (String.IsNullOrEmpty(Request.Cookies["AccessToken"]))
                {
                    return Redirect("/AccessDenied?type=3");
                }
                string unaccessToken = HashUtil.Decrypt(Request.Cookies["AccessToken"]);
                string hashToken = HashUtil.Hash(unaccessToken);

                var arr = unaccessToken.Split("|");
                string username = arr[0];

                var result = await _userService.Authencate(username);
                if (!result.IsSuccessed)
                {
                    return Redirect("/AccessDenied");

                }

                var userPrincipal = this.ValidateToken(result.ResultObj);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent =true
                };
                HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
                HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);

                var user = await _userService.GetByUserName(username);
                string jsonUser = JsonConvert.SerializeObject(user.ResultObj);

                HttpContext.Session.SetString(SystemConstants.AppSettings.UserInfo, jsonUser);


                await _trackingService.Create(new TrackingCreateRequets()
                {
                    Action = "Đăng nhập bằng SSO",
                    FullName = username,
                    UserName = username,
                    Time = DateTime.Now
                });

                return Redirect(ReturnUrl);
            }
            catch
            {
                return Redirect(ReturnUrl);
            }
         
        }
        [HttpGet]
        public IActionResult Signin(string ReturnUrl = "/")
        {
            return Redirect("/sso");
            //return Redirect("/Login");
        }
        [HttpGet]
        public async Task<IActionResult> Index(string ReturnUrl = "/")
        {
            //ViewBag.ReturnUrl = ReturnUrl;
            await _userService.Logout();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove(SystemConstants.AppSettings.UserInfo);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel request, string ReturnUrl = "/")
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var result = await _userService.Login(request);
                if (result.ResultObj == null)
                {
                    ModelState.AddModelError("", result.Message);
                    return View();
                }
                var userPrincipal = this.ValidateToken(result.ResultObj);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = request.RememberMe
                };
                HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
                HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);

                var user = await _userService.GetByUserName(request.UserName);
                string jsonUser = JsonConvert.SerializeObject(user.ResultObj);

                HttpContext.Session.SetString(SystemConstants.AppSettings.UserInfo, jsonUser);

                await _trackingService.Create(new TrackingCreateRequets()
                {
                    Action = "Đăng nhập chuẩn",
                    FullName = request.UserName,
                    UserName = request.UserName,
                    Time = DateTime.Now
                });

                return Redirect(ReturnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }

        }

        [HttpPost]
        public IActionResult Sso(LoginModel request, string ReturnUrl = "/")
        {
            try
            {
                string urlRedirect = _configuration["LoginAddress"] + "/Home/Index/" + "?app_url=" + Request.GetUri() + "Login/Authenticate/";


                string url = _configuration["LoginAddress"] + "/Home/Signin/" + "?UserName=" + request.UserName + "&Password=" + request.Password + "&UrlLgoin=" + HttpContext.Request.GetUri() + "Login/Index/&UrlRedirect=" + urlRedirect;

                return Redirect(url);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }

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
    }
}
