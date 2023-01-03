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
    public class SuKienRepository : ConnectDB, ISuKienRepository
    {
        private readonly SqlConnection _conn;
        public SuKienRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<IEnumerable<SuKienTrinhDien>> Gets(SuKienRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaChuDe", data.MaChuDe);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    IEnumerable<SuKienTrinhDien> list = conn.Query<SuKienTrinhDien>("SP_SuKienGets", param: parameters, commandType: CommandType.StoredProcedure);
                    IEnumerable<DiaPhuong> dp = await GetsDiaPhuong();
                    foreach (SuKienTrinhDien l in list)
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

        public async Task<SuKienTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    SuKienTrinhDien list = conn.QueryFirstOrDefault<SuKienTrinhDien>("SP_SuKienGet", parameters, commandType: CommandType.StoredProcedure);

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@LoaiDoiTuong", "DL_SuKien");
                    param.Add("@IDDoiTuong", id);
                    List<FileUpload> files = (conn.Query<FileUpload>("SP_ThuVienMediaGets", param, commandType: CommandType.StoredProcedure)).ToList();
                    list.Files = files;

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
        public async Task<SuKien> Add(SuKien data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaChuDe", data.MaChuDe);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@TrangThai", data.TrangThai);
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    SuKien list = await SqlMapper.QueryFirstOrDefaultAsync<SuKien>(conn, "SP_SuKienAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<SuKien> Edit(SuKien data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID.ToString());
                    parameters.Add("@MaChuDe", data.MaChuDe);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@TrangThai", data.TrangThai);
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    SuKien list = await SqlMapper.QueryFirstOrDefaultAsync<SuKien>(conn, "SP_SuKienEdit", parameters, commandType: CommandType.StoredProcedure);
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
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_SuKienDelete", parameters, commandType: CommandType.StoredProcedure);
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
