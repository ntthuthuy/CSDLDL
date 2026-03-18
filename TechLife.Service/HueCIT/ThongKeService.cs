using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Common.Enums;
using TechLife.Data;
using TechLife.Model.ThongKe;

namespace TechLife.Service.HueCIT
{
    public interface IThongKeService
    {
        Task<int> DiaDiemAnUong();
        Task<int> KhuVuiChoi();
        Task<int> DiSanVanHoa();
        Task<int> VeSinhCongCong();
        Task<int> DiemGiaoDich();
        Task<int> SoPhong();
        Task<int> SoGiuong();
        Task<List<ThongKeVm>> CountModifiedByYear(int year);
    }

    public class ThongKeService : Connect, IThongKeService
    {
        private readonly SqlConnection _conn;
        private readonly TLDbContext _context;

        public ThongKeService(IConfiguration configuration, TLDbContext context) : base(configuration)
        {
            _conn = IConnectData();
            _context = context;
        }

        public async Task<int> DiaDiemAnUong()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    int res = conn.QuerySingle<int>("SP_ThongKe_DiaDiemAnUong", commandType: CommandType.StoredProcedure);
                    return res;

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

        public async Task<int> KhuVuiChoi()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    int res = conn.QuerySingle<int>("SP_ThongKe_KhuVuiChoi", commandType: CommandType.StoredProcedure);
                    return res;

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

        public async Task<int> DiSanVanHoa()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    int res = conn.QuerySingle<int>("SP_ThongKe_DiSanVanHoa", commandType: CommandType.StoredProcedure);
                    return res;

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

        public async Task<int> VeSinhCongCong()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    int res = conn.QuerySingle<int>("SP_ThongKe_DiemVeSinh", commandType: CommandType.StoredProcedure);
                    return res;

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

        public async Task<int> DiemGiaoDich()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    int res = conn.QuerySingle<int>("SP_ThongKe_DiemGiaoDich", commandType: CommandType.StoredProcedure);
                    return res;

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

        public async Task<List<ThongKeVm>> CountModifiedByYear(int year)
        {
            try
            {
                var result = await _context.HoSo
                    .Where(x => !x.IsDelete)
                    .GroupBy(g => g.LinhVucKinhDoanhId)
                    .Select(x => new ThongKeVm
                    {
                        LinhVucKinhDoanhId = x.Key,
                        Create = x.Where(v => v.CreateOnDate.Year == DateTime.Now.Year).Count(),
                        Update = x.Where(v => v.LastModifiedOnDate.Year == DateTime.Now.Year).Count()
                    }).ToListAsync();

                var queryHDV = _context.HuongDanVien.Where(x => !x.IsDelete);

                var countHDV = new ThongKeVm
                {
                    LinhVucKinhDoanhId = (int)LinhVucKinhDoanh.HuongDanVien,
                    Create = await queryHDV.Where(x => x.CreateOnDate.HasValue && x.CreateOnDate.Value.Year == DateTime.Now.Year).CountAsync(),
                    Update = await queryHDV.Where(x => x.CreateOnDate.HasValue && x.LastModifiedOnDate.Value.Year == DateTime.Now.Year).CountAsync()
                };

                result.Add(countHDV);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> SoPhong()
        {
            throw new NotImplementedException();
        }

        public Task<int> SoGiuong()
        {
            throw new NotImplementedException();
        }
    }
}