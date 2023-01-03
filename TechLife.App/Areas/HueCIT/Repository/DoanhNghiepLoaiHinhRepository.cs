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
    public class DoanhNghiepLoaiHinhRepository : ConnectDB, IDoanhNghiepLoaiHinhRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepLoaiHinhRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepLoaiHinhTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepLoaiHinhTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiHinhTrinhDien>(conn, "SP_DoanhNghiepLoaiHinhGet", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<DoanhNghiepLoaiHinhTrinhDien> GetByDongBoID(int? id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    DoanhNghiepLoaiHinhTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiHinhTrinhDien>(conn, "SP_DoanhNghiepLoaiHinhGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DoanhNghiepLoaiHinhTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DoanhNghiepLoaiHinhTrinhDien> list = await conn.QueryAsync<DoanhNghiepLoaiHinhTrinhDien>("SP_DoanhNghiepLoaiHinhGets", commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepLoaiHinh> Insert(DoanhNghiepLoaiHinh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiHinh", data.LoaiHinh);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    DoanhNghiepLoaiHinh list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiHinh>(conn, "SP_DoanhNghiepLoaiHinhAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepLoaiHinh> Update(DoanhNghiepLoaiHinh data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@LoaiHinh", data.LoaiHinh);
                    DoanhNghiepLoaiHinh list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiHinh>(conn, "SP_DoanhNghiepLoaiHinhEdit", parameters, commandType: CommandType.StoredProcedure);
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
