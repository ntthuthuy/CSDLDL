using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IConnectDB
    {
        SqlConnection IConnectData();
        SqlConnection IConnectDataMain();
    }
}
