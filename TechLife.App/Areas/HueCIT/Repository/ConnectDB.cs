using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using TechLife.App.Areas.HueCIT.Interface;

namespace TechLife.App.Areas.HueCIT.Repository
{
    public class ConnectDB : IConnectDB
    {
        private readonly IConfiguration _configuration;
        public ConnectDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection IConnectData()
        {
            try
            {
                var conn = new SqlConnection
                {
                    ConnectionString = _configuration.GetConnectionString("SolutionDb2")
                };

                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlConnection IConnectDataMain()
        {
            try
            {
                var conn = new SqlConnection
                {
                    ConnectionString = _configuration.GetConnectionString("SolutionDb")
                };

                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
