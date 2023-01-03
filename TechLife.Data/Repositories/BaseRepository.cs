using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TechLife.Data.Extensions;

namespace TechLife.Data.Repositories
{
    public class BaseRepository
    {
        public Dictionary<string, object> prams;

        private DbContext _context;
        private SqlDataAccess _sqlAccess;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _sqlAccess = new SqlDataAccess(_context);
        }
        protected async Task intConn()
        {
            try
            {
                await _sqlAccess.intConn();
            }
            catch (Exception ex)
            {
                string me = ex.Message;
            }
        }
        protected async Task<IDbContextTransaction> CreateTransaction()
        {
            try
            {
                await intConn();

                return await _context.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected async Task<List<T>> GetAllByProc<T>(string procName, Dictionary<string, object> condition) where T : class, new()
        {
            var lst = new List<T>();
            try
            {
                var t = new T();
                var tableName = t.GetType().Name;

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
                        var checkHasProp = t.GetType().GetProperty(prop) != null;
                        //if (!checkHasProp)
                        //    return null;

                        param.Add(new SqlParameter(prop, value));
                    }
                }

                var dt = await _sqlAccess.ExecuteReaderProcedure(procName, param);
                return ObjectHelper.ToListData<T>(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected async Task<List<T>> GetAllByQueryTable<T>(Dictionary<string, object> condition) where T : class, new()
        {
            try
            {
                var t = new T();
                var tableName = AttributeReader.GetTableName<T>(_context);

                var sql = "select * from " + tableName + " where 1 = 1";
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
                        var checkHasProp = t.GetType().GetProperty(prop) != null;
                        if (!checkHasProp)
                            return null;
                        sql += " and " + key + " = @p_" + prop;
                        param.Add(new SqlParameter("p_" + prop, value));
                    }
                }

                var dt = await _sqlAccess.ExecuteSqlTextDataTable(sql, param);

                return ObjectHelper.ToListData<T>(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected async Task<T> GetItem<T>(string key, object value) where T : class, new()
        {
            var t = new T();
            var tableName = AttributeReader.GetTableName<T>(_context);

            var checkHasProp = t.GetType().GetProperty(key) != null;
            if (!checkHasProp)
                return null;

            var sql = "select * from " + tableName + " where " + key + " = @p_" + key;
            var param = new List<SqlParameter>()
            {
                new SqlParameter("p_"+key, value)
            };

            try
            {
                var dt = await _sqlAccess.ExecuteSqlTextDataTable(sql, param);
                if (dt == null || dt.Rows.Count == 0)
                    return null;
                var lstObj = ObjectHelper.ToListData<T>(dt);
                if (lstObj == null || lstObj.Count == 0)
                    return null;
                return lstObj[0];
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        protected async Task<int> InsertItem<T>(IDbContextTransaction transObj, T t) where T : class, new()
        {
            try
            {
                List<SqlParameter> param;

                var sqlInsert = t.BuildSqlInsert(out param, _context);

                return await _sqlAccess.ExecuteInsertSqlTextNoQuery(transObj, sqlInsert, param);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        protected async Task<bool> UpdateItem<T>(IDbContextTransaction transObj, T t, string key) where T : class, new()
        {
            try
            {
                List<SqlParameter> param;

                var sqlUpdate = t.BuildSqlUpdate(out param, _context, key);

                return await _sqlAccess.ExecuteUpdateSqlTextNoQuery(transObj, sqlUpdate, param);

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        protected async Task<bool> CommitTransaction(IDbContextTransaction transObj)
        {
            bool resule = false;
            try
            {
                resule = await _sqlAccess.CommitTransaction(transObj);
            }
            catch (Exception ex)
            {
                resule = false;
            }
            return resule;
        }
    }
}
