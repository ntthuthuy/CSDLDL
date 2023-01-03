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
    public class DoanhNghiepLoaiVanBanRepository : ConnectDB, IDoanhNghiepLoaiVanBanRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepLoaiVanBanRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepLoaiVanBanTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepLoaiVanBanTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiVanBanTrinhDien>(conn, "SP_DoanhNghiepLoaiVanBanGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepLoaiVanBanTrinhDien> GetByDongBoID(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    DoanhNghiepLoaiVanBanTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiVanBanTrinhDien>(conn, "SP_DoanhNghiepLoaiVanBanGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DoanhNghiepLoaiVanBanTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DoanhNghiepLoaiVanBanTrinhDien> list = await conn.QueryAsync<DoanhNghiepLoaiVanBanTrinhDien>("SP_DoanhNghiepLoaiVanBanGets", commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepLoaiVanBan> Insert(DoanhNghiepLoaiVanBan data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    DoanhNghiepLoaiVanBan list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiVanBan>(conn, "SP_DoanhNghiepLoaiVanBanAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepLoaiVanBan> Update(DoanhNghiepLoaiVanBan data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@TenLoai", data.TenLoai);
                    DoanhNghiepLoaiVanBan list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepLoaiVanBan>(conn, "SP_DoanhNghiepLoaiVanBanEdit", parameters, commandType: CommandType.StoredProcedure);
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
