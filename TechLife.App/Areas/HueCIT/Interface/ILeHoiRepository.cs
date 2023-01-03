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
    public interface ILeHoiRepository
    {
        Task<IEnumerable<LeHoiTrinhDien>> Gets(LeHoiRequest data);
        Task<LeHoiTrinhDien> Get(string id);
        Task<LeHoiTrinhDien> GetByLeHoiID(int? id);
        Task<LeHoi> Add(LeHoi data);
        Task<LeHoi> Edit(LeHoi data);
        Task<int> Delete(string id);
    }
}
