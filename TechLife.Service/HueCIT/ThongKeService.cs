using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechLife.Common;
using TechLife.Common.Enums;
using TechLife.Common.Enums.HueCIT;
using TechLife.Model;
using TechLife.Model.HueCIT;
using TechLife.Service.Common;

namespace TechLife.Service.HueCIT
{
    public interface IThongKeService
    {
        Task<int> DiaDiemAnUong();
        Task<int> KhuVuiChoi();
        Task<int> DiSanVanHoa();
        Task<int> VeSinhCongCong();
        Task<int> DiemGiaoDich();
    }

    public class ThongKeService : Connect, IThongKeService
    {
        private readonly SqlConnection _conn;

        public ThongKeService(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
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
    }
}