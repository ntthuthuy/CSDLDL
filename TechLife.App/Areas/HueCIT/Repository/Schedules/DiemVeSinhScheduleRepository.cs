using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model.HueCIT;
using TechLife.Service.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class DiemVeSinhScheduleRepository : IDiemVeSinhScheduleRepository
    {
        private readonly int PERPAGE = 10;
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;
        private readonly string SERVICE_ID = "+NZhaFPUhLE8BqhGzTJLMA==";

        private readonly IConfiguration _config;
        private readonly IDiemVeSinhDongBoService _diemVeSinhDongBoService;
        private readonly ILogger<DiemVeSinhScheduleRepository> _logger;
        public DiemVeSinhScheduleRepository(IConfiguration config,
                                            IDiemVeSinhDongBoService diemVeSinhDongBoService,
                                            ILogger<DiemVeSinhScheduleRepository> logger)
        {
            _config = config;
            _diemVeSinhDongBoService = diemVeSinhDongBoService;
            _logger = logger;
        }
        public async Task GetData()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var token = _config.GetValue<string>("CSDLDuLichToken");
                    var baseUrl = _config.GetValue<string>("SoHoaAddress");
                    httpClient.DefaultRequestHeaders.Add("token", token);

                    double totalrow, row;

                    DuLieuDuLichRequestBody body = new DuLieuDuLichRequestBody
                    {
                        serviceid = SERVICE_ID,
                        thamso = new Thamso { },
                        page = 1,
                        perpage = 1
                    };

                    var bodyJson = JsonConvert.SerializeObject(body);
                    HttpContent content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(baseUrl, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDiaDiemAnUongDongBo>(apiResponse);
                            totalrow = dataDaXuLy.totalrow;
                            row = Math.Round(totalrow / PERPAGE);
                        }
                        else
                        {
                            totalrow = 0;
                            row = 0;
                        }
                    }

                    if (totalrow > 0)
                    {
                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = SERVICE_ID,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDiemVeSinhDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        DiemVeSinhModel dvs = new DiemVeSinhModel
                                        {
                                            Ten = item.tendoituong,
                                            ViTri = item.vitri,
                                            DonVi = item.donvi,
                                            HienTrang = item.hientrang,
                                            GhiChu = item.ghichu,
                                            DiemVeSinhID = item.id,
                                            X = double.Parse(item.vd),
                                            Y = double.Parse(item.kd),
                                            IsStatus = false,
                                            IsDelete = false,
                                            NguonDongBo = NGUON_DONG_BO
                                        };

                                        var diemvesinh = await _diemVeSinhDongBoService.GetByDongBoId(dvs.DiemVeSinhID);
                                        if (diemvesinh != null)
                                        {
                                            dvs.Id = diemvesinh.Id;
                                            await _diemVeSinhDongBoService.Update(diemvesinh.Id, dvs);
                                        }
                                        else
                                        {
                                            await _diemVeSinhDongBoService.Create(dvs);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
