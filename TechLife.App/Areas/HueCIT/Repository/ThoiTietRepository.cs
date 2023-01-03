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

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class ThoiTietRepository : ConnectDB, IThoiTietRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        public ThoiTietRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
        }

        #region CRUD
        public async Task<IEnumerable<ThoiTiet>> GetsThoiTiet()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<ThoiTiet> list = conn.Query<ThoiTiet>("SP_ThoiTietGets", commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<ThoiTietTrinhDien>> GetsThoiTietTrinhDien(ThoiTietRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TuNgay", request.TuNgay);
                    parameters.Add("@DenNgay", request.DenNgay);
                    IEnumerable<ThoiTietTrinhDien> list = await SqlMapper.QueryAsync<ThoiTietTrinhDien>(conn, "SP_ThoiTietTrinhDienGets", parameters, commandType: CommandType.StoredProcedure);
                    foreach (ThoiTietTrinhDien l in list)
                    {
                        string[] temp = l.NhietDo.Split("-");
                        temp[0] = string.Format("{0}°C", temp[0]);
                        temp[1] = string.Format("{0}°C", temp[1]);
                        string from = string.Concat(temp[0].Where(c => !char.IsWhiteSpace(c)));
                        string to = string.Concat(temp[1].Where(c => !char.IsWhiteSpace(c)));
                        l.NhietDo = from + " - " + to;
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

        public async Task<ThoiTiet> GetThoiTiet(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    ThoiTiet list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTiet>(conn, "SP_ThoiTietGet", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<ThoiTiet> InsertThoiTiet(ThoiTiet data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.ID);
                    parameters.Add("@SymbolID", data.SymbolID);
                    parameters.Add("@TuNgay", data.TuNgay);
                    parameters.Add("@DenNgay", data.DenNgay);
                    parameters.Add("@DuBao", data.DuBao);
                    parameters.Add("@Title", data.Title);
                    parameters.Add("@Temperature", data.Temperature);
                    parameters.Add("@PlainArea", data.PlainArea);
                    parameters.Add("@MountainousRegion", data.MountainousRegion);
                    parameters.Add("@HueCityArea", data.HueCityArea);
                    parameters.Add("@MarineWeather", data.MarineWeather);
                    parameters.Add("@ForestfiresForecast", data.ForestfiresForecast);
                    parameters.Add("@Warning", data.Warning);
                    parameters.Add("@Content", data.Content);
                    parameters.Add("@Language", data.Language);
                    parameters.Add("@Published", data.Published);
                    parameters.Add("@OwnerCode", data.OwnerCode);
                    parameters.Add("@ModuleId", data.ModuleId);
                    parameters.Add("@CreatedByUserId", data.CreatedByUserId);
                    parameters.Add("@LastModifiedByUserId", data.LastModifiedByUserId);
                    parameters.Add("@NgayTao", data.NgayTao);
                    ThoiTiet list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTiet>(conn, "SP_ThoiTietAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ThoiTiet> UpdateThoiTiet(ThoiTiet data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.ID);
                    parameters.Add("@SymbolID", data.SymbolID);
                    parameters.Add("@TuNgay", data.TuNgay);
                    parameters.Add("@DenNgay", data.DenNgay);
                    parameters.Add("@DuBao", data.DuBao);
                    parameters.Add("@Title", data.Title);
                    parameters.Add("@Temperature", data.Temperature);
                    parameters.Add("@PlainArea", data.PlainArea);
                    parameters.Add("@MountainousRegion", data.MountainousRegion);
                    parameters.Add("@HueCityArea", data.HueCityArea);
                    parameters.Add("@MarineWeather", data.MarineWeather);
                    parameters.Add("@ForestfiresForecast", data.ForestfiresForecast);
                    parameters.Add("@Warning", data.Warning);
                    parameters.Add("@Content", data.Content);
                    parameters.Add("@Language", data.Language);
                    parameters.Add("@Published", data.Published);
                    parameters.Add("@OwnerCode", data.OwnerCode);
                    parameters.Add("@ModuleId", data.ModuleId);
                    parameters.Add("@CreatedByUserId", data.CreatedByUserId);
                    parameters.Add("@LastModifiedByUserId", data.LastModifiedByUserId);
                    parameters.Add("@NgayTao", data.NgayTao);
                    ThoiTiet list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTiet>(conn, "SP_ThoiTietEdit", parameters, commandType: CommandType.StoredProcedure);
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

        #endregion

        #region ĐỒNG BỘ
        public async Task GetData()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var baseUrl = _config.GetValue<string>("ThoiTietAddress");
                var url = $"{baseUrl}/GetWeather";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var dataDaXuLy = JsonConvert.DeserializeObject<List<ThoiTietModel>>(apiResponse);

                        if (dataDaXuLy.Count() > 0)
                        {
                            foreach (var item in dataDaXuLy)
                            {
                                DateTime? tuNgay = null;
                                try
                                {
                                    tuNgay = DateTime.Parse(item.WeatherOfDay);
                                }
                                catch (Exception ex)
                                {
                                    tuNgay = null;
                                }

                                ThoiTiet thoiTiet = new ThoiTiet
                                {
                                    ID = item.ID,
                                    SymbolID = item.SymbolID,
                                    Title = item.Title,
                                    TuNgay = tuNgay,
                                    Temperature = item.Temperature,
                                    PlainArea = item.PlainArea,
                                    MountainousRegion = item.MountainousRegion,
                                    HueCityArea = item.HueCityArea,
                                    MarineWeather = item.MarineWeather,
                                    ForestfiresForecast = item.ForestfiresForecast,
                                    Warning = item.Warning,
                                    Content = item.Content,
                                    Language = item.Language,
                                    Published = bool.Parse(item.Published),
                                    OwnerCode = item.OwnerCode,
                                    ModuleId = int.Parse(item.ModuleId),
                                    CreatedByUserId = int.Parse(item.CreatedByUserId),
                                    LastModifiedByUserId = int.Parse(item.LastModifiedByUserId),
                                    NgayTao = DateTime.Now
                                };

                                var thoiTiet_DB = await this.GetThoiTiet(thoiTiet.ID);
                                if (thoiTiet_DB != null)
                                {
                                    await this.UpdateThoiTiet(thoiTiet);
                                }
                                else
                                {
                                    await this.InsertThoiTiet(thoiTiet);
                                }

                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
