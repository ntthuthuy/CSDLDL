using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class TyGiaRepository : ConnectDB, ITyGiaRepository
    {
        private readonly SqlConnection _conn;
        public TyGiaRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<IEnumerable<TyGia>> Gets(TyGiaRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Ngay", data.Ngay);
                    IEnumerable<TyGia> list = conn.Query<TyGia>("SP_TyGiaGets", param: parameters, commandType: CommandType.StoredProcedure);

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
        public async Task<TyGia> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    TyGia list = conn.QueryFirstOrDefault<TyGia>("SP_TyGiaGet", param: parameters, commandType: CommandType.StoredProcedure);

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


        public async Task<TyGia> Add(TyGia data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenNgoaiTe", data.TenNgoaiTe);
                    parameters.Add("@KyHieu", data.KyHieu);
                    parameters.Add("@Ngay", data.Ngay);
                    parameters.Add("@GiaMua", data.GiaMua);
                    parameters.Add("@GiaBan", data.GiaBan);
                    TyGia list = await SqlMapper.QueryFirstOrDefaultAsync<TyGia>(conn, "SP_TyGiaAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<TyGia> Edit(TyGia data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID.ToString());
                    parameters.Add("@TenNgoaiTe", data.TenNgoaiTe);
                    parameters.Add("@KyHieu", data.KyHieu);
                    parameters.Add("@GiaMua", data.GiaMua);
                    parameters.Add("@GiaBan", data.GiaBan);
                    TyGia list = await SqlMapper.QueryFirstOrDefaultAsync<TyGia>(conn, "SP_TyGiaEdit", parameters, commandType: CommandType.StoredProcedure);
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
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_TyGiaDelete", parameters, commandType: CommandType.StoredProcedure);
                    return kq;

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
