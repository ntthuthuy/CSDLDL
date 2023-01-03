using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechLife.Data.Extensions
{
    public class AttributeReader
    {
        public static string GetTableName<T>(DbContext context) where T : class
        {
            // We need dbcontext to access the models
            var models = context.Model;

            // Get all the entity types information
            var entityTypes = models.GetEntityTypes();

            // T is Name of class
            var entityTypeOfT = entityTypes.First(t => t.ClrType == typeof(T));

            var key = entityTypes.First(t => t.ClrType == typeof(T)).FindPrimaryKey();
            var tableNameAnnotation = entityTypeOfT.GetAnnotation("Relational:TableName");
            var TableName = tableNameAnnotation.Value.ToString();
            return TableName;
        }
        public static string GetTableKey<T>(DbContext context) where T : class
        {
            // We need dbcontext to access the models
            var models = context.Model;

            // Get all the entity types information
            var entityTypes = models.GetEntityTypes();
          
            var key = entityTypes.First(t => t.ClrType == typeof(T)).FindPrimaryKey().Properties.First().Name;
            return key;
        }
    }
}
