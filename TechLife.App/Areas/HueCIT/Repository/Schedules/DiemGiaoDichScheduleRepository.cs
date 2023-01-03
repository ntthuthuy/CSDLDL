using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class DiemGiaoDichScheduleRepository : IDiemGiaoDichScheduleRepository
    {
        private const int PERPAGE = 10;
        private const int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;
        private readonly string SERVICE_ID_DIEM_GIAO_DICH = "lE83bvjq8tJG6pBCG+U0cQ==";

        private readonly IConfiguration _config;
        private readonly IDiemGiaoDichRepository _diemGiaoDichRepository;
        private readonly IFileUploaderDongBoService _fileUploaderDongBoService;
        private readonly IFileDongBoRepository _fileDongBoRepository;
        private readonly ILogger<DiemGiaoDichScheduleRepository> _logger;
        public DiemGiaoDichScheduleRepository(IConfiguration config
            , IDiemGiaoDichRepository diemGiaoDichRepository
            , IFileUploaderDongBoService fileUploaderDongBoService
            , IFileDongBoRepository fileDongBoRepository
            , ILogger<DiemGiaoDichScheduleRepository> logger)
        {
            _config = config;
            _diemGiaoDichRepository = diemGiaoDichRepository;
            _fileUploaderDongBoService = fileUploaderDongBoService;
            _fileDongBoRepository = fileDongBoRepository;
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
                        serviceid = SERVICE_ID_DIEM_GIAO_DICH,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachVcgtDongBo>(apiResponse);
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
                        var findDiemGiaoDich = await _diemGiaoDichRepository.Gets(new DiemGiaoDichRequest { });
                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = SERVICE_ID_DIEM_GIAO_DICH,
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
                                    var apiResponse = await res.Content.ReadAsStringAsync();
                                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDiemGiaoDichDongBo>(apiResponse);

                                    if (dataDaXuLy.totalrow > 0 && dataDaXuLy.data.Count() > 0)
                                    {
                                        DiemGiaoDich giaoDichNew;

                                        foreach (var item in dataDaXuLy.data.ToList())
                                        {
                                            DiemGiaoDich diemGiaoDich = new DiemGiaoDich
                                            {
                                                TenDiaDiem = item.tendoituong,
                                                DiaChi = item.diachi,
                                                Loai = 2,
                                                X = float.Parse(item.vido),
                                                Y = float.Parse(item.kinhdo),
                                                DiemGiaoDichID = item.id,
                                                GioPhucVu = "24/24"
                                            };

                                            if (findDiemGiaoDich.Any())
                                            {
                                                var dgd = await _diemGiaoDichRepository.GetByDiemGiaoDichID(diemGiaoDich.DiemGiaoDichID);
                                                if (dgd != null)
                                                {
                                                    diemGiaoDich.ID = dgd.ID;
                                                    giaoDichNew = await _diemGiaoDichRepository.Edit(diemGiaoDich);
                                                }
                                                else
                                                {
                                                    giaoDichNew = await _diemGiaoDichRepository.Add(diemGiaoDich);
                                                }
                                            }
                                            else
                                            {
                                                giaoDichNew = await _diemGiaoDichRepository.Add(diemGiaoDich);
                                            }

                                            if (!String.IsNullOrEmpty(item.anh) && giaoDichNew != null)
                                            {
                                                var findImage = await _fileDongBoRepository.GetsDongBo("DL_DiemGiaoDich", giaoDichNew.ID.ToString(), NGUON_DONG_BO);
                                                if (findImage != null)
                                                {
                                                    await _fileDongBoRepository.DeleteWithparentDongBo("DL_DiemGiaoDich", giaoDichNew.ID.ToString(), NGUON_DONG_BO);
                                                }
                                                await _fileUploaderDongBoService.UploadFileByUrl(item.anh, "DL_DiemGiaoDich", giaoDichNew.ID.ToString(), NGUON_DONG_BO);
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
