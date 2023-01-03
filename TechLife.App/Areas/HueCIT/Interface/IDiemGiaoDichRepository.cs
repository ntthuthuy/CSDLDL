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
    public interface IDiemGiaoDichRepository
    {
        Task<IEnumerable<DiemGiaoDichTrinhDien>> Gets(DiemGiaoDichRequest data);
        Task<DiemGiaoDichTrinhDien> Get(int id);
        Task<DiemGiaoDich> Add(DiemGiaoDich data);
        Task<DiemGiaoDich> Edit(DiemGiaoDich data);
        Task<int> Delete(int id);
        Task<IEnumerable<DiemGiaoDichSearch>> GetsSearch();
        Task<DiemGiaoDichTrinhDien> GetByDiemGiaoDichID(int id);
    }
}
