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
    public interface IDuongDayNongRepository
    {
        Task<IEnumerable<DuongDayNongTrinhDien>> Gets(DuongDayNongRequest data);
        Task<DuongDayNongTrinhDien> Get(int id);
        Task<DuongDayNong> Add(DuongDayNong data);
        Task<DuongDayNong> Edit(DuongDayNong data);
        Task<int> Delete(int id);
        Task<IEnumerable<DuongDayNongSearch>> GetsSearch();
    }
}
