using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using TechLife.Common.Enums;
using TechLife.Model;

namespace TechLife.Common.Extension
{
    public static class HttpRequestExtensions
    {
        public static void AddCookie(this HttpResponse response, string cookieName, string value)
        {
            response.Cookies.Append(cookieName, value,
                new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    Expires = DateTime.Now.AddMinutes(5)
                }
            );
        }

        public static string GetCookie(this HttpRequest request, string cookieName)
        {
            var cookies = request.Cookies.Select((header) => $"{header.Key}");
            if (cookies.Contains(cookieName))
            {
                return request.Cookies[cookieName];
            }

            return string.Empty;
        }

        public static void RemoveCookie(this HttpRequest request, string cookieName)
        {
            var cookies = request.Cookies.Select((header) => $"{header.Key}");
            if (cookies.Contains(cookieName))
            {
                request.HttpContext.Response.Cookies.Delete(cookieName);
            }
        }

        public static Uri GetUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80)
            };
            return uriBuilder.Uri;
        }

        public static string GetLanguageId(this HttpRequest request)
        {
            try
            {
                string langId = request.Cookies[".AspNetCore.Culture"];
                if (!string.IsNullOrEmpty(langId))
                {
                    return langId.Substring(2, 2);
                }
                else
                {
                    return "vi";
                }
            }
            catch
            {
                return "vi";
            }
        }

        public static string GetUserName(this HttpRequest request)
        {
            string userName = request.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
                return userName;
            else return "";
        }

        public static UserLoginVm GetUser(this HttpRequest request)
        {
            try
            {
                if (request.HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = request.HttpContext.User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return null;

                    return new UserLoginVm()
                    {
                        Id = Guid.Parse(userId),
                        UserName = request.HttpContext.User.Identity.Name,
                        FullName = request.HttpContext.User.FindFirst("FullName")?.Value,
                        AvartarUrl = request.HttpContext.User.FindFirst("AvartarUrl")?.Value ?? string.Empty,
                        LoginType = Enum.Parse<LoginType>(request.HttpContext.User.FindFirst("LoginType")?.Value ?? LoginType.Default.ToString()),
                    };
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime GetVersion(this HttpRequest request)
        {
            try
            {
                if (request.HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = request.HttpContext.User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return DateTime.MinValue;

                    return Convert.ToDateTime(request.HttpContext.User.FindFirst("Version")?.Value ?? DateTime.MinValue.ToString());
                }
                else
                    return DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetVersion(this ClaimsPrincipal User)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return DateTime.MinValue;

                    return Convert.ToDateTime(User.FindFirst("Version")?.Value ?? DateTime.MinValue.ToString());
                }
                else return DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static int? GetPageId(this ClaimsPrincipal User)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return null;

                    return Convert.ToInt32(User.FindFirst("PageId").Value);
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return Guid.Empty;

                    return Guid.Parse(userId);
                }
                else return Guid.Empty;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static UserLoginVm GetUser(this ClaimsPrincipal User, params string[] permissionInRole)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("Id").Value;
                    if (string.IsNullOrEmpty(userId)) return null;

                    return new UserLoginVm()
                    {
                        Id = Guid.Parse(userId),
                        UserName = User.Identity.Name,
                        FullName = User.FindFirst("FullName")?.Value,
                        AvartarUrl = User.FindFirst("AvartarUrl")?.Value ?? string.Empty,
                        IdToken = User.FindFirst("IdToken")?.Value ?? string.Empty,
                        LoginType = Enum.Parse<LoginType>(User.FindFirst("LoginType")?.Value ?? LoginType.Default.ToString()),
                    };
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public static string GetDomain(this HttpRequest request)
        {
            return $"{request.Host}";
        }

        public static string GetUrlDomain(this HttpRequest request, string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            return $"{request.Scheme}://{url}";
        }

        public static string GetUrl(this HttpRequest request, string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            return $"{request.Scheme}://{request.Host}{url.Trim()}";
        }

        public static string GetUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
    }
}