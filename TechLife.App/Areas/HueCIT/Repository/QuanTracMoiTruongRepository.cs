using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using static TechLife.Common.Enums.HueCIT.QuanTracMoiTruong;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class QuanTracMoiTruongRepository : ConnectDB, IQuanTracMoiTruongRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;

        // Hằng số đồng bộ
        private readonly string[] DANH_SACH_TEN_TONG_SO = new string[] { "AQI", "TEMP", "TVOC", "HUM", "PM01", "PM25", "PM10", "CO2" };
        private readonly Dictionary<string, string> DANH_SACH_DIA_DIEM = new Dictionary<string, string>
        {
            { "BO09_BO09", "Trường Đại Học Khoa Học Huế" }
        };
        //private readonly string START_DATE = "2022-10-01";
        private readonly DateTime START_DATE = new DateTime(2022, 10, 1);


        public QuanTracMoiTruongRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
        }

        public async Task<QuanTracMoiTruongTrinhDien> Get(QuanTracMoiTruongFilter filter)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", filter.Id);
                    parameters.Add("@TenThongSo", filter.TenThongSo);
                    parameters.Add("@Node", filter.Node);
                    QuanTracMoiTruongTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<QuanTracMoiTruongTrinhDien>(conn, "SP_QuanTracMoiTruongGet", parameters, commandType: CommandType.StoredProcedure);
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<QuanTracMoiTruongTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<QuanTracMoiTruongTrinhDien> list = await conn.QueryAsync<QuanTracMoiTruongTrinhDien>("SP_QuanTracMoiTruongGets", commandType: CommandType.StoredProcedure);
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<QuanTracMoiTruong> Insert(QuanTracMoiTruong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@_id", data._id);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@Node", data.Node);
                    parameters.Add("@TenNode", data.TenNode);
                    parameters.Add("@TenThongSo", data.TenThongSo);
                    parameters.Add("@GiaTri", data.GiaTri);
                    parameters.Add("@DonViTinh", data.DonViTinh);
                    parameters.Add("@ThoiDiem", data.ThoiDiem);
                    parameters.Add("@TrangThai", data.TrangThai);
                    QuanTracMoiTruong list = await SqlMapper.QueryFirstOrDefaultAsync<QuanTracMoiTruong>(conn, "SP_QuanTracMoiTruongAdd", parameters, commandType: CommandType.StoredProcedure);
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<QuanTracMoiTruong> Update(QuanTracMoiTruong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@_id", data._id);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@Node", data.Node);
                    parameters.Add("@TenNode", data.TenNode);
                    parameters.Add("@TenThongSo", data.TenThongSo);
                    parameters.Add("@GiaTri", data.GiaTri);
                    parameters.Add("@DonViTinh", data.DonViTinh);
                    parameters.Add("@ThoiDiem", data.ThoiDiem);
                    parameters.Add("@TrangThai", data.TrangThai);
                    QuanTracMoiTruong list = await SqlMapper.QueryFirstOrDefaultAsync<QuanTracMoiTruong>(conn, "SP_QuanTracMoiTruongEdit", parameters, commandType: CommandType.StoredProcedure);
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<List<DanhSachQuanTracMoiTruong>> DanhSachTheoTenThongSo(QuanTracMoiTruongRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    List<DanhSachQuanTracMoiTruong> list = new List<DanhSachQuanTracMoiTruong>();
                    
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@TuNgay", request.TuNgay);
                    param.Add("@DenNgay", request.DenNgay);
                    param.Add("@DiaDiem", request.DiaDiem);
                    IEnumerable<DateTime> DSThoiDiem = await SqlMapper.QueryAsync<DateTime>(conn, "SP_QuanTracMoiTruongGetsThoiDiem",param , commandType: CommandType.StoredProcedure);

                    foreach (var i in DSThoiDiem)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@ThoiDiem", i);
                        IEnumerable<QuanTracMoiTruongTrinhDien> DSQuanTrac = await SqlMapper.QueryAsync<QuanTracMoiTruongTrinhDien>(conn, "SP_QuanTracMoiTruongGetByThoiDiem", parameters, commandType: CommandType.StoredProcedure);

                        DanhSachQuanTracMoiTruong truong = new DanhSachQuanTracMoiTruong
                        {
                            ThoiDiem = i,
                            AQI = DSQuanTrac.Where(x => x.TenThongSo == "AQI").Select(x => { 
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai); 
                                return x; 
                            }).FirstOrDefault(),
                            CO2 = DSQuanTrac.Where(x => x.TenThongSo == "CO2").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            HUM = DSQuanTrac.Where(x => x.TenThongSo == "HUM").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            PM01 = DSQuanTrac.Where(x => x.TenThongSo == "PM01").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            PM10 = DSQuanTrac.Where(x => x.TenThongSo == "PM10").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            PM25 = DSQuanTrac.Where(x => x.TenThongSo == "PM25").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            TEMP = DSQuanTrac.Where(x => x.TenThongSo == "TEMP").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                            TVOC = DSQuanTrac.Where(x => x.TenThongSo == "TVOC").Select(x =>
                            {
                                x.TenTrangThai = Enum.GetName(typeof(TrangThaiQuanTrac), x.TrangThai);
                                return x;
                            }).FirstOrDefault(),
                        };

                        list.Add(truong);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        #region ĐỒNG BỘ
        public async Task GetData()
        {
            DateTime tungay = START_DATE;
            var DSQuanTrac = (await this.Gets()).ToList();

            foreach (var item in DSQuanTrac)
            {
                if (DSQuanTrac.Count() > 0)
                {
                    try
                    {
                        tungay = item.ThoiDiem;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    break;
                }
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var baseURL = _config.GetValue<string>("QuanTracAddress");
                
                while (DateTime.Compare(tungay.Date, DateTime.Now.Date) <= 0)
                {
                    foreach (var tendiadiem in DANH_SACH_DIA_DIEM)
                    {
                        foreach (var tenthongso in DANH_SACH_TEN_TONG_SO)
                        {
                            // Xử lý ngày req body data
                            string tungayString = "";
                            try
                            {
                                tungayString = tungay.ToString("yyyy-MM-dd");
                            }
                            catch (Exception)
                            {
                                continue;
                            }

                            var url = $"{baseURL}/{tendiadiem.Key}/multiday?index={tenthongso}&startDate={tungayString}&endDate={tungayString}";

                            using (var response = await httpClient.GetAsync(url))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var apiResponse = await response.Content.ReadAsStringAsync();
                                    var dataDaXuLy = JsonConvert.DeserializeObject<List<QuanTracMoiTruongDongBo>>(apiResponse);

                                    foreach (var item in dataDaXuLy)
                                    {
                                        // Xử lý ngày
                                        DateTime? thoiDiem = null;
                                        try
                                        {
                                            thoiDiem = DateTime.Parse(item.time);
                                        }
                                        catch (Exception ex)
                                        {
                                            thoiDiem = null;
                                        }

                                        QuanTracMoiTruong quanTrac = new QuanTracMoiTruong
                                        {
                                            Node = tendiadiem.Key,
                                            TenNode = tendiadiem.Value,
                                            TenThongSo = item.index,
                                            GiaTri = item.value,
                                            DonViTinh = item.unit,
                                            TrangThai = item.status,
                                            _id = item._id,
                                            ThoiDiem = thoiDiem,
                                        };

                                        if (DSQuanTrac.Count() > 0)
                                        {
                                            var DSQuanTracBy_id = await this.Get(new QuanTracMoiTruongFilter
                                            {
                                                Id = quanTrac._id,
                                                TenThongSo = quanTrac.TenThongSo,
                                                Node = quanTrac.Node
                                            });

                                            if (DSQuanTracBy_id != null)
                                            {
                                                quanTrac.Id = DSQuanTracBy_id.Id;
                                                await this.Update(quanTrac);
                                            }
                                            else
                                            {
                                                await this.Insert(quanTrac);
                                            }
                                        }
                                        else
                                        {
                                            await this.Insert(quanTrac);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    tungay = tungay.AddDays(1);
                }
            }
        }
        #endregion
    }
}
