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

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class FileRepository : ConnectDB, IFileRepository
    {
        private readonly SqlConnection _conn;
        public FileRepository(IConfiguration configuration) : base(configuration)
        {
            _conn = IConnectData();
        }
        public async Task<List<FileUpload>> Gets(string table, string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiDoiTuong", table);
                    parameters.Add("@IDDoiTuong", id);
                    IEnumerable<FileUpload> list = conn.Query<FileUpload>("SP_ThuVienMediaGets", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_ThuVienMediaDelete", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<int> DeleteWithparent(string table, string id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoaiDoiTuong", table);
                    parameters.Add("@IDDoiTuong", id);
                    int kq = await SqlMapper.ExecuteAsync(conn, "SP_ThuVienMediaDeleteWithParent", parameters, commandType: CommandType.StoredProcedure);
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
