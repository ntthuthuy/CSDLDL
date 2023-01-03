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
    public class DoanhNghiepTrangThaiRepository : ConnectDB, IDoanhNghiepTrangThaiRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepTrangThaiRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepTrangThaiTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepTrangThaiTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrangThaiTrinhDien>(conn, "SP_DoanhNghiepTrangThaiGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepTrangThaiTrinhDien> GetByDongBoID(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    DoanhNghiepTrangThaiTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrangThaiTrinhDien>(conn, "SP_DoanhNghiepTrangThaiGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DoanhNghiepTrangThaiTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DoanhNghiepTrangThaiTrinhDien> list = await conn.QueryAsync<DoanhNghiepTrangThaiTrinhDien>("SP_DoanhNghiepTrangThaiGets", commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepTrangThai> Insert(DoanhNghiepTrangThai data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TrangThai", data.TrangThai);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@TenClassCSS", data.TenClassCSS);
                    DoanhNghiepTrangThai list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrangThai>(conn, "SP_DoanhNghiepTrangThaiAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepTrangThai> Update(DoanhNghiepTrangThai data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@TrangThai", data.TrangThai);
                    parameters.Add("@TenClassCSS", data.TenClassCSS);
                    DoanhNghiepTrangThai list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrangThai>(conn, "SP_DoanhNghiepTrangThaiEdit", parameters, commandType: CommandType.StoredProcedure);
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
