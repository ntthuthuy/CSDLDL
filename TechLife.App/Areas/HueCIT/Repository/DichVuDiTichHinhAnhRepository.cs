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
    public class DichVuDiTichHinhAnhRepository : ConnectDB, IDichVuDiTichHinhAnhRepository
    {
        private readonly SqlConnection _conn;
        public DichVuDiTichHinhAnhRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DichVuDiTichHinhAnh> Add(DichVuDiTichHinhAnh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDVDT", data.MaDVDT);
                    parameters.Add("@TieuDeAnh", data.TieuDeAnh);
                    parameters.Add("@URLHinhAnh", data.URLHinhAnh);
                    parameters.Add("@IsAnhDaiDien", data.IsAnhDaiDien);
                    parameters.Add("@SapXep", data.SapXep);
                    DichVuDiTichHinhAnh list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTichHinhAnh>(conn, "SP_DichVuDiTichHinhAnhAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DichVuDiTichHinhAnh> Edit(DichVuDiTichHinhAnh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@MaDVDT", data.MaDVDT);
                    parameters.Add("@TieuDeAnh", data.TieuDeAnh);
                    parameters.Add("@URLHinhAnh", data.URLHinhAnh);
                    parameters.Add("@IsAnhDaiDien", data.IsAnhDaiDien);
                    parameters.Add("@SapXep", data.SapXep);
                    DichVuDiTichHinhAnh list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTichHinhAnh>(conn, "SP_DichVuDiTichHinhAnhEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DichVuDiTichHinhAnhTrinhDien> GetByMaDVDT(int MaDVDT)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDVDT", MaDVDT);
                    DichVuDiTichHinhAnhTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DichVuDiTichHinhAnhTrinhDien>(conn, "SP_DichVuDiTichHinhAnhGetByMaDVDT", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<DichVuDiTichHinhAnhTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DichVuDiTichHinhAnhTrinhDien> list = conn.Query<DichVuDiTichHinhAnhTrinhDien>("SP_DichVuDiTichHinhAnhGets", commandType: CommandType.StoredProcedure);
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
