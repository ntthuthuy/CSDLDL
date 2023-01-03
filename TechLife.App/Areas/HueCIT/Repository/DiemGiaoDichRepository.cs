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
    public class DiemGiaoDichRepository : ConnectDB, IDiemGiaoDichRepository
    {
        private readonly SqlConnection _conn;
        public DiemGiaoDichRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<IEnumerable<DiemGiaoDichTrinhDien>> Gets(DiemGiaoDichRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@ID", data.DiaDiem);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    parameters.Add("@DongBo", data.NguonDongBo);
                    IEnumerable<DiemGiaoDichTrinhDien> list = conn.Query<DiemGiaoDichTrinhDien>("SP_DiemGiaoDichGets", param: parameters, commandType: CommandType.StoredProcedure);

                    IEnumerable<DiaPhuong> dp = await GetsDiaPhuong();
                    foreach (DiemGiaoDichTrinhDien l in list)
                    {
                        if (l.PhuongXaId != 0)
                        {
                            var item = dp.FirstOrDefault(i => i.Id == l.PhuongXaId);
                            if (item != null)
                            {
                                l.TenPhuongXa = item.TenDiaPhuong;
                            }
                        }

                        if (l.QuanHuyenId != 0)
                        {
                            var item = dp.FirstOrDefault(i => i.Id == l.QuanHuyenId);
                            if (item != null)
                            {
                                l.TenQuanHuyen = item.TenDiaPhuong;
                            }
                        }
                    }

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

        public async Task<DiemGiaoDichTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    DiemGiaoDichTrinhDien list = conn.QueryFirstOrDefault<DiemGiaoDichTrinhDien>("SP_DiemGiaoDichGet", parameters, commandType: CommandType.StoredProcedure);
                    IEnumerable<DiaPhuong> dp = await GetsDiaPhuong();

                    if (list.PhuongXaId != 0)
                    {
                        var item = dp.FirstOrDefault(i => i.Id == list.PhuongXaId);
                        if (item != null)
                        {
                            list.TenPhuongXa = item.TenDiaPhuong;
                        }
                    }

                    if (list.QuanHuyenId != 0)
                    {
                        var item = dp.FirstOrDefault(i => i.Id == list.QuanHuyenId);
                        if (item != null)
                        {
                            list.TenQuanHuyen = item.TenDiaPhuong;
                        }
                    }

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

        public async Task<DiemGiaoDich> Add(DiemGiaoDich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@TenDiaDiem", data.TenDiaDiem);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@GioPhucVu", data.GioPhucVu);
                    parameters.Add("@DiaChi", data.DiaChi);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@DiemGiaoDichID", data.DiemGiaoDichID);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    DiemGiaoDich list = await SqlMapper.QueryFirstOrDefaultAsync<DiemGiaoDich>(conn, "SP_DiemGiaoDichAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DiemGiaoDich> Edit(DiemGiaoDich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@TenDiaDiem", data.TenDiaDiem);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@GioPhucVu", data.GioPhucVu);
                    parameters.Add("@DiaChi", data.DiaChi);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    DiemGiaoDich list = await SqlMapper.QueryFirstOrDefaultAsync<DiemGiaoDich>(conn, "SP_DiemGiaoDichEdit", parameters, commandType: CommandType.StoredProcedure);
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
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_DiemGiaoDichDelete", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DiemGiaoDichSearch>> GetsSearch()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DiemGiaoDichSearch> list = conn.Query<DiemGiaoDichSearch>("SP_DiemGiaoDichGetsSearch", commandType: CommandType.StoredProcedure);

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

        public async Task<DiemGiaoDichTrinhDien> GetByDiemGiaoDichID(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DiemGiaoDichID", id);
                    DiemGiaoDichTrinhDien list = conn.QueryFirstOrDefault<DiemGiaoDichTrinhDien>("SP_DiemGiaoDichGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);

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

        private async Task<IEnumerable<DiaPhuong>> GetsDiaPhuong()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DiaPhuong> list = conn.Query<DiaPhuong>("SP_DiaPhuongGets", commandType: CommandType.StoredProcedure);

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
