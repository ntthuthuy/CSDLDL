using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Data;
using TechLife.Data.Repositories;
using TechLife.Model.HueCIT;
using TechLife.Service.HueCIT;
using LoaiDiaDiemAnUong = TechLife.App.Areas.HueCIT.Models.LoaiDiaDiemAnUong;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class DanhMucScheduleRepository : IDanhMucScheduleRepository
    {
        private readonly int PERPAGE = 50;
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.SoHoa;

        private readonly int LINH_VUC_ID_DIEM_DU_LICH = (int)LinhVucKinhDoanh.DiemDuLich;
        private readonly int LINH_VUC_ID_LU_HANH = (int)LinhVucKinhDoanh.LuHanh;
        private readonly int LINH_VUC_ID_VAN_CHUYEN = (int)LinhVucKinhDoanh.VanChuyen;
        private readonly int LINH_VUC_ID_KHU_VUI_CHOI = (int)LinhVucKinhDoanh.KhuVuiChoi;
        private readonly int LINH_VUC_ID_THE_THAO = (int)LinhVucKinhDoanh.TheThao;
        private readonly int LINH_VUC_ID_CSSK = (int)LinhVucKinhDoanh.CSSK;

        private readonly string SERVICE_ID_DIA_DIEM_AN_UONG = "4BTYSbqakdg1A7fWr9qa/Q==";
        private readonly string SERVICE_ID_DIEM_DU_LICH = "wMhgvtnwerKMAUlFfzNE4w==";
        private readonly string SERVICE_ID_CO_SO_MUA_SAM = "yhX5adxP6hVfMT6JEPZsxg==";
        private readonly string SERVICE_ID_LU_HANH = "1uDmGjtxCbkEyePdF1lYVQ==";
        private readonly string SERVICE_ID_VAN_CHUYEN = "3RyYOuKCRjnstethwapQKw==";
        private readonly string SERVICE_ID_KHU_VUI_CHOI = "ukzX6qDQElFQ8mpP3E1bPw==";
        private readonly string SERVICE_ID_THE_THAO = "CYe596Eym68xxw07d+nVOQ==";
        private readonly string SERVICE_ID_CSSK = "ZpiYQL99yu80qkCJOdEuHQ==";
        private readonly string SERVICE_ID_AM_THUC = "7NL8tqxUups3P0nh9xI2+w==";
        private readonly string SERVICE_ID_LE_HOI = "Trgt9xxv4dco836j35ROgQ==";
        private readonly string SERVICE_ID_DIEM_GIAO_DICH = "NS5lpcoNzxyB2RLqPsVacw==";

        private readonly IDanhMucDongBoService _danhMucDongBoService;
        private readonly IDanhMucDongBoRepository _danhMucDongBoRepository;
        private readonly IConfiguration _config;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly ILoaiDichVuDongBoService _loaiDichVuDongBoService;

        public DanhMucScheduleRepository(IConfiguration config,
                                         IDanhMucDongBoService danhMucDongBoService,
                                         IDanhMucRepository danhMucRepository,
                                         ILoaiDichVuDongBoService loaiDichVuDongBoService,
                                         IDanhMucDongBoRepository danhMucDongBoRepository)
        {
            _config = config;
            _danhMucDongBoService = danhMucDongBoService;
            _danhMucRepository = danhMucRepository;
            _loaiDichVuDongBoService = loaiDichVuDongBoService;
            _danhMucDongBoRepository = danhMucDongBoRepository;
        }

        // Hồ sơ
        public async Task GetDataDanhMucDiemDuLich()
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
                        serviceid = SERVICE_ID_DIEM_DU_LICH,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_DIEM_DU_LICH,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_DIEM_DU_LICH,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_DIEM_DU_LICH, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_DIEM_DU_LICH,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }
        public async Task GetDataDanhMucLuHanh()
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
                        serviceid = SERVICE_ID_LU_HANH,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_LU_HANH,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_LU_HANH,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_LU_HANH, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_LU_HANH,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }
        public async Task GetDataDanhMucVanChuyen()
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
                        serviceid = SERVICE_ID_VAN_CHUYEN,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_VAN_CHUYEN,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {

                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_VAN_CHUYEN,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_VAN_CHUYEN, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_VAN_CHUYEN,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }
        public async Task GetDataDanhMucKhuVuiChoi()
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
                        serviceid = SERVICE_ID_KHU_VUI_CHOI,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_KHU_VUI_CHOI,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            // DANH MỤC TỪ SỐ HÓA
                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_KHU_VUI_CHOI,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_KHU_VUI_CHOI, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_KHU_VUI_CHOI,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }
        public async Task GetDataDanhMucTheThao()
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
                        serviceid = SERVICE_ID_THE_THAO,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_THE_THAO,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_THE_THAO,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_THE_THAO, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_THE_THAO,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }
        public async Task GetDataDanhCSSK()
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
                        serviceid = SERVICE_ID_CSSK,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_CSSK,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO 
                                            DanhMucModel danhMucMoi = null;

                                            DanhMucModel danhmuc = new DanhMucModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                LoaiId = LINH_VUC_ID_CSSK,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            // FIND DANH MỤC THEO [LOẠI ID] && [DONGBOID]
                                            var findDanhMuc = await _danhMucDongBoService.GetByDongBoID(LINH_VUC_ID_CSSK, danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _danhMucDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                // THÊM MỚI VÀO HUECIT
                                                danhMucMoi = await _danhMucDongBoService.Create(danhmuc);
                                            }

                                            if (danhMucMoi != null)
                                            {
                                                // CẬP NHẬT ID HUECIT LÊN SỐ HÓA
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_CSSK,
                                                    eformid = danhmuc.DongBoID.ToString(), // ID SO HOA
                                                    idloaihinh = danhMucMoi.Id.ToString(), // ID HUECIT
                                                    tenloaihinh = danhMucMoi.Ten
                                                });
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
                throw ex;
            }
        }

        public async Task GetDataDanhMucCoSoMuaSam()
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
                        serviceid = SERVICE_ID_CO_SO_MUA_SAM,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_CO_SO_MUA_SAM,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            LoaiDichVu danhMucMoi = null;

                                            LoaiDichVu danhmuc = new LoaiDichVu
                                            {
                                                //Id = item.idloaihinh,
                                                TenLoai = item.tenloaihinh,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                                IsStatus = true,
                                            };

                                            var findDanhMuc = await _loaiDichVuDongBoService.GetByDongBoID(danhmuc.DongBoID);
                                            if (findDanhMuc != null)
                                            {
                                                danhmuc.Id = findDanhMuc.Id;
                                                danhMucMoi = await _loaiDichVuDongBoService.Update(findDanhMuc.Id, danhmuc);
                                            }
                                            else
                                            {
                                                danhMucMoi = await _loaiDichVuDongBoService.Create(danhmuc);
                                            }

                                            if (danhMucMoi != null)
                                            {
                                                // CẬP NHẬT DANH MỤC LÊN SỐ HÓA
                                                await _danhMucDongBoRepository.AddOrEditCoSoMuaSam(new DanhMucCoSoMuaSamFormData
                                                {
                                                    serviceid = SERVICE_ID_CO_SO_MUA_SAM,
                                                    cosoid = danhmuc.DongBoID.ToString(),
                                                    idloaihinh = danhMucMoi.Id.ToString(),
                                                    tenloaihinh = danhMucMoi.TenLoai
                                                });
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
                throw ex;
            }
        }

        // Loại địa điểm ăn uống
        public async Task GetDataDanhMucDiaDiemAnUong()
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
                        serviceid = SERVICE_ID_DIA_DIEM_AN_UONG,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

                            totalrow = dataDaXuLy.totalrow;
                            row = Math.Round(totalrow / PERPAGE);
                        }
                        else
                        {
                            totalrow = 0;
                            row = 0;
                        }

                        if (totalrow > 0)
                        {
                            for (int i = 1; i <= row + 1; i++)
                            {
                                DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                                {
                                    serviceid = SERVICE_ID_DIA_DIEM_AN_UONG,
                                    thamso = new Thamso { },
                                    page = i,
                                    perpage = PERPAGE,
                                };

                                var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                                HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                                using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                                {
                                    if (res.IsSuccessStatusCode)
                                    {
                                        var apiRes = await res.Content.ReadAsStringAsync();
                                        var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                        if (dataLoop.totalrow > 0)
                                        {
                                            foreach (var item in dataLoop.data)
                                            {
                                                // DANH MỤC MỚI THÊM VÀO
                                                LoaiDiaDiemAnUong danhMucMoi = null;

                                                LoaiDiaDiemAnUong danhmuc = new LoaiDiaDiemAnUong
                                                {
                                                    //Id = item.idloaihinh,
                                                    TenLoai = item.tenloaihinh,
                                                    DongBoID = item.id,
                                                    NguonDongBo = NGUON_DONG_BO,
                                                    IsDelete = false,
                                                    IsStatus = true,
                                                };

                                                var findDanhMuc = await _danhMucRepository.GetByDongBoID(danhmuc.DongBoID);
                                                if (findDanhMuc != null)
                                                {
                                                    danhmuc.Id = findDanhMuc.ID;
                                                    danhMucMoi = await _danhMucRepository.UpdateLoaiDiaDiemAnUong(danhmuc);
                                                }
                                                else
                                                {
                                                    danhMucMoi = await _danhMucRepository.InsertLoaiDiaDiemAnUong(danhmuc);
                                                }

                                                // Thêm mới vào database số hóa
                                                if (danhMucMoi != null)
                                                {
                                                    var success = await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                    {
                                                        serviceid = SERVICE_ID_DIA_DIEM_AN_UONG,
                                                        eformid = danhmuc.DongBoID.ToString(),
                                                        idloaihinh = danhMucMoi.Id.ToString(),
                                                        tenloaihinh = danhMucMoi.TenLoai
                                                    });
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
                throw ex;
            }
        }

        // Loại ẩm thực
        public async Task GetDataDanhMucAmThuc()
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
                        serviceid = SERVICE_ID_AM_THUC,
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                        var findDSDanhMuc = await _danhMucRepository.GetsLoaiAmThuc();

                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = SERVICE_ID_AM_THUC,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            // DANH MỤC MỚI THÊM VÀO
                                            LoaiAmThuc danhMucMoi = null;

                                            LoaiAmThuc model = new LoaiAmThuc
                                            {
                                                //ID = item.idloaihinh,
                                                TenLoai = item.tenloaihinh,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                                IsDelete = false,
                                            };

                                            if (findDSDanhMuc.Any())
                                            {
                                                var findAmThuc = await _danhMucRepository.GetByDongBoID(model.DongBoID);
                                                if (findAmThuc != null)
                                                {
                                                    model.ID = findAmThuc.ID;
                                                    danhMucMoi = await _danhMucRepository.UpdateLoaiAmThuc(model);
                                                }
                                                else
                                                {
                                                    danhMucMoi = await _danhMucRepository.InsertLoaiAmThuc(model);
                                                }

                                                if (danhMucMoi != null)
                                                {
                                                    await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                    {
                                                        serviceid = SERVICE_ID_AM_THUC,
                                                        eformid = model.DongBoID.ToString(),
                                                        idloaihinh = danhMucMoi.ID.ToString(),
                                                        tenloaihinh = danhMucMoi.TenLoai,
                                                    });
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
                throw ex;
            }
        }

        // Loại lễ hội
        public async Task GetDataDanhMucLeHoi()
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                                serviceid = SERVICE_ID_LE_HOI,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            LoaiLeHoiModel danhMucMoi = null;

                                            LoaiLeHoiModel model = new LoaiLeHoiModel
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                            };

                                            var findAmThuc = await _danhMucRepository.GetLoaiLeHoiByDongBo(model.DongBoID);
                                            if (findAmThuc != null)
                                            {
                                                model.Id = findAmThuc.Id;
                                                danhMucMoi = await _danhMucRepository.EditLoaiLeHoi(model);
                                            }
                                            else
                                            {
                                                danhMucMoi = await _danhMucRepository.AddLoaiLeHoi(model);
                                            }

                                            // CẬP NHẬT DANH MỤC LÊN SỐ HÓA
                                            if (danhMucMoi != null)
                                            {
                                                await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                {
                                                    serviceid = SERVICE_ID_LE_HOI,
                                                    eformid = model.DongBoID.ToString(),
                                                    idloaihinh = danhMucMoi.Id.ToString(),
                                                    tenloaihinh = danhMucMoi.Ten,
                                                });
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
                throw ex;
            }
        }

        // Loại điểm giao dịch
        public async Task GetDataDanhMucDiemGiaoDich()
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
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiResponse);

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
                        var findDSDanhMuc = await _danhMucRepository.GetsLoaiDiemGiaoDich();

                        for (int i = 1; i <= row + 1; i++)
                        {
                            DuLieuDuLichRequestBody bodyLoop = new DuLieuDuLichRequestBody
                            {
                                serviceid = SERVICE_ID_DIEM_GIAO_DICH,
                                thamso = new Thamso { },
                                page = i,
                                perpage = PERPAGE,
                            };

                            var bodyJsonLoop = JsonConvert.SerializeObject(bodyLoop);
                            HttpContent contentLoop = new StringContent(bodyJsonLoop, Encoding.UTF8, "application/json");

                            using (var res = await httpClient.PostAsync(baseUrl, contentLoop))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    var apiRes = await res.Content.ReadAsStringAsync();
                                    var dataLoop = JsonConvert.DeserializeObject<DanhSachDanhMucHoSoDongBo>(apiRes);

                                    if (dataLoop.totalrow > 0)
                                    {
                                        foreach (var item in dataLoop.data)
                                        {
                                            LoaiDiemGiaoDich danhMucMoi = null;

                                            LoaiDiemGiaoDich model = new LoaiDiemGiaoDich
                                            {
                                                //Id = item.idloaihinh,
                                                Ten = item.tenloaihinh,
                                                DongBoID = item.id,
                                                NguonDongBo = NGUON_DONG_BO,
                                            };

                                            if (findDSDanhMuc.Any())
                                            {
                                                var findAmThuc = await _danhMucRepository.GetLoaiDiemGiaoDichByDongBo(model.DongBoID);
                                                if (findAmThuc != null)
                                                {
                                                    model.Id = findAmThuc.Id;
                                                    danhMucMoi = await _danhMucRepository.EditLoaiDiemGiaoDich(model);
                                                }
                                                else
                                                {
                                                    danhMucMoi = await _danhMucRepository.AddLoaiDiemGiaoDich(model);
                                                }

                                                if (danhMucMoi != null)
                                                {
                                                    // CẬP NHẬT ID DANH MỤC LÊN SỐ HÓA
                                                    await _danhMucDongBoRepository.AddOrEditEformid(new DanhMucEformidFormData
                                                    {
                                                        serviceid = SERVICE_ID_DIEM_GIAO_DICH,
                                                        eformid = model.DongBoID.ToString(),
                                                        idloaihinh = danhMucMoi.Id.ToString(),
                                                        tenloaihinh = danhMucMoi.Ten,
                                                    });
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
                throw ex;
            }
        }
    }
}
