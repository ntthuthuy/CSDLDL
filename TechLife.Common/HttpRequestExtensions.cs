using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TechLife.Model;

namespace TechLife.Common
{
    public static class HttpRequestExtensions
    {
        public static string GetRawUrl(this HttpRequest request)
        {
            var httpContext = request.HttpContext;
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
        }
        public static string GetBackUrl(this HttpRequest request)
        {
            var httpContext = request.HttpContext;
            string url = httpContext.Request.Headers["Referer"].ToString();
            if (!String.IsNullOrEmpty(url)) return url;
            else return "/";
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
            string langId = request.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            if (!string.IsNullOrEmpty(langId))
                return request.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            else return "vi";
        }
        public static UserModel GetUser(this HttpRequest request)
        {
            var data = request.HttpContext.Session.GetString(SystemConstants.AppSettings.UserInfo);
            if (!String.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else
            {
                return null;
            }
        }
        public static string GetRawUrl(this HttpRequest request, string url, bool IsQuery = true)
        {
            var httpContext = request.HttpContext;
            if (!IsQuery)
            {
                return $"{url}";
            }
            else
                return $"{url}{HttpUtility.UrlDecode(httpContext.Request.QueryString.ToString())}";
        }

    }
}
