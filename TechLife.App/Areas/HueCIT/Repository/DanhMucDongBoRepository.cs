using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class DanhMucDongBoRepository : IDanhMucDongBoRepository
    {
        private readonly IConfiguration _config;
        public DanhMucDongBoRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> AddOrEditEformid(DanhMucEformidFormData param)
        {
            try
            {
                int id = -1;

                var token = _config.GetValue<string>("CSDLDuLichToken");
                var baseurl = _config.GetValue<string>("SoHoaCapNhatAddress");

                var data = new DanhMucEformidFormData
                {
                    serviceid = param.serviceid,
                    eformid = param.eformid,
                    idloaihinh = param.idloaihinh,
                    tenloaihinh = param.tenloaihinh,
                };

                var client = new RestClient(baseurl);
                client.Timeout = -1;

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("token", token);
                request.AlwaysMultipartFormData = true;

                request.AddParameter("serviceid", data.serviceid);
                request.AddParameter("eformid", data.eformid);
                request.AddParameter("idloaihinh", data.idloaihinh);
                request.AddParameter("tenloaihinh", data.tenloaihinh);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK) && response != null)
                {
                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhMucResponse>(response.Content);
                    id = Convert.ToInt32(dataDaXuLy.data);
                }

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> AddOrEditDiemDuLich(DanhMucDiemDuLichFormData param)
        {
            try
            {
                int id = -1;

                var token = _config.GetValue<string>("CSDLDuLichToken");
                var baseurl = _config.GetValue<string>("SoHoaCapNhatAddress");

                var data = new DanhMucDiemDuLichFormData
                {
                    serviceid = param.serviceid,
                    diemdlid = param.diemdlid,
                    idloaihinh = param.idloaihinh,
                    tenloaihinh = param.tenloaihinh,
                };

                var client = new RestClient(baseurl);
                client.Timeout = -1;

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("token", token);
                request.AlwaysMultipartFormData = true;

                request.AddParameter("serviceid", data.serviceid);
                request.AddParameter("diemdlid", data.diemdlid);
                request.AddParameter("idloaihinh", data.idloaihinh);
                request.AddParameter("tenloaihinh", data.tenloaihinh);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK) && response != null)
                {
                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhMucResponse>(response.Content);
                    id = Convert.ToInt32(dataDaXuLy.data);
                }

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> AddOrEditCoSoMuaSam(DanhMucCoSoMuaSamFormData param)
        {
            try
            {
                int id = -1;

                var token = _config.GetValue<string>("CSDLDuLichToken");
                var baseurl = _config.GetValue<string>("SoHoaCapNhatAddress");

                var data = new DanhMucCoSoMuaSamFormData
                {
                    serviceid = param.serviceid,
                    cosoid = param.cosoid,
                    idloaihinh = param.idloaihinh,
                    tenloaihinh = param.tenloaihinh,
                };

                var client = new RestClient(baseurl);
                client.Timeout = -1;

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("token", token);
                request.AlwaysMultipartFormData = true;

                request.AddParameter("serviceid", data.serviceid);
                request.AddParameter("cosoid", data.cosoid);
                request.AddParameter("idloaihinh", data.idloaihinh);
                request.AddParameter("tenloaihinh", data.tenloaihinh);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK) && response != null)
                {
                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhMucResponse>(response.Content);
                    id = Convert.ToInt32(dataDaXuLy.data);
                }

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> AddOrEditLuHanh(DanhMucLuHanhFormData param)
        {
            try
            {
                int id = -1;

                var token = _config.GetValue<string>("CSDLDuLichToken");
                var baseurl = _config.GetValue<string>("SoHoaCapNhatAddress");

                var data = new DanhMucLuHanhFormData
                {
                    serviceid = param.serviceid,
                    phanloaiid = param.phanloaiid,
                    idloaihinh = param.idloaihinh,
                    tenloaihinh = param.tenloaihinh,
                };

                var client = new RestClient(baseurl);
                client.Timeout = -1;

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("token", token);
                request.AlwaysMultipartFormData = true;

                request.AddParameter("serviceid", data.serviceid);
                request.AddParameter("phanloaiid", data.phanloaiid);
                request.AddParameter("idloaihinh", data.idloaihinh);
                request.AddParameter("tenloaihinh", data.tenloaihinh);

                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK) && response != null)
                {
                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhMucResponse>(response.Content);
                    id = Convert.ToInt32(dataDaXuLy.data);
                }

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
