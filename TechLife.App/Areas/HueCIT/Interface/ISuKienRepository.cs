using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Common;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface ISuKienRepository
    {
        Task<IEnumerable<SuKienTrinhDien>> Gets(SuKienRequest data);
        Task<SuKienTrinhDien> Get(int id);
        Task<SuKien> Add(SuKien data);
        Task<SuKien> Edit(SuKien data);
        Task<int> Delete(int id);
    }
}
