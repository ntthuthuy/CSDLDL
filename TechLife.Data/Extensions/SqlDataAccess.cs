using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLife.Data.Extensions
{
    public class SqlDataAccess
    {
        private DbConnection _conn;
        public SqlDataAccess(DbContext context)
        {
            _conn = context.Database.GetDbConnection();
        }
        public async Task intConn()
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    await _conn.OpenAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DataTable> ExecuteSqlTextDataTable(string mySql, object commandParameters)
        {
            DataTable dt = new DataTable();

            try
            {
                await intConn();

                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = mySql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 50;
                    if (commandParameters != null)
                    {

                        if (commandParameters.GetType() == typeof(SqlParameter[]))
                        {
                            cmd.Parameters.AddRange((SqlParameter[])commandParameters);
                        }
                        else if (commandParameters.GetType() == typeof(List<SqlParameter>))
                        {
                            foreach (var item in (List<SqlParameter>)commandParameters)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }
                        else
                        {
                            return null;

                        }
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                await _conn.CloseAsync();
                commandParameters = null;
            }
            return dt;
        }
        public async Task<DataTable> ExecuteReaderProcedure(string proName, object commandParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    await _conn.OpenAsync();
                }
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = proName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 50;
                    if (commandParameters != null)
                    {
                        if (commandParameters.GetType() == typeof(SqlParameter[]))
                        {
                            cmd.Parameters.AddRange((SqlParameter[])commandParameters);
                        }
                        else if (commandParameters.GetType() == typeof(List<SqlParameter>))
                        {
                            foreach (var item in (List<SqlParameter>)commandParameters)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                await _conn.CloseAsync();
                commandParameters = null;
            }
            return dt;
        }
        public async Task<int> ExecuteInsertSqlTextNoQuery(IDbContextTransaction transObj, string mySql, List<SqlParameter> commandParameters)
        {
            DbTransaction trans = transObj.GetDbTransaction();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    await _conn.OpenAsync();
                }

                using (var cmd = trans.Connection.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = mySql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 50;
                    if (commandParameters != null)
                    {
                        for (int i = 0; i < commandParameters.Count(); i++)
                        {
                            cmd.Parameters.Add(commandParameters[i]);
                        }
                    }
                    if (trans.Connection.State == ConnectionState.Closed)
                    {
                        await trans.Connection.OpenAsync();
                    }
                    object temp = await cmd.ExecuteScalarAsync();

                    if (temp != null)
                    {
                        return int.Parse(temp.ToString());
                    }
                    else
                    {
                        await trans.RollbackAsync();
                        await trans.DisposeAsync();
                        await _conn.CloseAsync();
                        await _conn.DisposeAsync();
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                await trans.DisposeAsync();
                await _conn.CloseAsync();
                await _conn.DisposeAsync();
                return 0;
            }
        }
        public async Task<bool> ExecuteUpdateSqlTextNoQuery(IDbContextTransaction transObj, string mySql, List<SqlParameter> commandParameters)
        {
            DbTransaction trans = transObj.GetDbTransaction();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    await _conn.OpenAsync();
                }

                using (var cmd = trans.Connection.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = mySql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 50;
                    if (commandParameters != null)
                    {
                        for (int i = 0; i < commandParameters.Count(); i++)
                        {
                            cmd.Parameters.Add(commandParameters[i]);
                        }
                    }
                    if (trans.Connection.State == ConnectionState.Closed)
                    {
                        await trans.Connection.OpenAsync();
                    }
                    var result = await cmd.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        await trans.RollbackAsync();
                        await trans.DisposeAsync();
                        await _conn.CloseAsync();
                        await _conn.DisposeAsync();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                await trans.DisposeAsync();
                await _conn.CloseAsync();
                await _conn.DisposeAsync();
                return false;
            }
        }
        public async Task<bool> CommitTransaction(IDbContextTransaction transObj)
        {
            bool resule = false;
            DbTransaction trans = transObj.GetDbTransaction();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    await _conn.OpenAsync();
                }
                await trans.CommitAsync();
                resule = true;
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                resule = false;
            }
            finally
            {
                await transObj.GetDbTransaction().DisposeAsync();
                await _conn.CloseAsync();
                await _conn.DisposeAsync();
            }
            return resule;
        }
    }
}
