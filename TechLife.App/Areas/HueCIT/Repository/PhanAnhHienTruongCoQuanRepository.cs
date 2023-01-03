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
    public class PhanAnhHienTruongCoQuanRepository : ConnectDB, IPhanAnhHienTruongCoQuanRepository
    {
        public PhanAnhHienTruongCoQuanRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PhanAnhHienTruongCoQuan> GetPhanAnhHienTruongCoQuan(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDinhDanh", id);
                    PhanAnhHienTruongCoQuan list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongCoQuan>(conn, "SP_PhanAnhHienTruongCoQuanGet", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<PhanAnhHienTruongCoQuan>> GetsPhanAnhHienTruongCoQuan()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<PhanAnhHienTruongCoQuan> list = conn.Query<PhanAnhHienTruongCoQuan>("SP_PhanAnhHienTruongCoQuanGets", commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongCoQuan> InsertPhanAnhHienTruongCoQuan(PhanAnhHienTruongCoQuan data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDinhDanh", data.Id);
                    parameters.Add("@TenCoQuan", data.TenCoQuan);

                    PhanAnhHienTruongCoQuan list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongCoQuan>(conn, "SP_PhanAnhHienTruongCoQuanAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongCoQuan> UpdatePhanAnhHienTruongCoQuan(PhanAnhHienTruongCoQuan data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDinhDanh", data.Id);
                    parameters.Add("@TenCoQuan", data.TenCoQuan);

                    PhanAnhHienTruongCoQuan list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongCoQuan>(conn, "SP_PhanAnhHienTruongCoQuanEdit", parameters, commandType: CommandType.StoredProcedure);
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
