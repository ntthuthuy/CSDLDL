using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechLife.Data
{
    public class TLDbContextFactory : IDesignTimeDbContextFactory<TLDbContext>
    {
        public TLDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var connectionString = configuration.GetConnectionString("SolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<TLDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TLDbContext(optionsBuilder.Options);
        }
    }
}
