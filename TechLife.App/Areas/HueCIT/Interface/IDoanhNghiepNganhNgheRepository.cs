using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepNganhNgheRepository
    {
        Task<IEnumerable<DoanhNghiepNganhNgheTrinhDien>> Gets();
        Task<DoanhNghiepNganhNgheTrinhDien> Get(int id);
        Task<DoanhNghiepNganhNgheTrinhDien> GetByDongBoID(string id);
        Task<DoanhNghiepNganhNghe> Insert(DoanhNghiepNganhNghe data);
        Task<DoanhNghiepNganhNghe> Update(DoanhNghiepNganhNghe data);
    }
}
