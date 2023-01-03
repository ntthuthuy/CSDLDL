using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace TechLife.SSO.ApiClients
{

    public static class UserApiClient
    {
        static string urlApi = WebConfigurationManager.AppSettings["ApiUrl"];
        class ApiResult
        {
            public bool isSuccessed { get; set; }
            public string message { get; set; }
            public Object validationErrors { get; set; }
            public string resultObj { get; set; }
        }
        public static string Authenticate(string username)
        {
            try
            {
                string url = urlApi + "/Users/authenticate/" + username + "";

                UriBuilder builder = new UriBuilder(url);
                HttpClient client = new HttpClient();

                var result = client.GetAsync(builder.Uri).Result;
                HttpResponseMessage response = result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
                using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                {
                    string jsonString = JsonConvert.DeserializeObject(sr.ReadToEnd()).ToString();
                    var resule = JsonConvert.DeserializeObject<ApiResult>(jsonString);

                    return resule.resultObj;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}