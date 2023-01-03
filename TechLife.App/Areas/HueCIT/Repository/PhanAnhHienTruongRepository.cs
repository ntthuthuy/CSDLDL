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
    public class PhanAnhHienTruongRepository : ConnectDB, IPhanAnhHienTruongRepository
    {
        private readonly SqlConnection _conn;

        public PhanAnhHienTruongRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectDataMain();
        }

        public async Task<PhanAnhHienTruong> GetByDongBoID(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PhanAnhID", id);
                    PhanAnhHienTruong list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruong>(conn, "SP_PhanAnhHienTruongGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<PhanAnhHienTruong>> GetsPhanAnhHienTruongByLoaiXuLy(int loaixuly)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiXuLy", loaixuly);
                    IEnumerable<PhanAnhHienTruong> list = await SqlMapper.QueryAsync<PhanAnhHienTruong>(conn, "SP_PhanAnhHienTruongGetsByLoaiXuLy", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<PhanAnhHienTruong>> GetsPhanAnhHienTruongByMaDinhDanh(string madinhdanh)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<PhanAnhHienTruong> list = conn.Query<PhanAnhHienTruong>("SP_PhanAnhHienTruongGetsMaDinhDanh", commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<PhanAnhHienTruongTrinhDien>> GetsPhanAnhHienTruongTrinhDien(PhanAnhHienTruongTrinhDienRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Keyword", request.Keywork);
                    parameters.Add("@LinhVuc", request.LinhVuc);
                    parameters.Add("@TuNgay", request.TuNgay);
                    parameters.Add("@DenNgay", request.DenNgay);
                    parameters.Add("@LoaiXuLy", request.LoaiXuLy);
                    IEnumerable<PhanAnhHienTruongTrinhDien> list = conn.Query<PhanAnhHienTruongTrinhDien>("SP_PhanAnhHienTruongGets", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruongTrinhDien> GetPhanAnhHienTruongTrinhDien(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);

                    PhanAnhHienTruongTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruongTrinhDien>(conn, "SP_PhanAnhHienTruongGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruong> InsertPhanAnhHienTruong(PhanAnhHienTruong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaLinhVuc", data.MaLinhVuc);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@NguoiGui", data.NguoiGui);
                    parameters.Add("@NgayGui", data.NgayGui);
                    parameters.Add("@MaCoQuanXuLy", data.MaCoQuanXuLy);
                    parameters.Add("@YKienXuLy", data.YKienXuLy);
                    parameters.Add("@NgayXuLy", data.NgayXuLy);
                    parameters.Add("@LoaiXuLy", data.LoaiXuLy);
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@DiaChiSuKien", data.DiaChiSuKien);
                    parameters.Add("@TenCoQuan", data.TenCoQuan);
                    parameters.Add("@NgayTao", data.NgayTao);
                    parameters.Add("@PhanAnhID", data.PhanAnhId);
                    PhanAnhHienTruong list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruong>(conn, "SP_PhanAnhHienTruongAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruong> UpdatePhanAnhHienTruong(PhanAnhHienTruong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@MaLinhVuc", data.MaLinhVuc);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@NguoiGui", data.NguoiGui);
                    parameters.Add("@NgayGui", data.NgayGui);
                    parameters.Add("@MaCoQuanXuLy", data.MaCoQuanXuLy);
                    parameters.Add("@YKienXuLy", data.YKienXuLy);
                    parameters.Add("@NgayXuLy", data.NgayXuLy);
                    parameters.Add("@LoaiXuLy", data.LoaiXuLy);
                    parameters.Add("@TieuDe", data.TieuDe);
                    parameters.Add("@DiaChiSuKien", data.DiaChiSuKien);
                    parameters.Add("@TenCoQuan", data.TenCoQuan);
                    parameters.Add("@NgayTao", data.NgayTao);
                    PhanAnhHienTruong list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruong>(conn, "SP_PhanAnhHienTruongEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<PhanAnhHienTruong> Edit(PhanAnhHienTruongEditRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.ID);
                    parameters.Add("@MaDiaDiem", data.MaDiaDiem);
                    parameters.Add("@MaLoaiDuLieu", data.MaLoaiDuLieu);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    PhanAnhHienTruong list = await SqlMapper.QueryFirstOrDefaultAsync<PhanAnhHienTruong>(conn, "SP_PhanAnhHienTruongEditMap", parameters, commandType: CommandType.StoredProcedure);
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
    
        public async Task<IEnumerable<PhanAnhHienTruongTrinhDien>> GetsByLinhVuc(int linhvucId) 
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LinhVucID", linhvucId);
                    IEnumerable<PhanAnhHienTruongTrinhDien> list = await SqlMapper.QueryAsync<PhanAnhHienTruongTrinhDien>(conn, "SP_PhanAnhHienTruongGetsByLinhVuc", parameters, commandType: CommandType.StoredProcedure);
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
                    int result = await SqlMapper.ExecuteAsync(conn, "SP_PhanAnhHienTruongDelete", parameters, commandType: CommandType.StoredProcedure);

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
