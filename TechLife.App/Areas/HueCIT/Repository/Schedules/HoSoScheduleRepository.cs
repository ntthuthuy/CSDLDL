using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HueCIT;
using TechLife.Service.HueCIT;
using X.PagedList;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class HoSoScheduleRepository : IHoSoScheduleRepository
    {
        private readonly int PERPAGE = 50;
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;
        private readonly int ID_DANH_MUC_LICH_SU = 12;

        private readonly string _langId = "vi";
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private const string IMG_TYPE = "bmp, png, jpg, jpeg, gif, webp";
        private const string MEDIA_TYPE = "mp4, mov, wmv, avi, flv, mkv, 3gp, mp3, wav";
        private const string DOC_TYPE = "doc, docx, xls, xlsx, pdf";

        private readonly IHoSoService _hoSoService;
        private readonly IHoSoDongBoService _hoSoDongBoService;
        private readonly IDiaPhuongDongBoService _diaPhuongDongBoService;
        private readonly IConfiguration _config;
        private readonly ILogger<HoSoScheduleRepository> _logger;
        public HoSoScheduleRepository(IConfiguration config
                                    , IHoSoService hoSoService
                                    , IHoSoDongBoService hoSoDongBoService
                                    , IDiaPhuongDongBoService diaPhuongDongBoService
                                    , ILogger<HoSoScheduleRepository> logger)
        {
            _config = config;
            _hoSoService = hoSoService;
            _hoSoDongBoService = hoSoDongBoService;
            _diaPhuongDongBoService = diaPhuongDongBoService;
            _logger = logger;
        }

        public async Task GetDataVcgt()
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
                        serviceid = "Nl6ShhLlYcWSnDhAqBvexA==",
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
                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = "Nl6ShhLlYcWSnDhAqBvexA==",
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachVcgtDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.tendoituong))
                                        {
                                            continue;
                                        }

                                        DuLieuDuLichModel hoSoNew = null;
                                        FileUploadModel image = null;

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendoituong,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.KhuVuiChoi,
                                            LoaiHinhId = 0,
                                            SoNha = item.diachi,
                                            ToaDoX = float.Parse(item.vido),
                                            ToaDoY = float.Parse(item.kinhdo),
                                            SoDienThoai = item.sdt,
                                            Email = item.email,
                                            Website = item.website,
                                            GioDongCua = item.dongcua,
                                            GioMoCua = item.mocua,
                                            GiaThamKhaoTu = item.giatu,
                                            GiaThamKhaoDen = item.giaden,
                                            GioiThieu = item.mota,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };

                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        if (!String.IsNullOrEmpty(item.anh))
                                        {
                                            image = new FileUploadModel
                                            {
                                                FileName = item.anh.Substring(item.anh.LastIndexOf('/') + 1),
                                                FileUrl = item.anh,
                                                Type = LoaiFile.hosodulich.ToString(),
                                                IsImage = true,
                                                Id = hoSoNew.Id,
                                                IsStatus = true,
                                                NgayTao = DateTime.Now,
                                                FileType = CheckFileType(item.anh.Substring(item.anh.LastIndexOf('/') + 1)),
                                                NguonDongBo = NGUON_DONG_BO
                                            };

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).FirstOrDefault();

                                                if (findImage != null)
                                                {
                                                    await _hoSoDongBoService.EditFile(findImage.FileId, image);
                                                }
                                                else
                                                {
                                                    await _hoSoDongBoService.AddFile(image);
                                                }
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
        public async Task GetDataCongTyVanChuyen()
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
                        serviceid = "QA6CVkYU96xCXwOIs9eOAA==",
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachCongTyVanChuyen>(apiResponse);
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
                                serviceid = "QA6CVkYU96xCXwOIs9eOAA==",
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachCongTyVanChuyen>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.tendoituong))
                                        {
                                            continue;
                                        }

                                        FileUploadModel image = null;
                                        DuLieuDuLichModel hoSoNew = null;

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendoituong,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.VanChuyen,
                                            LoaiHinhId = (await _hoSoDongBoService.LoaiHinhDongBo(new LoaiHinhDB { LinhVucId = (int)LinhVucKinhDoanh.VanChuyen, Ten = item.loaihinh })),
                                            SoNha = item.diachi,
                                            ToaDoX = float.Parse(item.vido),
                                            ToaDoY = float.Parse(item.kinhdo),
                                            SoDienThoai = item.sdt,
                                            Email = item.email,
                                            Website = item.website,
                                            GioDongCua = item.dongcua,
                                            GioMoCua = item.mocua,
                                            GiaThamKhaoTu = item.giatu,
                                            GiaThamKhaoDen = item.giaden,
                                            GioiThieu = item.mota,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };

                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        if (!String.IsNullOrEmpty(item.anh))
                                        {
                                            image = new FileUploadModel
                                            {
                                                FileId = -1,
                                                FileName = item.anh.Substring(item.anh.LastIndexOf('/') + 1),
                                                FileUrl = item.anh,
                                                Type = LoaiFile.hosodulich.ToString(),
                                                IsImage = true,
                                                Id = hoSoNew.Id,
                                                IsStatus = true,
                                                NgayTao = DateTime.Now,
                                                FileType = CheckFileType(item.anh.Substring(item.anh.LastIndexOf('/') + 1)),
                                                NguonDongBo = NGUON_DONG_BO
                                            };

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).FirstOrDefault();

                                                if (findImage != null)
                                                {
                                                    await _hoSoDongBoService.EditFile(findImage.FileId, image);
                                                }
                                                else
                                                {
                                                    await _hoSoDongBoService.AddFile(image);
                                                }
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
        public async Task GetDataCongTyLuHanh()
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
                        serviceid = "uHfVdBTTartRsJwVh/e2MQ==",
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachCongTyLuHanhDongBo>(apiResponse);
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
                                serviceid = "uHfVdBTTartRsJwVh/e2MQ==",
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachCongTyLuHanhDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.ten))
                                        {
                                            continue;
                                        }

                                        DuLieuDuLichModel hoSoNew = null;

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.ten,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.LuHanh,
                                            LoaiHinhId = 0,
                                            SoNha = item.diachi,
                                            ToaDoX = float.Parse(item.vd),
                                            ToaDoY = float.Parse(item.kd),
                                            SoDienThoai = item.sdt,
                                            Fax = item.fax,
                                            Email = item.email,
                                            HoTenNguoiDaiDien = item.truongbplh,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };

                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
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
        public async Task GetDataDiemDuLich()
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
                        serviceid = "UO86oYo/xPvv5DCkOE1iRg==",
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDiemDuLichDongBo>(apiResponse);
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
                                serviceid = "UO86oYo/xPvv5DCkOE1iRg==",
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDiemDuLichDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.tendoituong))
                                        {
                                            continue;
                                        }

                                        FileUploadModel image = null;
                                        DuLieuDuLichModel hoSoNew = null;

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendoituong,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.DiemDuLich,
                                            LoaiHinhId = (item.nhomloaihinh.Trim().ToUpper().Equals("DI TÍCH")) ? ID_DANH_MUC_LICH_SU : 0,
                                            SoNha = item.diachi,
                                            ToaDoX = (String.IsNullOrEmpty(item.vido) ? -1 : double.Parse(item.vido)),
                                            ToaDoY = (String.IsNullOrEmpty(item.kinhdo) ? -1 : double.Parse(item.kinhdo)),
                                            SoDienThoai = item.sdt,
                                            Email = item.email,
                                            Website = item.website,
                                            GioDongCua = item.dongcua,
                                            GioMoCua = item.mocua,
                                            GiaThamKhaoTu = item.giathamkhaotu,
                                            GiaThamKhaoDen = item.giathamkhaoden,
                                            GioiThieu = item.mota,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };


                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        if (!String.IsNullOrEmpty(item.anh))
                                        {
                                            image = new FileUploadModel
                                            {
                                                FileName = item.anh.Substring(item.anh.LastIndexOf('/') + 1),
                                                FileUrl = item.anh,
                                                Type = LoaiFile.hosodulich.ToString(),
                                                IsImage = true,
                                                IsStatus = true,
                                                NgayTao = DateTime.Now,
                                                Id = hoSoNew.Id,
                                                FileType = CheckFileType(item.anh.Substring(item.anh.LastIndexOf('/') + 1)),
                                                NguonDongBo = NGUON_DONG_BO
                                            };

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).FirstOrDefault();

                                                if (findImage != null)
                                                {
                                                    await _hoSoDongBoService.EditFile(findImage.FileId, image);
                                                }
                                                else
                                                {
                                                    await _hoSoDongBoService.AddFile(image);
                                                }
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
        public async Task GetDataDiaDiemAnUong()
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
                        serviceid = "sK5m3WdlgW53U6+mvOScKA==",
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
                                serviceid = "sK5m3WdlgW53U6+mvOScKA==",
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDiaDiemAnUongDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.tendoituong))
                                        {
                                            continue;
                                        }

                                        FileUploadModel image = null;
                                        DuLieuDuLichModel hoSoNew = null;

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendoituong,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.NhaHang,
                                            LoaiHinhId = 0,
                                            SoNha = item.diachi,
                                            ToaDoX = float.Parse(item.vido),
                                            ToaDoY = float.Parse(item.kinhdo),
                                            SoDienThoai = item.sdt,
                                            Email = item.email,
                                            Website = item.website,
                                            GioDongCua = item.dongcua,
                                            GioMoCua = item.mocua,
                                            GiaThamKhaoTu = item.giathamkhaotu,
                                            GiaThamKhaoDen = item.giathamkhaoden,
                                            GioiThieu = item.mota,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };


                                        var findHoSo = await _hoSoDongBoService.GetHoSoByTen(hoso.LinhVucKinhDoanhId, hoso.Ten.Trim());
                                        if (findHoSo != null)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        if (!String.IsNullOrEmpty(item.anh))
                                        {
                                            image = new FileUploadModel
                                            {
                                                FileId = -1,
                                                FileName = item.anh.Substring(item.anh.LastIndexOf('/') + 1),
                                                FileUrl = item.anh,
                                                Type = LoaiFile.hosodulich.ToString(),
                                                IsImage = true,
                                                Id = hoSoNew.Id,
                                                IsStatus = true,
                                                NgayTao = DateTime.Now,
                                                FileType = CheckFileType(item.anh.Substring(item.anh.LastIndexOf('/') + 1)),
                                                NguonDongBo = NGUON_DONG_BO
                                            };

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).FirstOrDefault();

                                                if (findImage != null)
                                                {
                                                    await _hoSoDongBoService.EditFile(findImage.FileId, image);
                                                }
                                                else
                                                {
                                                    await _hoSoDongBoService.AddFile(image);
                                                }
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
        public async Task GetDataCoSoMuaSam()
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
                        serviceid = "1BqJIsH72udGuL1XT/FjGw==",
                        thamso = new Thamso
                        {
                            tukhoa = "",
                            loaidiadiem = 0,
                            trangthaipheduyet = 2
                        },
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachCoSoMuaSamDongBo>(apiResponse);

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
                                serviceid = "1BqJIsH72udGuL1XT/FjGw==",
                                thamso = new Thamso
                                {
                                    tukhoa = "",
                                    loaidiadiem = 0,
                                    trangthaipheduyet = 2
                                },
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
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachCoSoMuaSamDongBo>(apiRes);

                                    foreach (var item in dataLoop.data.ToList())
                                    {
                                        if (String.IsNullOrEmpty(item.tendiadiem))
                                        {
                                            continue;
                                        }

                                        DuLieuDuLichModel hoSoNew = null;
                                        List<FileUploadModel> imagesReq = new List<FileUploadModel>();

                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendiadiem,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.MuaSam,
                                            LoaiHinhId = await _hoSoDongBoService.LoaiHinhDongBo(new LoaiHinhDB { LinhVucId = (int)LinhVucKinhDoanh.MuaSam, DongBoID = item.loaidiadiem, NguonDongBo = NGUON_DONG_BO }),
                                            SoNha = item.diachi,
                                            ToaDoX = item.vido,
                                            ToaDoY = item.kinhdo,
                                            SoDienThoai = item.lienhe,
                                            GioMoCua = item.thoigianmocua,
                                            GioiThieu = item.gioithieu,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            IsStatus = true,
                                            IsDelete = false,
                                            MaSoThue = item.masothue,
                                            QuanHuyenId = item.maquanhuyen ?? 0,
                                            QuanHuyen = item.tenmaquanhuyen ?? "",
                                            PhuongXaId = item.maphuongxa ?? 0,
                                            PhuongXa = item.tenmaphuongxa ?? ""
                                        };

                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        if (item.stttt_luoihinhanhdiadiemmuasam_dulich.Any())
                                        {
                                            item.stttt_luoihinhanhdiadiemmuasam_dulich.ForEach(x =>
                                            {
                                                imagesReq.Add(new FileUploadModel
                                                {
                                                    FileId = -1,
                                                    FileName = x.hinhanhdinhkem.Substring(x.hinhanhdinhkem.LastIndexOf('/') + 1),
                                                    FileUrl = x.hinhanhdinhkem,
                                                    Type = LoaiFile.hosodulich.ToString(),
                                                    IsImage = true,
                                                    Id = hoSoNew.Id,
                                                    IsStatus = true,
                                                    NgayTao = DateTime.Now,
                                                    FileType = CheckFileType(x.hinhanhdinhkem.Substring(x.hinhanhdinhkem.LastIndexOf('/') + 1)),
                                                    NguonDongBo = NGUON_DONG_BO
                                                });
                                            });

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).Select(x => x.FileId).ToList();

                                                if (findImage.Any())
                                                {
                                                    await _hoSoDongBoService.DeleteFiles(findImage);
                                                }

                                                await _hoSoDongBoService.AddFiles(imagesReq);
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
        public async Task GetDataDiSanVanHoa()
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
                        serviceid = "8a6ecGjayr99x4YuPJkzYw==",
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDiSanVanHoaDongBo>(apiResponse);
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
                                serviceid = "8a6ecGjayr99x4YuPJkzYw==",
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                var apiRes = await res.Content.ReadAsStringAsync();
                                var dataLoop = JsonConvert.DeserializeObject<DanhSachDiSanVanHoaDongBo>(apiRes);

                                foreach (var item in dataLoop.data)
                                {
                                    if (String.IsNullOrEmpty(item.tendisan))
                                    {
                                        continue;
                                    }

                                    FileUploadModel image = null;
                                    DuLieuDuLichModel hoSoNew = null;
                                    DiaPhuongModelDongBo quanhuyen = null, phuongxa = null;
                                    int quanhuyenId = 0, phuongxaId = 0;

                                    // Find loại hình Id
                                    var loaihinhId = await _hoSoDongBoService.LoaiHinhDongBo(new LoaiHinhDB
                                    {
                                        LinhVucId = (int)LinhVucKinhDoanh.DiSanVanHoa,
                                        DongBoID = item.phanloaidisan,
                                        NguonDongBo = NGUON_DONG_BO
                                    });

                                    if (loaihinhId > 0)
                                    {
                                        // Cập nhật đồng bộ ID địa phương
                                        // Quận huyện
                                        if (!String.IsNullOrEmpty(item.tenhuyenthithanh))
                                        {
                                            quanhuyen = await _diaPhuongDongBoService.GetByName(item.tenhuyenthithanh);
                                            if (quanhuyen != null)
                                            {
                                                await _diaPhuongDongBoService.Edit(quanhuyen.Id, new DiaPhuongModelDongBo
                                                {
                                                    DongBoID = item.huyenthithanh,
                                                    NguonDongBo = NGUON_DONG_BO,
                                                });
                                                // Get ID quận huyện đồng bộ
                                                quanhuyenId = (await _diaPhuongDongBoService.GetByDongBo(item.huyenthithanh, NGUON_DONG_BO)).Id;
                                            }
                                        }
                                        // Phường xã
                                        if (!String.IsNullOrEmpty(item.tenxaphuong))
                                        {
                                            phuongxa = await _diaPhuongDongBoService.GetByName(item.tenxaphuong);
                                            if (phuongxa != null)
                                            {
                                                await _diaPhuongDongBoService.Edit(phuongxa.Id, new DiaPhuongModelDongBo
                                                {
                                                    DongBoID = item.xaphuong,
                                                    NguonDongBo = NGUON_DONG_BO,
                                                });
                                                // Get ID phường xã đồng bộ
                                                phuongxaId = (await _diaPhuongDongBoService.GetByDongBo(item.xaphuong, NGUON_DONG_BO)).Id;
                                            }
                                        }

                                        // Thêm mới và cập nhật di sản văn hóa
                                        DuLieuDuLichModel hoso = new DuLieuDuLichModel
                                        {
                                            Ten = item.tendisan,
                                            LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.DiSanVanHoa,
                                            LoaiHinhId = loaihinhId,
                                            ToaDoX = item.vido,
                                            ToaDoY = item.kinhdo,
                                            GioiThieu = item.thongtinchitiet,
                                            NguonDongBo = NGUON_DONG_BO,
                                            DongBoID = item.id,
                                            SoNha = item.diachi,
                                            DuongPho = item.tenthonto,
                                            PhuongXaId = phuongxaId,
                                            QuanHuyenId = quanhuyenId,
                                            IsStatus = true,
                                            IsDelete = false,
                                        };

                                        var findHoSo = await _hoSoDongBoService.GetHoSoByDongBo(hoso.LinhVucKinhDoanhId, hoso.DongBoID);
                                        if (findHoSo != null)
                                        {
                                            hoSoNew = await _hoSoDongBoService.Edit(findHoSo.Id, hoso);
                                        }
                                        else
                                        {
                                            hoSoNew = await _hoSoDongBoService.Add(_langId, hoso);
                                        }


                                        // Add Image
                                        string anhdaidien = item.anhdaidien;
                                        if (!String.IsNullOrEmpty(anhdaidien))
                                        {
                                            image = new FileUploadModel
                                            {
                                                FileName = anhdaidien.Substring(anhdaidien.LastIndexOf('/') + 1),
                                                FileUrl = anhdaidien,
                                                Type = LoaiFile.hosodulich.ToString(),
                                                IsImage = true,
                                                Id = hoSoNew.Id,
                                                IsStatus = true,
                                                NgayTao = DateTime.Now,
                                                FileType = CheckFileType(anhdaidien.Substring(anhdaidien.LastIndexOf('/') + 1)),
                                                NguonDongBo = NGUON_DONG_BO
                                            };

                                            if (hoSoNew != null)
                                            {
                                                var findImage = (await _hoSoDongBoService.GetFileDongBoByHoSoId(hoSoNew.Id, LoaiFile.hosodulich.ToString(), NGUON_DONG_BO)).FirstOrDefault();

                                                if (findImage != null)
                                                {
                                                    await _hoSoDongBoService.EditFile(findImage.FileId, image);
                                                }
                                                else
                                                {
                                                    await _hoSoDongBoService.AddFile(image);
                                                }
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
        private int CheckFileType(string name)
        {
            var FileExtension = name.Substring(name.LastIndexOf('.') + 1).ToLower();
            int ft = (int)FileType.Other;
            if (IMG_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Img;
            }
            else if (MEDIA_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Media;
            }
            else if (DOC_TYPE.Contains(FileExtension))
            {
                ft = (int)FileType.Doc;
            }

            return ft;
        }
    }
}
