using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class DanhMucRepository : ConnectDB, IDanhMucRepository
    {
        private readonly SqlConnection _conn;
        public DanhMucRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        #region LOẠI ĐỊA ĐIỂM ĂN UỐNG
        public async Task<IEnumerable<LoaiDiaDiemAnUong>> GetsLoaiDiaDiemAnUong()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<LoaiDiaDiemAnUong> list = conn.Query<LoaiDiaDiemAnUong>("SP_LoaiDiaDiemAnUongGets", commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiaDiemAnUong> GetLoaiDiaDiemAnUong(int id)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    LoaiDiaDiemAnUong list = conn.QueryFirstOrDefault<LoaiDiaDiemAnUong>("SP_LoaiDiaDiemAnUongGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiaDiemAnUong> GetLoaiDiaDiemAnUongDongBo(int? dongboId)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", dongboId);
                    LoaiDiaDiemAnUong list = conn.QueryFirstOrDefault<LoaiDiaDiemAnUong>("SP_LoaiDiaDiemAnUongGetDongBo", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiaDiemAnUong> InsertLoaiDiaDiemAnUong(LoaiDiaDiemAnUong data)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@MoTa", data.MoTa);
                    LoaiDiaDiemAnUong list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiDiaDiemAnUong>(conn, "SP_LoaiDiaDiemAnUongAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiaDiemAnUong> UpdateLoaiDiaDiemAnUong(LoaiDiaDiemAnUong data)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@IsDelete", data.IsDelete);
                    parameters.Add("@MoTa", data.MoTa);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    LoaiDiaDiemAnUong list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiDiaDiemAnUong>(conn, "SP_LoaiDiaDiemAnUongEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiaDiemAnUong> UpdateLoaiDiaDiemAnUongDongBoID(int id, int? dongboId)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    parameters.Add("@DongBoID", dongboId);
                    LoaiDiaDiemAnUong list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiDiaDiemAnUong>(conn, "SP_LoaiDiaDiemAnUongUpdateDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteLoaiDiaDiemAnUong(int id)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LoaiDiaDiemAnUongDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region LOẠI ẨM THỰC ĐỊA ĐIỂM ĂN UỐNG
        public async Task<IEnumerable<LoaiAmThucDiaDiemAnUong>> GetsLoaiAmThucDiaDiemAnUong()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<LoaiAmThucDiaDiemAnUong> list = conn.Query<LoaiAmThucDiaDiemAnUong>("SP_LoaiAmThucDiaDiemAnUongGets", commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThucDiaDiemAnUong> GetLoaiAmThucDiaDiemAnUong(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    LoaiAmThucDiaDiemAnUong list = conn.QueryFirst<LoaiAmThucDiaDiemAnUong>("SP_LoaiAmThucDiaDiemAnUongGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThucDiaDiemAnUong> InsertLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenLoaiAmThuc", data.TenLoaiAmThuc);
                    parameters.Add("@MoTa", data.MoTa);
                    LoaiAmThucDiaDiemAnUong list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiAmThucDiaDiemAnUong>(conn, "SP_LoaiAmThucDiaDiemAnUongAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThucDiaDiemAnUong> UpdateLoaiAmThucDiaDiemAnUong(LoaiAmThucDiaDiemAnUong data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@TenLoaiAmThuc", data.TenLoaiAmThuc);
                    parameters.Add("@MoTa", data.MoTa);
                    LoaiAmThucDiaDiemAnUong list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiAmThucDiaDiemAnUong>(conn, "SP_LoaiAmThucDiaDiemAnUongEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteLoaiAmThucDiaDiemAnUong(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LoaiAmThucDiaDiemAnUongDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region LOẠI ẨM THỰC
        public async Task<IEnumerable<LoaiAmThuc>> GetsLoaiAmThuc()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<LoaiAmThuc> list = conn.Query<LoaiAmThuc>("SP_LoaiAmThucGets", commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> GetLoaiAmThuc(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    LoaiAmThuc list = conn.QueryFirstOrDefault<LoaiAmThuc>("SP_LoaiAmThucGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> GetByDongBoID (int? dongBoId)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", dongBoId);
                    LoaiAmThuc list = conn.QueryFirstOrDefault<LoaiAmThuc>("SP_LoaiAmThucGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> GetByTenLoai(string tenloai)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenLoai", tenloai);
                    LoaiAmThuc list = conn.QueryFirstOrDefault<LoaiAmThuc>("SP_LoaiAmThucGetByTenLoai", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> InsertLoaiAmThuc(LoaiAmThuc data)
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
                    LoaiAmThuc list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiAmThuc>(conn, "SP_LoaiAmThucAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> UpdateLoaiAmThuc(LoaiAmThuc data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@TenLoai", data.TenLoai);
                    parameters.Add("@IsDelete", data.IsDelete);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    LoaiAmThuc list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiAmThuc>(conn, "SP_LoaiAmThucEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiAmThuc> EditDongBoID(int id, int? dongboId)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    parameters.Add("@DongBoID", dongboId);
                    LoaiAmThuc list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiAmThuc>(conn, "SP_LoaiAmThucEditDongBo", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteLoaiAmThuc(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LoaiAmThucDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region CHỦ ĐỀ SỰ KIỆN
        public async Task<IEnumerable<ChuDeSuKien>> GetsChuDeSuKien()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<ChuDeSuKien> list = conn.Query<ChuDeSuKien>("SP_SuKienChuDeGets", commandType: CommandType.StoredProcedure);
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

        public async Task<ChuDeSuKien> GetChuDeSuKien(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    ChuDeSuKien list = conn.QueryFirstOrDefault<ChuDeSuKien>("SP_SuKienChuDeGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ChuDeSuKien> InsertChuDeSuKien(ChuDeSuKien data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ChuDe", data.ChuDe);
                    ChuDeSuKien list = await SqlMapper.QueryFirstOrDefaultAsync<ChuDeSuKien>(conn, "SP_SuKienChuDeAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ChuDeSuKien> UpdateChuDeSuKien(ChuDeSuKien data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID);
                    parameters.Add("@ChuDe", data.ChuDe);
                    ChuDeSuKien list = await SqlMapper.QueryFirstOrDefaultAsync<ChuDeSuKien>(conn, "SP_SuKienChuDeEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteChuDeSuKien(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_SuKienChuDeDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region DANH MỤC
        public async Task<IEnumerable<DanhMuc>> GetsDanhMuc(int id)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LinhVucKinhDoanh", id);
                    IEnumerable<DanhMuc> list = conn.Query<DanhMuc>("SP_DanhMucGets", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DanhMuc> GetDanhMuc(int id)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DanhMuc list = conn.QueryFirstOrDefault<DanhMuc>("SP_DanhMucGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DanhMuc> InsertDanhMuc(DanhMuc data)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Ten", data.Ten);
                    parameters.Add("@LoaiId", data.LoaiId);
                    parameters.Add("@MoTa", data.MoTa);
                    parameters.Add("@IsStatus", true);
                    parameters.Add("@IsDelete", false);
                    DanhMuc list = await SqlMapper.QueryFirstOrDefaultAsync<DanhMuc>(conn, "SP_DanhMucAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DanhMuc> UpdateDanhMuc(DanhMuc data)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@Ten", data.Ten);
                    parameters.Add("@MoTa", data.MoTa);
                    DanhMuc list = await SqlMapper.QueryFirstOrDefaultAsync<DanhMuc>(conn, "SP_DanhMucEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteDanhMuc(int id)
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_DanhMucDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region LOẠI LỄ HỘI
        public async Task<IEnumerable<LoaiLeHoiTrinhDien>> GetsLoaiLeHoi()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<LoaiLeHoiTrinhDien> list = conn.Query<LoaiLeHoiTrinhDien>("SP_LeHoiLoaiGets", commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiLeHoiTrinhDien> GetLoaiLeHoi(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    LoaiLeHoiTrinhDien list = conn.QueryFirstOrDefault<LoaiLeHoiTrinhDien>("SP_LeHoiLoaiGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiLeHoiTrinhDien> GetLoaiLeHoiByDongBo(int? id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    LoaiLeHoiTrinhDien list = conn.QueryFirstOrDefault<LoaiLeHoiTrinhDien>("SP_LeHoiLoaiGetDongBo", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiLeHoiModel> AddLoaiLeHoi(LoaiLeHoiModel model)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Ten", model.Ten);
                    parameters.Add("@DongBoID", model.DongBoID);
                    parameters.Add("@NguonDongBo", model.NguonDongBo);
                    LoaiLeHoiModel list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiLeHoiModel>(conn, "SP_LeHoiLoaiAdd", parameters, commandType: CommandType.StoredProcedure);
                    
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

        public async Task<LoaiLeHoiModel> EditLoaiLeHoi(LoaiLeHoiModel data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.Id);
                    parameters.Add("@Ten", data.Ten);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@IsDelete", data.IsDelete);
                    LoaiLeHoiModel list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiLeHoiModel>(conn, "SP_LeHoiLoaiEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteLoaiLeHoi(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LeHoiLoaiDelete", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

        #region LOẠI ĐIỂM GIAO DỊCH
        public async Task<IEnumerable<LoaiDiemGiaoDichTrinhDien>> GetsLoaiDiemGiaoDich()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<LoaiDiemGiaoDichTrinhDien> list = conn.Query<LoaiDiemGiaoDichTrinhDien>("SP_LoaiDiemGiaoDichGets", commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiemGiaoDichTrinhDien> GetLoaiDiemGiaoDich(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    LoaiDiemGiaoDichTrinhDien list = conn.QueryFirstOrDefault<LoaiDiemGiaoDichTrinhDien>("SP_LoaiDiemGiaoDichGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiemGiaoDich> AddLoaiDiemGiaoDich(LoaiDiemGiaoDich model)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Ten", model.Ten);
                    parameters.Add("@DongBoID", model.DongBoID);
                    parameters.Add("@NguonDongBo", model.NguonDongBo);
                    LoaiDiemGiaoDich list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiDiemGiaoDich>(conn, "SP_LoaiDiemGiaoDichAdd", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<LoaiDiemGiaoDich> EditLoaiDiemGiaoDich(LoaiDiemGiaoDich data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.Id);
                    parameters.Add("@Ten", data.Ten);
                    parameters.Add("@DongBoID", data.DongBoID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@IsDelete", data.IsDelete);
                    LoaiDiemGiaoDich list = await SqlMapper.QueryFirstOrDefaultAsync<LoaiDiemGiaoDich>(conn, "SP_LoaiDiemGiaoDichEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteLoaiDiemGiaoDich(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LoaiDiemGiaoDichDelete", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LoaiDiemGiaoDichTrinhDien> GetLoaiDiemGiaoDichByDongBo(int? id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    LoaiDiemGiaoDichTrinhDien list = conn.QueryFirstOrDefault<LoaiDiemGiaoDichTrinhDien>("SP_LoaiDiemGiaoDichGetDongBo", parameters, commandType: CommandType.StoredProcedure);
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
        #endregion

    }
}
