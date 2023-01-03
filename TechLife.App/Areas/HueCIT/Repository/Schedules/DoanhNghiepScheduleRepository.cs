using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository.Schedules
{
    public class DoanhNghiepScheduleRepository : ConnectDB, IDoanhNghiepScheduleRepository
    {
        private readonly int NGUON_DONG_BO = (int)NguonDongBo.HTTTDN;
        // Mặc định perpage của api doanh nghiệp là 20
        private readonly int PERPAGE = 20;

        private readonly SqlConnection _conn;
        private readonly IConfiguration _configuration;
        private readonly IDoanhNghiepTrangThaiRepository _doanhNghiepTrangThaiRepository;
        private readonly IDoanhNghiepLoaiHinhRepository _doanhNghiepLoaiHinhRepository;
        private readonly IDoanhNghiepNganhNgheRepository _doanhNghiepNganhNgheRepository;
        private readonly IDoanhNghiepLoaiVanBanRepository _doanhNghiepLoaiVanBanRepository;
        private readonly IDoanhNghiepRepository _doanhNghiepRepository;
        private readonly IDoanhNghiepVanBanRepository _doanhNghiepVanBanRepository;
        private readonly ILogger<DoanhNghiepScheduleRepository> _logger;
        public DoanhNghiepScheduleRepository(IConfiguration configuration,
                                             IDoanhNghiepTrangThaiRepository doanhNghiepTrangThaiRepository,
                                             IDoanhNghiepLoaiHinhRepository doanhNghiepLoaiHinhRepository,
                                             IDoanhNghiepNganhNgheRepository doanhNghiepNganhNgheRepository,
                                             IDoanhNghiepLoaiVanBanRepository doanhNghiepLoaiVanBanRepository,
                                             IDoanhNghiepVanBanRepository doanhNghiepVanBanRepository,
                                             IDoanhNghiepRepository doanhNghiepRepository,
                                             ILogger<DoanhNghiepScheduleRepository> logger) : base(configuration)
        {
            _conn = IConnectData();
            _configuration = configuration;
            _doanhNghiepTrangThaiRepository = doanhNghiepTrangThaiRepository;
            _doanhNghiepLoaiHinhRepository = doanhNghiepLoaiHinhRepository;
            _doanhNghiepLoaiVanBanRepository = doanhNghiepLoaiVanBanRepository;
            _doanhNghiepNganhNgheRepository = doanhNghiepNganhNgheRepository;
            _doanhNghiepVanBanRepository = doanhNghiepVanBanRepository;
            _doanhNghiepRepository = doanhNghiepRepository;
            _logger = logger;
        }

        // Đồng bộ danh mục doanh nghiệp
        public async Task GetDataLoaiHinh()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                    var url = $"{baseUrl}/GetsLoaiHinh";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepLoaiHinhDongBo>(apiResponse);
                            bool isAnyLoaiHinh = (await _doanhNghiepLoaiHinhRepository.Gets()).ToList().Count() > 0 ? true : false;
                            DoanhNghiepLoaiHinhTrinhDien doanhNghiepLoaiHinh = null;

                            if (dataDaXuLy.data.Any())
                            {
                                foreach (var item in dataDaXuLy.data)
                                {
                                    if (string.IsNullOrEmpty(item.loaiHinhID.ToString()) || string.IsNullOrEmpty(item.tenLoaiHinh.ToString()))
                                    {
                                        continue;
                                    }

                                    DoanhNghiepLoaiHinh loaihinh = new DoanhNghiepLoaiHinh()
                                    {
                                        LoaiHinh = item.tenLoaiHinh,
                                        DongBoID = item.loaiHinhID,
                                        NguonDongBo = NGUON_DONG_BO
                                    };

                                    if (isAnyLoaiHinh)
                                    {
                                        doanhNghiepLoaiHinh = await _doanhNghiepLoaiHinhRepository.GetByDongBoID(loaihinh.DongBoID);
                                        if (doanhNghiepLoaiHinh != null)
                                        {
                                            loaihinh.Id = doanhNghiepLoaiHinh.Id;
                                            await _doanhNghiepLoaiHinhRepository.Update(loaihinh);
                                        }
                                        else
                                        {
                                            await _doanhNghiepLoaiHinhRepository.Insert(loaihinh);
                                        }
                                    }
                                    else
                                    {
                                        await _doanhNghiepLoaiHinhRepository.Insert(loaihinh);
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
        public async Task GetDataLoaiVanBan()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                    var url = $"{baseUrl}/GetsLoaiVanBan";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepLoaiVanBanDongBo>(apiResponse);
                            bool IsAnyLoaiVanBan = (await _doanhNghiepLoaiVanBanRepository.Gets()).ToList().Count() > 0 ? true : false;

                            DoanhNghiepLoaiVanBanTrinhDien doanhNghiepLoaiVanBan = null;

                            if (dataDaXuLy.data.Any())
                            {
                                foreach (var item in dataDaXuLy.data)
                                {
                                    if (string.IsNullOrEmpty(item.loaiGiayPhepID.ToString()))
                                    {
                                        continue;
                                    }
                                    DoanhNghiepLoaiVanBan loaivanban = new DoanhNghiepLoaiVanBan()
                                    {
                                        TenLoai = item.loaiGiayPhep,
                                        DongBoID = item.loaiGiayPhepID,
                                        NguonDongBo = NGUON_DONG_BO
                                    };

                                    if (IsAnyLoaiVanBan)
                                    {
                                        doanhNghiepLoaiVanBan = await _doanhNghiepLoaiVanBanRepository.GetByDongBoID(loaivanban.DongBoID ?? -1);
                                        if (doanhNghiepLoaiVanBan != null)
                                        {
                                            loaivanban.Id = doanhNghiepLoaiVanBan.Id;
                                            await _doanhNghiepLoaiVanBanRepository.Update(loaivanban);
                                        }
                                        else
                                        {
                                            await _doanhNghiepLoaiVanBanRepository.Insert(loaivanban);
                                        }
                                    }
                                    else
                                    {
                                        await _doanhNghiepLoaiVanBanRepository.Insert(loaivanban);
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
        public async Task GetDataNganhNghe()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                    var url = $"{baseUrl}/GetsNganhNghe";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepNganhNgheDongBo>(apiResponse);
                            bool isAnyNganhNghe = (await _doanhNghiepNganhNgheRepository.Gets()).ToList().Count() > 0 ? true : false;
                            DoanhNghiepNganhNgheTrinhDien doanhNghiepNganhNghe = null;

                            if (dataDaXuLy.data.Any())
                            {
                                foreach (var item in dataDaXuLy.data)
                                {
                                    if (string.IsNullOrEmpty(item.maNganhNghe) || item.maNganhNghe == null)
                                    {
                                        continue;
                                    }

                                    DoanhNghiepNganhNghe nganhnghe = new DoanhNghiepNganhNghe()
                                    {
                                        DongBoID = item.maNganhNghe,
                                        TenNganhNghe = item.tenNganhNghe,
                                        NguonDongBo = NGUON_DONG_BO
                                    };

                                    if (isAnyNganhNghe)
                                    {
                                        doanhNghiepNganhNghe = await _doanhNghiepNganhNgheRepository.GetByDongBoID(nganhnghe.DongBoID);
                                        if (doanhNghiepNganhNghe != null)
                                        {
                                            nganhnghe.Id = doanhNghiepNganhNghe.Id;
                                            await _doanhNghiepNganhNgheRepository.Update(nganhnghe);
                                        }
                                        else
                                        {
                                            await _doanhNghiepNganhNgheRepository.Insert(nganhnghe);
                                        }
                                    }
                                    else
                                    {
                                        await _doanhNghiepNganhNgheRepository.Insert(nganhnghe);
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
        public async Task GetDataTrangThai()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                    var url = $"{baseUrl}/GetsTrangThai";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepTrangThaiDongBo>(apiResponse);
                            bool isAnyTrangThai = (await _doanhNghiepTrangThaiRepository.Gets()).ToList().Count() > 0 ? true : false;
                            DoanhNghiepTrangThaiTrinhDien doanhNghiepTrangThai = null;

                            if (dataDaXuLy.data.Any())
                            {
                                foreach (var item in dataDaXuLy.data)
                                {
                                    if (string.IsNullOrEmpty(item.trangThaiID.ToString()))
                                    {
                                        continue;
                                    }

                                    DoanhNghiepTrangThai trangthai = new DoanhNghiepTrangThai()
                                    {
                                        TrangThai = item.tenTrangThai,
                                        DongBoID = item.trangThaiID,
                                        NguonDongBo = NGUON_DONG_BO,
                                        TenClassCSS = "Class_DoanhNghiep_TrangThai_" + item.trangThaiID
                                    };

                                    if (isAnyTrangThai)
                                    {
                                        doanhNghiepTrangThai = await _doanhNghiepTrangThaiRepository.GetByDongBoID(trangthai.DongBoID ?? -1);
                                        if (doanhNghiepTrangThai != null)
                                        {
                                            trangthai.Id = doanhNghiepTrangThai.Id;
                                            await _doanhNghiepTrangThaiRepository.Update(trangthai);
                                        }
                                        else
                                        {
                                            await _doanhNghiepTrangThaiRepository.Insert(trangthai);
                                        }
                                    }
                                    else
                                    {
                                        await _doanhNghiepTrangThaiRepository.Insert(trangthai);
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


        // Đồng bộ dữ liệu doanh nghiệp
        public async Task GetDataDoanhNghiep()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    double totalrow = 0;
                    double row = 0;

                    var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                    var url = $"{baseUrl}/GetsDoanhNghiep?page=1";

                    using (var res = await httpClient.GetAsync(url))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            var apiRes = await res.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<DanhSachDoanhNghiepDongBo>(apiRes);

                            totalrow = data.data.totalRow;
                            row = Math.Round(totalrow / PERPAGE);
                        }
                    }

                    if (totalrow > 0)
                    {
                        for (var i = 1; i <= row; i++)
                        {
                            var urlLoop = $"{baseUrl}/GetsDoanhNghiep?page={i}";
                            using (var response = await httpClient.GetAsync(urlLoop))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var apiResponse = await response.Content.ReadAsStringAsync();
                                    var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepDongBo>(apiResponse);

                                    if (dataDaXuLy.data.data.Any())
                                    {
                                        foreach (var item in dataDaXuLy.data.data)
                                        {
                                            DoanhNghiep doanhNghiepNew = null;
                                            int maNganhNgheChinh = 0;
                                            int maTrangThai = 0;
                                            int maLoaiHinh = 0;

                                            // Ngành nghề chính
                                            if (!String.IsNullOrEmpty(item.maNganhNgheChinh))
                                            {
                                                var nganhNghe = await _doanhNghiepNganhNgheRepository.GetByDongBoID(item.maNganhNgheChinh);
                                                if (nganhNghe != null)
                                                {
                                                    maNganhNgheChinh = nganhNghe.Id;
                                                }
                                            }

                                            // Trạng thái doanh nghiệp
                                            if (!String.IsNullOrEmpty(item.trangThaiID.ToString()))
                                            {
                                                var trangthai = await _doanhNghiepTrangThaiRepository.GetByDongBoID(item.trangThaiID);
                                                if (trangthai != null)
                                                {
                                                    maTrangThai = trangthai.Id;
                                                }
                                            }

                                            // Loại hình doanh nghiệp
                                            if (!String.IsNullOrEmpty(item.loaiHinhID.ToString()))
                                            {
                                                var loaihinh = await _doanhNghiepLoaiHinhRepository.GetByDongBoID(item.loaiHinhID);
                                                if (loaihinh != null)
                                                {
                                                    maLoaiHinh = loaihinh.Id;
                                                }
                                            }

                                            // Xử lý ngày
                                            DateTime? ngayThanhLap = null;
                                            try
                                            {
                                                ngayThanhLap = DateTime.Parse(item.ngayCap);
                                            }
                                            catch (Exception ex)
                                            {
                                                ngayThanhLap = null;
                                            }

                                            // Doanh nghiệp
                                            DoanhNghiep dn = new DoanhNghiep()
                                            {
                                                DiaChi = item.diaChi,
                                                DienThoai = item.dienThoai,
                                                HopThu = item.email,
                                                NgayThanhLap = ngayThanhLap,
                                                NguoiDaiDien = item.nguoiDaiDien,
                                                TenDoanhNghiep = item.tenDoanhNghiep,
                                                TrangChu = item.web,
                                                X = item.lat,
                                                Y = item.Long,
                                                MaSoDoanhNghiep = item.maDoanhNghiep.Trim(),
                                                NguonDongBo = NGUON_DONG_BO,
                                                DongBoID = item.maDoanhNghiep.Trim(),
                                                IDPhuongXa = item.maPhuongXa,
                                                IDQuanHuyen = item.maQuanHuyen,
                                                MaNganhNgheChinh = maNganhNgheChinh,
                                                MaTrangThai = maTrangThai,
                                                MaLoaiHinh = maLoaiHinh
                                            };

                                            var finddoanhNghiep = await _doanhNghiepRepository.GetByDongBoID(dn.DongBoID);
                                            if (finddoanhNghiep != null)
                                            {
                                                dn.Id = finddoanhNghiep.Id;
                                                doanhNghiepNew = await _doanhNghiepRepository.Update(dn);
                                            }
                                            else
                                            {
                                                doanhNghiepNew = await _doanhNghiepRepository.Insert(dn);
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


        // Đồng bộ dữ liệu văn bản doanh nghiệp
        public async Task GetDataVanBan()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Danh sách doanh nghiệp đã có HueCIT
                    List<string> listDoanhNgiep = (await _doanhNghiepRepository.Gets(new DoanhNghiepRequest { })).Select(x => x.MaSoDoanhNghiep).ToList();

                    // Tính tổng số vòng lặp dựa theo tổng số dữ liệu api trả về 
                    double totalrow = listDoanhNgiep.Count / PERPAGE;

                    for (int i = 0; i < totalrow + 1; i++)
                    {
                        // Nối chuỗi các mã doanh nghiệp ngăn cách bằng dấu phẩy 
                        var result = listDoanhNgiep.Skip(i * PERPAGE).Take(PERPAGE);
                        string stringIds = String.Join(",", result);

                        // URL
                        var baseUrl = _configuration.GetValue<string>("DoanhNghiepAddress");
                        var url = $"{baseUrl}/GetsNhieuVanBan?id={stringIds}";

                        using (var response = await httpClient.GetAsync(url))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var apiResponse = await response.Content.ReadAsStringAsync();
                                var dataDaXuLy = JsonConvert.DeserializeObject<DanhSachDoanhNghiepVanBanDongBo>(apiResponse);

                                // Kiểm tra văn bản của doanh nghiệp theo mã doanh nghiệp có tồn tại
                                bool isAnyDoanhNghiepVanBan = (await _doanhNghiepVanBanRepository.GetsByMaDoanhNghiep(listDoanhNgiep[i])).ToList().Count() > 0;

                                // Kiểm tra api đồng bộ trả về có rỗng không
                                if (dataDaXuLy.data.Any())
                                {
                                    foreach (var item in dataDaXuLy.data)
                                    {
                                        // ID loại văn bản
                                        // Tìm kiếm loại hình văn bản đã đồng bộ [DongBoID] trên database HueCIT có tồn tại bằng mã doanh nghiệp
                                        // Nếu có thì trả về ID 
                                        int maLoai = 0;
                                        var getLoai = await _doanhNghiepLoaiHinhRepository.GetByDongBoID(item.loaiGiayPhepID);
                                        if (getLoai != null)
                                        {
                                            maLoai = getLoai.Id;
                                        }

                                        // ID doanh nghiệp
                                        // Tìm kiếm danh sách doanh nghiệp theo ID đồng bộ [DongBoID]
                                        // Nếu có thì lấy ID doanh nghiệp
                                        // Nếu không bỏ qua
                                        var maDoanhNghiep = await _doanhNghiepRepository.GetByDongBoID(item.maDoanhNghiep);
                                        if (maDoanhNghiep == null)
                                        {
                                            continue;
                                        }

                                        // Xử lý ngày
                                        DateTime? ngayHetHieuLuc = null;
                                        DateTime? ngayHieuLuc = null;
                                        try
                                        {
                                            ngayHetHieuLuc = DateTime.Parse(item.ngayHetHieuLuc);
                                        }
                                        catch (Exception ex)
                                        {
                                            ngayHetHieuLuc = null;
                                        }
                                        try
                                        {
                                            ngayHieuLuc = DateTime.Parse(item.ngayHieuLuc);
                                        }
                                        catch (Exception ex)
                                        {
                                            ngayHieuLuc = null;
                                        }


                                        // Model văn bản của doanh nghiệp
                                        DoanhNghiepVanBan vanban = new DoanhNghiepVanBan()
                                        {
                                            MaDoanhNghiep = maDoanhNghiep.Id,
                                            MaLoai = maLoai,
                                            NgayHetHieuLuc = ngayHetHieuLuc,
                                            NgayHieuLuc = ngayHieuLuc,
                                            SoKyHieu = item.soKyHieu,
                                            TenGiayPhep = item.tenGiayPhep,
                                            MaSoDoanhNghiep = item.maDoanhNghiep,
                                            TepKemTheo = "https://thongtindoanhnghiep.thuathienhue.gov.vn/UploadFiles/GiayPhep/" + item.fileDinhKem,
                                        };

                                        // Kiểm tra database HueCIT của bảng văn bản của doanh nghiệp có rỗng
                                        if (isAnyDoanhNghiepVanBan)
                                        {
                                            // Tìm kiếm văn bản của doanh nghiệp có tồn tại trên database HueCIT
                                            // Nếu có thì xóa
                                            // Nếu không thì thêm mới văn bản
                                            var VanBan = await _doanhNghiepVanBanRepository.GetBySoKyHieu(vanban.SoKyHieu);
                                            if (VanBan != null)
                                            {
                                                await _doanhNghiepVanBanRepository.Delete(VanBan.Id);
                                            }
                                        }
                                        await _doanhNghiepVanBanRepository.Insert(vanban);
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
