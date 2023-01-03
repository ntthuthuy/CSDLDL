using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IPhanAnhHienTruongCoQuanRepository
    {
        Task<IEnumerable<PhanAnhHienTruongCoQuan>> GetsPhanAnhHienTruongCoQuan();
        Task<PhanAnhHienTruongCoQuan> GetPhanAnhHienTruongCoQuan(string id);
        Task<PhanAnhHienTruongCoQuan> InsertPhanAnhHienTruongCoQuan(PhanAnhHienTruongCoQuan data);
        Task<PhanAnhHienTruongCoQuan> UpdatePhanAnhHienTruongCoQuan(PhanAnhHienTruongCoQuan data);
    }
}
