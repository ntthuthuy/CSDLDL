using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IPhanAnhHienTruongLinhVucRepository
    {
        Task<IEnumerable<PhanAnhHienTruongLinhVuc>> GetsLinhVucPhanAnhHienTruong();
        Task<IEnumerable<PhanAnhHienTruongLinhVuc>> GetsIsEnableLinhVucPhanAnhHienTruong(bool isEnable);
        Task<PhanAnhHienTruongLinhVuc> InsertLinhVucPhanAnhHienTruong(PhanAnhHienTruongLinhVuc data);
        Task<PhanAnhHienTruongLinhVuc> UpdateLinhVucPhanAnhHienTruong(PhanAnhHienTruongLinhVuc data);
        Task<PhanAnhHienTruongLinhVuc> UpdateIsEnableLinhVucPhanAnhHienTruong(int id, bool isEnable);
        Task<PhanAnhHienTruongLinhVuc> GetLinhVucPhanAnhHienTruong(int id);
    }
}
