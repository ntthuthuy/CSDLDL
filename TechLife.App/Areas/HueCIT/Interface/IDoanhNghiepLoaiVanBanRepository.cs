using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepLoaiVanBanRepository
    {
        Task<IEnumerable<DoanhNghiepLoaiVanBanTrinhDien>> Gets();
        Task<DoanhNghiepLoaiVanBanTrinhDien> Get(int id);
        Task<DoanhNghiepLoaiVanBanTrinhDien> GetByDongBoID(int id);
        Task<DoanhNghiepLoaiVanBan> Insert(DoanhNghiepLoaiVanBan data);
        Task<DoanhNghiepLoaiVanBan> Update(DoanhNghiepLoaiVanBan data);
    }
}
