using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TechLife.Data.Extensions
{
    public static class ObjectHelper
    {
        public static List<T> ToListData<T>(this DataTable dataTable) where T : class, new()
        {
            var dataList = new List<T>();
            dataList = JsonConvert.DeserializeObject<List<T>>(ToJsonData(dataTable));
            return dataList;
        }
        public static string ToJsonData(DataTable dataTable)
        {
            return JsonConvert.SerializeObject(dataTable);
        }

        public static string BuildSqlInsert<T>(this T t, out List<SqlParameter> param, DbContext context) where T : class, new()
        {
            var properties = t.GetType().GetProperties();
            param = new List<SqlParameter>();

            var tablename = AttributeReader.GetTableName<T>(context);

            var sql = "INSERT INTO " + tablename + " ( \n";
            var sqlParam = " VALUES (\n";

            var allowProperties =
                properties.Where(
                    o => !o.GetType().IsGenericType && !o.GetGetMethod().IsVirtual)
                    .ToArray();

            var key = AttributeReader.GetTableKey<T>(context);
            foreach (var propertyInfo in allowProperties)
            {
                var propName = propertyInfo.Name;
                var paramName = "P_" + propName;
                var value = t.GetType().GetProperty(propName).GetValue(t, null);
                if (value != null && propName != key)
                {
                    sql += propName + ", \n";
                    sqlParam += "@" + paramName + ", \n";
                    param.Add(new SqlParameter(paramName, value));
                }
            }
            var lastIndex = sql.LastIndexOf(',');
            sql = sql.Remove(lastIndex, 1) + ")\n";

            lastIndex = sqlParam.LastIndexOf(',');
            sqlParam = sqlParam.Remove(lastIndex, 1) + ")\n SELECT SCOPE_IDENTITY();";

            return sql + sqlParam;
        }
        public static string BuildSqlUpdate<T>(this T t, out List<SqlParameter> param, DbContext context, params string[] arCondition) where T : class, new()
        {
            var properties = t.GetType().GetProperties();   

            param = new List<SqlParameter>();
            var tablename = AttributeReader.GetTableName<T>(context);
            var sql = "UPDATE " + tablename + " SET \n";
            var sqlWhere = "WHERE 1 = 1";

            var allowProperties =
                properties.Where(
                    o => !o.GetType().IsGenericType && !o.GetGetMethod().IsVirtual)
                    .ToArray();


            foreach (var propertyInfo in allowProperties)
            {
                var propName = propertyInfo.Name;
                var paramName = "P_" + propName;
                var value = t.GetType().GetProperty(propName).GetValue(t, null);
                if (value != null )
                {
                    
                    if (arCondition.Contains(propName))
                    {
                        sqlWhere += " AND " + propName + " = @" + paramName + "\n";
                    }
                    else
                    {
                        sql += propName + " = @" + paramName + ",\n";
                    }
                    param.Add(new SqlParameter(paramName, value));
                }
               
            }
            var lastIndex = sql.LastIndexOf(',');
            sql = sql.Remove(lastIndex, 1);

            return sql + sqlWhere;
        }


        public static string BuildSqlDelete<T>(this T t, out List<SqlParameter> param, params string[] arCondition) where T : class, new()
        {
            var properties = t.GetType().GetProperties();
            param = new List<SqlParameter>();
            var tablename = t.GetType().Name;
            var sql = "DELETE " + tablename + " \n";
            var sqlWhere = "WHERE 1 = 1";

            var allowProperties =
                properties.Where(
                    o => !o.GetType().IsGenericType && !o.GetGetMethod().IsVirtual)
                    .ToArray();

            foreach (var propertyInfo in allowProperties)
            {
                var propName = propertyInfo.Name;
                var paramName = "P_" + propName;
                var value = t.GetType().GetProperty(propName).GetValue(t, null);

                if (arCondition.Contains(propName))
                {
                    sqlWhere += " AND " + propName + " = @" + paramName + "\n";
                }
                param.Add(new SqlParameter(paramName, value));
            }
            sql += ",\n";
            var lastIndex = sql.LastIndexOf(',');
            sql = sql.Remove(lastIndex, 1);

            return sql + sqlWhere;
        }

        public static string BuildSqlInsertList<T>(this T t, out List<SqlParameter> param) where T : class, new()
        {
            var properties = t.GetType().GetProperties();
            param = new List<SqlParameter>();
            var tablename = t.GetType().Name;
            var sql = "INSERT INTO " + tablename + " ( \n";
            var sqlParam = " VALUES (\n";

            var allowProperties =
                properties.Where(
                    o => !o.GetType().IsGenericType && !o.GetGetMethod().IsVirtual)
                    .ToArray();

            foreach (var propertyInfo in allowProperties)
            {
                var propName = propertyInfo.Name;
                var paramName = "P_" + propName;
                var value = t.GetType().GetProperty(propName).GetValue(t, null);
                if (value != null)
                {
                    sql += propName + ", \n";
                    sqlParam += "@" + paramName + ", \n";
                    param.Add(new SqlParameter(paramName, value));
                }
            }
            var lastIndex = sql.LastIndexOf(',');
            sql = sql.Remove(lastIndex, 1) + ")\n";

            lastIndex = sqlParam.LastIndexOf(',');
            sqlParam = sqlParam.Remove(lastIndex, 1) + ")\n";

            return sql + sqlParam;
        }

    }
}
