using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepVanBanRepository
    {
        Task<IEnumerable<DoanhNghiepVanBanTrinhDien>> Gets();
        Task<IEnumerable<DoanhNghiepVanBanTrinhDien>> GetsByMaDoanhNghiep(string madoanhnghiep);
        Task<DoanhNghiepVanBanTrinhDien> Get(int id);
        Task<DoanhNghiepVanBan> Insert(DoanhNghiepVanBan data);
        Task Delete(int id);
        Task<DoanhNghiepVanBanTrinhDien> GetBySoKyHieu(string soKyHieu);
    }
}
