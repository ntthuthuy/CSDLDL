using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class DoanhNghiepNganhNgheRepository : ConnectDB, IDoanhNghiepNganhNgheRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepNganhNgheRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepNganhNgheTrinhDien> GetByDongBoID(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    DoanhNghiepNganhNgheTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepNganhNgheTrinhDien>(conn, "SP_DoanhNghiepNganhNgheGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepNganhNgheTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepNganhNgheTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepNganhNgheTrinhDien>(conn, "SP_DoanhNghiepNganhNgheGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DoanhNghiepNganhNgheTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DoanhNghiepNganhNgheTrinhDien> list = await conn.QueryAsync<DoanhNghiepNganhNgheTrinhDien>("SP_DoanhNghiepNganhNgheGets", commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepNganhNghe> Insert(DoanhNghiepNganhNghe data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@TenNganhNghe", data.TenNganhNghe);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    DoanhNghiepNganhNghe list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepNganhNghe>(conn, "SP_DoanhNghiepNganhNgheAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepNganhNghe> Update(DoanhNghiepNganhNghe data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@TenNganhNghe", data.TenNganhNghe);
                    DoanhNghiepNganhNghe list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepNganhNghe>(conn, "SP_DoanhNghiepNganhNgheEdit", parameters, commandType: CommandType.StoredProcedure);
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
    }
}
