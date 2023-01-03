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
    public class PhanAnhHienTruongLinhVucRepository : ConnectDB, IPhanAnhHienTruongLinhVucRepository
    {
        private readonly SqlConnection _conn;

        public PhanAnhHienTruongLinhVucRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectDataMain();
        }

        public async Task<PhanAnhHienTruongLinhVuc> InsertLinhVucPhanAnhHienTruong(PhanAnhHienTruongLinhVuc data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@LinhVuc", data.LinhVuc);
                    PhanAnhHienTruongLinhVuc list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongLinhVuc>(conn, "SP_LoaiLinhVucPhanAnhHienTruongAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongLinhVuc> UpdateLinhVucPhanAnhHienTruong(PhanAnhHienTruongLinhVuc data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@LinhVuc", data.LinhVuc);
                    PhanAnhHienTruongLinhVuc list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongLinhVuc>(conn, "SP_LoaiLinhVucPhanAnhHienTruongEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongLinhVuc> UpdateIsEnableLinhVucPhanAnhHienTruong(int id, bool isEnable)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    parameters.Add("@IsEnable", isEnable);
                    PhanAnhHienTruongLinhVuc list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongLinhVuc>(conn, "SP_LoaiLinhVucPhanAnhHienTruongUpdateIsEnable", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<PhanAnhHienTruongLinhVuc>> GetsLinhVucPhanAnhHienTruong()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<PhanAnhHienTruongLinhVuc> list = conn.Query<PhanAnhHienTruongLinhVuc>("SP_LoaiLinhVucPhanAnhHienTruongGets", commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongLinhVuc> GetLinhVucPhanAnhHienTruong(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    PhanAnhHienTruongLinhVuc list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongLinhVuc>(conn, "SP_LoaiLinhVucPhanAnhHienTruongGet", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<IEnumerable<PhanAnhHienTruongLinhVuc>> GetsIsEnableLinhVucPhanAnhHienTruong(bool isEnable)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IsEnable", isEnable);
                    IEnumerable<PhanAnhHienTruongLinhVuc> list = await SqlMapper.QueryAsync<PhanAnhHienTruongLinhVuc>(conn, "SP_LoaiLinhVucPhanAnhHienTruongGetsIsEnable", parameters, commandType: CommandType.StoredProcedure);
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
