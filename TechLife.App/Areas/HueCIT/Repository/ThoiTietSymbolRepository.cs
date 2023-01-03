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
    public class ThoiTietSymbolRepository : ConnectDB, IThoiTietSymbolRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        public ThoiTietSymbolRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
        }

        public async Task<IEnumerable<ThoiTietSymbol>> GetsThoiTietSymbol()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<ThoiTietSymbol> list = conn.Query<ThoiTietSymbol>("SP_ThoiTietSymbolGets", commandType: CommandType.StoredProcedure);
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

        public async Task<ThoiTietSymbol> GetThoiTietSymbol(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    ThoiTietSymbol list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTietSymbol>(conn, "SP_ThoiTietSymbolGet", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<ThoiTietSymbol> InsertThoiTietSymbol(ThoiTietSymbol data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.ID);
                    parameters.Add("@Language", data.Language);
                    parameters.Add("@SymbolName", data.SymbolName);
                    parameters.Add("@IdFile", data.IDFile);
                    parameters.Add("@TenFileDinhKem", data.TenFileDinhKem);
                    parameters.Add("@SortOrder", data.SortOrder);
                    parameters.Add("@Published", data.Published);
                    parameters.Add("@NgayTao", data.NgayTao);
                    ThoiTietSymbol list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTietSymbol>(conn, "SP_ThoiTietSymbolAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ThoiTietSymbol> UpdateThoiTietSymbol(ThoiTietSymbol data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.ID);
                    parameters.Add("@Language", data.Language);
                    parameters.Add("@SymbolName", data.SymbolName);
                    parameters.Add("@IdFile", data.IDFile);
                    parameters.Add("@TenFileDinhKem", data.TenFileDinhKem);
                    parameters.Add("@SortOrder", data.SortOrder);
                    parameters.Add("@Published", data.Published);
                    parameters.Add("@NgayTao", data.NgayTao);
                    ThoiTietSymbol list = await SqlMapper.QueryFirstOrDefaultAsync<ThoiTietSymbol>(conn, "SP_ThoiTietSymbolEdit", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task GetDataSymbol()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var baseUrl = _config.GetValue<string>("ThoiTietAddress");
                var url = $"{baseUrl}/GetWeatherSymboy";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var dataDaXuLy = JsonConvert.DeserializeObject<List<ThoiTietSymbolModel>>(apiResponse);

                        if (dataDaXuLy.Count() > 0)
                        {
                            var thoiTietSymbols_DB = await this.GetsThoiTietSymbol();
                            foreach (var symbol in dataDaXuLy)
                            {
                                ThoiTietSymbol thoiTietSymbol = new ThoiTietSymbol
                                {
                                    ID = symbol.ID,
                                    IDFile = symbol.IDFile,
                                    Language = symbol.Language,
                                    Published = bool.Parse(symbol.Published),
                                    SortOrder = int.Parse(symbol.SortOrder),
                                    SymbolName = symbol.SymbolName,
                                    TenFileDinhKem = "https://thuathienhue.gov.vn/Portals/0/Nam2015/T7/" + symbol.TenFileDinhKem,
                                    NgayTao = DateTime.Now
                                };

                                var thoiTietSymbolDB = await this.GetThoiTietSymbol(thoiTietSymbol.ID);
                                if (thoiTietSymbolDB != null)
                                {
                                    await this.UpdateThoiTietSymbol(thoiTietSymbol);
                                }
                                else
                                {
                                    await this.InsertThoiTietSymbol(thoiTietSymbol);
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
