using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class VeDiTichLoaiRepository : ConnectDB, IVeDiTichLoaiRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        private readonly ILogger<VeDiTichLoaiRepository> _logger;
        public VeDiTichLoaiRepository(IConfiguration configuration,
                                      ILogger<VeDiTichLoaiRepository> logger) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
            _logger = logger;
        }

        #region CRUD
        public async Task<VeDiTichLoai> Get(int VeId, int LoaiDoiTuong)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@VeId", VeId);
                    parameters.Add("@LoaiDoiTuong", LoaiDoiTuong);
                    VeDiTichLoai list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichLoai>(conn, "SP_VeDiTichLoaiGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<VeDiTichLoai>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<VeDiTichLoai> list = await conn.QueryAsync<VeDiTichLoai>("SP_VeDiTichLoaiGets", commandType: CommandType.StoredProcedure);
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

        public async Task<VeDiTichLoai> Insert(VeDiTichLoai data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@VeId", data.LoaiVeId);
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@GiaVe", data.GiaVe);
                    parameters.Add("@LoaiDoiTuong", data.LoaiDoiTuong);
                    VeDiTichLoai list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichLoai>(conn, "SP_VeDiTichLoaiAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<VeDiTichLoai> Update(VeDiTichLoai data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@VeId", data.LoaiVeId);
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@GiaVe", data.GiaVe);
                    parameters.Add("@LoaiDoiTuong", data.LoaiDoiTuong);
                    VeDiTichLoai list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichLoai>(conn, "SP_VeDiTichLoaiEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<VeDiTichLoaiTrinhDien>> GetsTrinhDien(VeDiTichLoaiRequest req)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiKhach", req.LoaiDoiTuong);
                    parameters.Add("@LoaiVe", req.LoaiVe);
                    IEnumerable<VeDiTichLoai> list = await SqlMapper.QueryAsync<VeDiTichLoai>(conn, "SP_VeDiTichLoaiGets", parameters, commandType: CommandType.StoredProcedure);

                    var result = list.Select(x =>
                    {
                        var loai = new VeDiTichLoaiTrinhDien
                        {
                            Id = x.Id,
                            GiaVe = x.GiaVe,
                            TenLoai = x.TenLoai,
                            Loai = (LoaiKhachVeDiTich)x.LoaiDoiTuong
                        };

                        if (Enum.IsDefined(typeof(LoaiKhachVeDiTich), x.LoaiDoiTuong))
                        {
                            loai.TenDoiTuong = StringEnum.GetStringValue((LoaiKhachVeDiTich)x.LoaiDoiTuong);
                        }
                        else
                        {
                            loai.TenDoiTuong = "Không xác định";
                        }

                        return loai;
                    });

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

        #endregion


        #region ĐỒNG BỘ
        public async Task GetDataLoaiVe()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _config.GetValue<string>("VeAddress");
                    var url = $"{baseUrl}/loaive";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var dataDaXuLy = JsonConvert.DeserializeObject<List<VeDiTichLoaiDongBo>>(apiResponse);

                        foreach (var item in dataDaXuLy)
                        {
                            var DSloaive = await this.Gets();
                            VeDiTichLoai data = new VeDiTichLoai()
                            {
                                TenLoai = item.name,
                                GiaVe = item.price,
                                LoaiDoiTuong = item.customerTypeID,
                                LoaiVeId = item.ticketTypeID
                            };

                            if (DSloaive.Count() > 0)
                            {
                                var loaive = await this.Get(item.ticketTypeID, item.customerTypeID);
                                if (loaive != null)
                                {
                                    data.Id = loaive.Id;
                                    await this.Update(data);
                                }
                                else
                                {
                                    await this.Insert(data);
                                }
                            }
                            else
                            {
                                await this.Insert(data);
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
        #endregion

    }
}
