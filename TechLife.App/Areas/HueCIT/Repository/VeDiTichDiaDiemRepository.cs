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

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class VeDiTichDiaDiemRepository : ConnectDB, IVeDiTichDiaDiemRepository
    {
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;
        private readonly ILogger<VeDiTichDiaDiemRepository> _logger;
        public VeDiTichDiaDiemRepository(IConfiguration configuration,
                                         ILogger<VeDiTichDiaDiemRepository> logger) : base(configuration)
        {
            _conn = IConnectData();
            _config = configuration;
            _logger = logger;
        }

        public async Task<VeDiTichDiaDiem> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    VeDiTichDiaDiem list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichDiaDiem>(conn, "SP_VeDiTichDiaDiemGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<VeDiTichDiaDiem>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<VeDiTichDiaDiem> list = await conn.QueryAsync<VeDiTichDiaDiem>("SP_VeDiTichDiaDiemGets", commandType: CommandType.StoredProcedure);
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

        public async Task<VeDiTichDiaDiem> Insert(VeDiTichDiaDiem data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    VeDiTichDiaDiem list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichDiaDiem>(conn, "SP_VeDiTichDiaDiemAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<VeDiTichDiaDiem> Update(VeDiTichDiaDiem data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    VeDiTichDiaDiem list = await SqlMapper.QueryFirstOrDefaultAsync<VeDiTichDiaDiem>(conn, "SP_VeDiTichDiaDiemEdit", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task GetDataDiaDiem()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseUrl = _config.GetValue<string>("VeAddress");
                    var url = $"{baseUrl}/DiaDiem";
                    using (var response = await httpClient.GetAsync(url))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        var dataDaXuLy = JsonConvert.DeserializeObject<List<VeDiTichDiaDiemDongBo>>(apiResponse);

                        foreach (var item in dataDaXuLy)
                        {
                            VeDiTichDiaDiem data = new VeDiTichDiaDiem()
                            {
                                Id = item.Id,
                                DiaDiem = item.title
                            };

                            var DSdiadiem = await this.Gets();
                            if (DSdiadiem.Count() > 0)
                            {
                                var diadiem = await this.Get(item.Id);
                                if (diadiem != null)
                                {
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
