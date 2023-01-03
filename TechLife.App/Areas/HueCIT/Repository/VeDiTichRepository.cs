using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class VeDiTichRepository : ConnectDB, IVeDiTichRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        private readonly IVeDiTichDiaDiemRepository _veDiTichDiaDiemRepository;

        private readonly string TU_NGAY = "20220509";

        public VeDiTichRepository(IConfiguration configuration,
                                  IVeDiTichDiaDiemRepository vediTichDiaDiemRepository) 
            : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
            _veDiTichDiaDiemRepository = vediTichDiaDiemRepository;
        }

        public async Task<IEnumerable<VeDiTichTrinhDien>> Gets(VeDiTichRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiKhach", request.LoaiKhach);
                    parameters.Add("@DiaDiem", request.DiaDiem);
                    parameters.Add("@TuNgay", request.TuNgay);
                    parameters.Add("@DenNgay", request.DenNgay);
                    IEnumerable<VeDiTichTrinhDien> list = await SqlMapper.QueryAsync<VeDiTichTrinhDien>(conn, "SP_VeDiTichGets", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<VeDiTich> Insert(VeDiTich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@NgayBan", data.NgayBan);
                    parameters.Add("@LoaiKhach", data.LoaiKhach);
                    parameters.Add("@MaDiaDiem", data.MaDiaDiem);
                    parameters.Add("@SoLuong", data.SoLuong);
                    parameters.Add("@TongTien", data.TongTien);
                    parameters.Add("@SoVeDon", data.SoVeDon);
                    parameters.Add("@SoVeTuyen", data.SoVeTuyen);
                    parameters.Add("@TenLoaiKhach", data.TenLoaiKhach);
                    parameters.Add("@NgayDongBo", data.NgayDongBo);

                    VeDiTich list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTich>(conn, "SP_VeDiTichAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    int result = await SqlMapper.ExecuteAsync(conn, "SP_VeDiTichDelete", parameters, commandType: CommandType.StoredProcedure);

                    return result;
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

        public async Task<IEnumerable<VeDiTichTrinhDien>> GetsByNgayBan(DateTime ngayban)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@NgayBan", ngayban);
                    IEnumerable<VeDiTichTrinhDien> list = await SqlMapper.QueryAsync<VeDiTichTrinhDien>(conn, "SP_VeDiTichGetsByNgayBan", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task GetDataVeDiTich()
        {
            try
            {
                DateTime tungay = DateTime.ParseExact(TU_NGAY, "yyyyMMdd", new CultureInfo("vi-VN"));

                var DSveditich = await this.Gets(new VeDiTichRequest());
                if (DSveditich.Count() > 0)
                {
                    List<VeDiTichTrinhDien> list = DSveditich.ToList();
                    foreach (var i in list)
                    {
                        tungay = i.NgayBan;
                        break;
                    }
                }

                while (DateTime.Compare(tungay.Date, DateTime.Now.Date) <= 0)
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var baseUrl = _config.GetValue<string>("VeAddress");
                        var url = $"{baseUrl}/doanhthudiadiem/{tungay.ToString("yyyyMMdd")}/{tungay.ToString("yyyyMMdd")}";
                        using (var response = await httpClient.GetAsync(url))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var dataDaXuLy = JsonConvert.DeserializeObject<List<VeDiTichDongBo>>(apiResponse);

                            // Xóa vé dí tích trước đó
                            var findVeDiTich = await this.GetsByNgayBan(tungay);
                            if (findVeDiTich.Any())
                            {
                                foreach (var i in findVeDiTich)
                                {
                                    await this.Delete(i.Id);
                                }
                            }

                            // Thêm vé di tích thời điểm hiện tại
                            foreach (var item in dataDaXuLy)
                            {
                                VeDiTichDiaDiem diadiem = new VeDiTichDiaDiem()
                                {
                                    Id = item.placeID,
                                    DiaDiem = item.placeTitle
                                };

                                VeDiTich ve = new VeDiTich()
                                {
                                    NgayBan = tungay.Date,
                                    LoaiKhach = item.customerType,
                                    SoLuong = item.soVeDon + item.soVeTuyen,
                                    TongTien = item.total,
                                    SoVeDon = item.soVeDon,
                                    SoVeTuyen = item.soVeTuyen,
                                    TenLoaiKhach = item.customerTypeName,
                                    NgayDongBo = DateTime.Now,
                                    MaDiaDiem = item.placeID
                                };

                                VeDiTichDiaDiem dd = await _veDiTichDiaDiemRepository.Get(diadiem.Id);
                                if (dd != null)
                                {
                                    await _veDiTichDiaDiemRepository.Update(diadiem);
                                }
                                else
                                {
                                    await _veDiTichDiaDiemRepository.Insert(diadiem);
                                }

                                await this.Insert(ve);
                            }
                        }
                    }

                    tungay = tungay.AddDays(1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
