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
    public class PhanAnhHienTruongHinhAnhRepository : ConnectDB, IPhanAnhHienTruongHinhAnhRepository
    {
        public PhanAnhHienTruongHinhAnhRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<PhanAnhHienTruongHinhAnh>> GetsPhanAnhHienTruongHinhAnh()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<PhanAnhHienTruongHinhAnh> list = conn.Query<PhanAnhHienTruongHinhAnh>("SP_PhanAnhHienTruongHinhAnhGets", commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<PhanAnhHienTruongHinhAnh>> GetsPhanAnhHienTruongHinhAnhByPhanAnhId(int PhanAnhId)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PhanAnhId", PhanAnhId);
                    IEnumerable<PhanAnhHienTruongHinhAnh> list = await SqlMapper.QueryAsync<PhanAnhHienTruongHinhAnh>(conn, "SP_PhanAnhHienTruongHinhAnhGetsByPhanAnhID", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<PhanAnhHienTruongHinhAnh> InsertPhanAnhHienTruongHinhAnh(PhanAnhHienTruongHinhAnh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaPhanAnh", data.MaPhanAnh);
                    parameters.Add("@HinhAnh", data.HinhAnh);
                    parameters.Add("@HinhAnhThumb", data.HinhAnhThumb);
                    parameters.Add("@IsKetQua", data.IsKetQua);

                    PhanAnhHienTruongHinhAnh list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongHinhAnh>(conn, "SP_PhanAnhHienTruongHinhAnhAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongHinhAnh> UpdatePhanAnhHienTruongHinhAnh(PhanAnhHienTruongHinhAnh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaPhanAnh", data.MaPhanAnh);
                    parameters.Add("@HinhAnh", data.HinhAnh);
                    parameters.Add("@HinhAnhThumb", data.HinhAnhThumb);
                    parameters.Add("@IsKetQua", data.IsKetQua);
                    parameters.Add("@Id", data.Id);

                    PhanAnhHienTruongHinhAnh list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongHinhAnh>(conn, "SP_PhanAnhHienTruongHinhAnhEdit", parameters, commandType: CommandType.StoredProcedure);
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
                    int result = await SqlMapper.ExecuteAsync(conn, "SP_PhanAnhHienTruongHinhAnhDelete", parameters, commandType: CommandType.StoredProcedure);

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
    }
}
