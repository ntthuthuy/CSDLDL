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

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class FileDongBoRepository : ConnectDB, IFileDongBoRepository
    {
        public FileDongBoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<FileUpload>> GetsDongBo(string table, string id, int nguondongbo)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiDoiTuong", table);
                    parameters.Add("@IDDoiTuong", id);
                    parameters.Add("@NguonDongBo", nguondongbo);
                    IEnumerable<FileUpload> list = conn.Query<FileUpload>("SP_ThuVienMediaGetsDongBo", parameters, commandType: CommandType.StoredProcedure);
                    return list.ToList();

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

        public async Task<int> DeleteWithparentDongBo(string table, string id, int nguondongbo)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiDoiTuong", table);
                    parameters.Add("@IDDoiTuong", id);
                    parameters.Add("@NguonDongBo", nguondongbo);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_ThuVienMediaDeleteWithParentDongBo", parameters, commandType: CommandType.StoredProcedure);
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
    }
}
