using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class DoanhNghiepVanBanRepository : ConnectDB, IDoanhNghiepVanBanRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepVanBanRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepVanBanTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepVanBanTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepVanBanTrinhDien>(conn, "SP_DoanhNghiepVanBanGet", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<IEnumerable<DoanhNghiepVanBanTrinhDien>> GetsByMaDoanhNghiep(string madoanhnghiep)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaSoDoanhNghiep", madoanhnghiep);
                    IEnumerable<DoanhNghiepVanBanTrinhDien> list = await SqlMapper.QueryAsync<DoanhNghiepVanBanTrinhDien>(conn, "SP_DoanhNghiepVanBanGetsByMaDoanhNghiep", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task Delete(int id)
        {
            
            using (SqlConnection conn = IConnectData())
            {
                List<int> result = new List<int>();
                try
                {
                    await conn.OpenAsync();
                    
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@ID", id);
                        await SqlMapper.ExecuteAsync(conn, "SP_DoanhNghiepVanBanDelete", parameters, commandType: CommandType.StoredProcedure);
                    
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

        public async Task<IEnumerable<DoanhNghiepVanBanTrinhDien>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    IEnumerable<DoanhNghiepVanBanTrinhDien> list = await conn.QueryAsync<DoanhNghiepVanBanTrinhDien>("SP_DoanhNghiepVanBanGets", commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepVanBan> Insert(DoanhNghiepVanBan data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaDoanhNghiep", data.MaDoanhNghiep);
                    parameters.Add("@MaLoai", data.MaLoai);
                    parameters.Add("@SoKyHieu", data.SoKyHieu);
                    parameters.Add("@TrichYeu", data.TrichYeu);
                    parameters.Add("@NgayHieuLuc", data.NgayHieuLuc);
                    parameters.Add("@NgayHetHieuLuc", data.NgayHetHieuLuc);
                    parameters.Add("@TepKemTheo", data.TepKemTheo);
                    parameters.Add("@TenGiayPhep", data.TenGiayPhep);
                    parameters.Add("@MaSoDoanhNghiep", data.MaSoDoanhNghiep);
                    DoanhNghiepVanBan list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepVanBan>(conn, "SP_DoanhNghiepVanBanAdd", parameters, commandType: CommandType.StoredProcedure);
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
    
        public async Task<DoanhNghiepVanBanTrinhDien> GetBySoKyHieu(string soKyHieu)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@SoKyHieu", soKyHieu);
                    DoanhNghiepVanBanTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepVanBanTrinhDien>(conn, "SP_DoanhNghiepVanBanGetBySoKyHieu", parameters, commandType: CommandType.StoredProcedure);
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
