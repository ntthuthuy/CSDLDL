using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Service.HueCIT
{
    public class Connect : IConnect
    {
        private readonly IConfiguration _configuration;
        public Connect(IConfiguration configuration)
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
