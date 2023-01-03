using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepRepository
    {
        Task<IEnumerable<DoanhNghiepTrinhDien>> Gets(DoanhNghiepRequest req);
        Task<DoanhNghiepTrinhDien> Get(int id);
        Task<DoanhNghiepTrinhDien> GetByDongBoID(string id);
        Task<DoanhNghiep> Insert(DoanhNghiep data);
        Task<DoanhNghiep> Update(DoanhNghiep data);
    }
}
