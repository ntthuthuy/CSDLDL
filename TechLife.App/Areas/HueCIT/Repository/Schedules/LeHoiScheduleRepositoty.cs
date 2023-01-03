using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums.HueCIT;
using TechLife.Service.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class LeHoiScheduleRepositoty : ILeHoiScheduleRepositoty
    {
        private readonly int PERPAGE = 10;
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;
        private readonly string SERVICE_ID_LE_HOI = "o5BamQhdjPmL8xzOQXFvpQ==";

        private readonly IConfiguration _config;
        private readonly ILeHoiRepository _leHoiRepository;
        private readonly IFileDongBoRepository _fileDongBoRepository;
        private readonly IFileUploaderDongBoService _fileUploaderDongBoService;
        private readonly ILogger<LeHoiScheduleRepositoty> _logger;
        public LeHoiScheduleRepositoty(IConfiguration config,
                                       ILeHoiRepository leHoiRepository,
                                       IFileDongBoRepository fileDongBoRepository,
                                       IFileUploaderDongBoService fileUploaderDongBoService,
                                       ILogger<LeHoiScheduleRepositoty> logger)
        {
            _config = config;
            _leHoiRepository = leHoiRepository;
            _fileDongBoRepository = fileDongBoRepository;
            _fileUploaderDongBoService = fileUploaderDongBoService;
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
                        serviceid = SERVICE_ID_LE_HOI,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachLeHoiDongBo>(apiResponse);

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
                        bool isDSLeHoi = (await _leHoiRepository.Gets(new LeHoiRequest { NguonDongBo = NGUON_DONG_BO })).Any() ? true : false;

                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = SERVICE_ID_LE_HOI,
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachLeHoiDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0 && dataLoop.data.Count() > 0)
                                    {
                                        foreach (var item in dataLoop.data.ToList())
                                        {
                                            // Xử lý ngày 
                                            DateTime? moCua = null;
                                            DateTime? dongCua = null;
                                            try
                                            {
                                                moCua = DateTime.Parse(item.mocua);
                                            }
                                            catch (Exception ex)
                                            {
                                                moCua = null;
                                            }

                                            try
                                            {
                                                dongCua = DateTime.Parse(item.dongcua);
                                            }
                                            catch (Exception ex)
                                            {
                                                dongCua = null;
                                            }

                                            // Dữ liệu lễ hội đồng bộ
                                            LeHoi lehoiNew = null;
                                            LeHoi leHoi = new LeHoi
                                            {
                                                TenLeHoi = item.tendoituong,
                                                Loai = 2,
                                                Cap = 0,
                                                NoiDung = item.mota,
                                                X = float.Parse(item.vido),
                                                Y = float.Parse(item.kinhdo),
                                                DiaDiem = item.diachi,
                                                BatDau = moCua,
                                                KetThuc = dongCua,
                                                LeHoiID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                            };

                                            // Kiểm tra danh sách lễ hội
                                            // True: cập nhật
                                            // False: thêm mới
                                            if (isDSLeHoi)
                                            {
                                                var lh = await _leHoiRepository.GetByLeHoiID(leHoi.LeHoiID);
                                                if (lh != null)
                                                {
                                                    leHoi.ID = lh.ID;
                                                    lehoiNew = await _leHoiRepository.Edit(leHoi);
                                                }
                                                else
                                                {
                                                    lehoiNew = await _leHoiRepository.Add(leHoi);
                                                }
                                            }
                                            else
                                            {
                                                lehoiNew = await _leHoiRepository.Add(leHoi);
                                            }

                                            // Lưu hình ảnh lễ hội
                                            if (!String.IsNullOrEmpty(item.anh) && lehoiNew != null)
                                            {
                                                var findImage = await _fileDongBoRepository.GetsDongBo("CN_LeHoi", lehoiNew.ID.ToString(), NGUON_DONG_BO);
                                                if (findImage != null)
                                                {
                                                    await _fileDongBoRepository.DeleteWithparentDongBo("CN_LeHoi", lehoiNew.ID.ToString(), NGUON_DONG_BO);
                                                }
                                                await _fileUploaderDongBoService.UploadFileByUrl(item.anh, "CN_LeHoi", lehoiNew.ID.ToString(), NGUON_DONG_BO);
                                            }
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
