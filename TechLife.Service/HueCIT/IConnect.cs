using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Service.HueCIT
{
    internal interface IConnect
    {
        SqlConnection IConnectData();
    }
}
