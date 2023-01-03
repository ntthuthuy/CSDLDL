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
    public class DoanhNghiepRepository : ConnectDB, IDoanhNghiepRepository
    {
        private readonly SqlConnection _conn;
        public DoanhNghiepRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }

        public async Task<DoanhNghiepTrinhDien> GetByDongBoID(string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DongBoID", id);
                    DoanhNghiepTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrinhDien>(conn, "SP_DoanhNghiepGetByDongBoID", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiepTrinhDien> Get(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    DoanhNghiepTrinhDien list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiepTrinhDien>(conn, "SP_DoanhNghiepGet", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DoanhNghiepTrinhDien>> Gets(DoanhNghiepRequest req)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TuKhoa", req.tukhoa);
                    parameters.Add("@Loai", req.loai);
                    parameters.Add("@DiaChi", req.diachi);
                    parameters.Add("@NganhNghe", req.nganhnghe);
                    parameters.Add("@Huyen", req.huyen);
                    IEnumerable<DoanhNghiepTrinhDien> list = await SqlMapper.QueryAsync<DoanhNghiepTrinhDien>(conn, "SP_DoanhNghiepGets", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiep> Insert(DoanhNghiep data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MaSoDoanhNghiep", data.MaSoDoanhNghiep);
                    parameters.Add("@NgayThanhLap", data.NgayThanhLap);
                    parameters.Add("@TenDoanhNghiep", data.TenDoanhNghiep);
                    parameters.Add("@NguoiDaiDien", data.NguoiDaiDien);
                    parameters.Add("@DiaChi", data.DiaChi);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@HopThu", data.HopThu);
                    parameters.Add("@TrangChu", data.TrangChu);
                    parameters.Add("@MaLoaiHinh", data.MaLoaiHinh);
                    parameters.Add("@MaNganhNgheChinh", data.MaNganhNgheChinh);
                    parameters.Add("@MaTrangThai", data.MaTrangThai);
                    parameters.Add("@IDQuanHuyen", data.IDQuanHuyen);
                    parameters.Add("@IDPhuongXa", data.IDPhuongXa);
                    parameters.Add("@NguonDongBo", data.NguonDongBo);
                    parameters.Add("@DongBoID", data.DongBoID);
                    DoanhNghiep list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiep>(conn, "SP_DoanhNghiepAdd", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<DoanhNghiep> Update(DoanhNghiep data)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", data.Id);
                    parameters.Add("@MaSoDoanhNghiep", data.MaSoDoanhNghiep);
                    parameters.Add("@TenDoanhNghiep", data.TenDoanhNghiep);
                    parameters.Add("@NgayThanhLap", data.NgayThanhLap);
                    parameters.Add("@NguoiDaiDien", data.NguoiDaiDien);
                    parameters.Add("@DiaChi", data.DiaChi);
                    parameters.Add("@X", data.X);
                    parameters.Add("@Y", data.Y);
                    parameters.Add("@DienThoai", data.DienThoai);
                    parameters.Add("@HopThu", data.HopThu);
                    parameters.Add("@TrangChu", data.TrangChu);
                    parameters.Add("@MaLoaiHinh", data.MaLoaiHinh);
                    parameters.Add("@MaNganhNgheChinh", data.MaNganhNgheChinh);
                    parameters.Add("@MaTrangThai", data.MaTrangThai);
                    parameters.Add("@IDQuanHuyen", data.IDQuanHuyen);
                    parameters.Add("@IDPhuongXa", data.IDPhuongXa);
                    DoanhNghiep list = await SqlMapper.QueryFirstOrDefaultAsync<DoanhNghiep>(conn, "SP_DoanhNghiepEdit", parameters, commandType: CommandType.StoredProcedure);
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
