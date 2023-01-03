
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
    public class QuanHuyenRepository : ConnectDB, IQuanHuyenRepository
    {
        public QuanHuyenRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<QuanHuyen>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<QuanHuyen> list = await SqlMapper.QueryAsync<QuanHuyen>(conn, "SP_QuanHuyenGets", commandType: CommandType.StoredProcedure);
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
