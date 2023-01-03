using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;
using TechLife.Data;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class DiaDiemAnUongRepositor : ConnectDB, IDiaDiemAnUongRepository
    {
        private readonly SqlConnection _conn;
        private readonly TLDbContext _context;
        public DiaDiemAnUongRepositor(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectDataMain();
        }
        public async Task<IEnumerable<DuLieuDuLichModel>> GetsDiaDiemAnUong()
        {
            using (SqlConnection conn = IConnectDataMain())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<DuLieuDuLichModel> list = conn.Query<DuLieuDuLichModel>("SP_LoaiDiaDiemAnUongGets", commandType: CommandType.StoredProcedure);
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
