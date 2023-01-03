using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepTrangThaiRepository
    {
        Task<IEnumerable<DoanhNghiepTrangThaiTrinhDien>> Gets();
        Task<DoanhNghiepTrangThaiTrinhDien> Get(int id);
        Task<DoanhNghiepTrangThaiTrinhDien> GetByDongBoID(int id);
        Task<DoanhNghiepTrangThai> Insert(DoanhNghiepTrangThai data);
        Task<DoanhNghiepTrangThai> Update(DoanhNghiepTrangThai data);
    }
}
