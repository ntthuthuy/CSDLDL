using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechLife.Common;

namespace TechLife.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<List<T>> RawQuery<T>(this DbContext context, string sql, Dictionary<string, object> condition)
        {
            try
            {
                var param = new List<SqlParameter>();
                if (condition != null && condition.Count > 0)
                {
                    foreach (var item in condition)
                    {
                        var key = item.Key;
                        var value = item.Value;
                        var prop = key.Trim();
                        if (prop.Contains("LOWER"))
                        {
                            prop = prop.Replace("LOWER", "").Replace("(", "").Replace(")", "");
                        }
                        if (prop.Contains("UPPER"))
                        {
                            prop = prop.Replace("UPPER", "").Replace("(", "").Replace(")", "");
                        }

                        param.Add(new SqlParameter("@" + prop, value));
                    }
                }
                return await ExecuteQuery<T>(context, sql, param);
            }
            catch
            {
                return new List<T>();
            }
        }

        public static async Task<T> RawQuery<T>(this DbContext context, string sql, string key, string id) where T : new()
        {
            try
            {
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@" + key, id));
                return await ExecuteQueryByKey<T>(context, sql, param);
            }
            catch
            {
                return new T();
            }
        }

        public static async Task<List<T>> RawProcedure<T>(this DbContext context, string procName, Dictionary<string, object> condition)
        {
            try
            {
                var param = new List<SqlParameter>();
                if (condition != null && condition.Count > 0)
                {
                    foreach (var item in condition)
                    {
                        var key = item.Key;
                        var value = item.Value;
                        var prop = key.Trim();
                        if (prop.Contains("LOWER"))
                        {
                            prop = prop.Replace("LOWER", "").Replace("(", "").Replace(")", "");
                        }
                        if (prop.Contains("UPPER"))
                        {
                            prop = prop.Replace("UPPER", "").Replace("(", "").Replace(")", "");
                        }

                        param.Add(new SqlParameter("@" + prop, value));
                    }
                }
                return await ExecuteProcedure<T>(context, procName, param);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        private static async Task<List<T>> ExecuteQuery<T>(DbContext context, string sql, object commandParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Closed)
                {
                    await context.Database.GetDbConnection().OpenAsync();
                }
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = sql;
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
            catch
            {
                dt = new DataTable();
            }
            finally
            {
                await context.Database.GetDbConnection().CloseAsync();
            }
            return dt.ToListData<T>();
        }

        private static async Task<T> ExecuteQueryByKey<T>(DbContext context, string sql, object commandParameters) where T : new()
        {
            DataTable dt = new DataTable();
            try
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Closed)
                {
                    await context.Database.GetDbConnection().OpenAsync();
                }
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = sql;
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
                            return new T();
                        }
                    }
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch
            {
                dt = new DataTable();
            }
            finally
            {
                await context.Database.GetDbConnection().CloseAsync();
            }
            return dt.ToListData<T>()[0];
        }

        private static async Task<List<T>> ExecuteProcedure<T>(DbContext context, string proName, object commandParameters)
        {
            DataTable dt = new DataTable();

            try
            {
                if (context.Database.GetDbConnection().State == ConnectionState.Closed)
                {
                    await context.Database.GetDbConnection().OpenAsync();
                }
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
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
                dt = new DataTable();
            }
            finally
            {
                await context.Database.GetDbConnection().CloseAsync();
            }
            return dt.ToListData<T>();
        }
    }
}