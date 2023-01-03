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
    public class LeHoiRepository : ConnectDB, ILeHoiRepository
    {
        private readonly SqlConnection _conn;
        public LeHoiRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<IEnumerable<LeHoiTrinhDien>> Gets(LeHoiRequest data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@Cap", data.Cap);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    IEnumerable<LeHoiTrinhDien> list = conn.Query<LeHoiTrinhDien>("SP_LeHoiGets", param: parameters, commandType: CommandType.StoredProcedure);
                    IEnumerable<DiaPhuong> dp = await GetsDiaPhuong();
                    foreach (LeHoiTrinhDien l in list)
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

        public async Task<LeHoiTrinhDien> Get(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    LeHoiTrinhDien list = conn.QueryFirstOrDefault<LeHoiTrinhDien>("SP_LeHoiGet", parameters, commandType: CommandType.StoredProcedure);
                    IEnumerable<DiaPhuong> dp = await GetsDiaPhuong();

                    if (Enum.IsDefined(typeof(CapQuanLyLeHoi), list.Cap))
                    {
                        list.CapQuanLy = StringEnum.GetStringValue(list.Cap);
                    }
                    else
                    {
                        list.CapQuanLy = "Không xác định";
                    }

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

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@LoaiDoiTuong", "CN_LeHoi");
                    param.Add("@IDDoiTuong", id);
                    List<FileUpload> files = (conn.Query<FileUpload>("SP_ThuVienMediaGets", param, commandType: CommandType.StoredProcedure)).ToList();
                    list.Files = files;

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
        public async Task<LeHoiTrinhDien> GetByLeHoiID(int? id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LeHoiID", id);
                    LeHoiTrinhDien list = conn.QueryFirstOrDefault<LeHoiTrinhDien>("SP_LeHoiGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
                    if (list != null)
                    {
                        if (Enum.IsDefined(typeof(CapQuanLyLeHoi), list.Cap))
                        {
                            list.CapQuanLy = StringEnum.GetStringValue(list.Cap);
                        }
                        else
                        {
                            list.CapQuanLy = "Không xác định";
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

        public async Task<LeHoi> Add(LeHoi data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TenLeHoi", data.TenLeHoi);
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@Cap", data.Cap);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@LeHoiID", data.LeHoiID);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    LeHoi list = await SqlMapper.QueryFirstOrDefaultAsync<LeHoi>(conn, "SP_LeHoiAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<LeHoi> Edit(LeHoi data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", data.ID.ToString());
                    parameters.Add("@TenLeHoi", data.TenLeHoi);
                    parameters.Add("@Loai", data.Loai);
                    parameters.Add("@Cap", data.Cap);
                    parameters.Add("@NoiDung", data.NoiDung);
                    parameters.Add("@BatDau", data.BatDau);
                    parameters.Add("@KetThuc", data.KetThuc);
                    parameters.Add("@DiaDiem", data.DiaDiem);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@PhuongXaId", data.PhuongXaId);
                    parameters.Add("@QuanHuyenId", data.QuanHuyenId);
                    LeHoi list = await SqlMapper.QueryFirstOrDefaultAsync<LeHoi>(conn, "SP_LeHoiEdit", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> Delete(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_LeHoiDelete", parameters, commandType: CommandType.StoredProcedure);
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
