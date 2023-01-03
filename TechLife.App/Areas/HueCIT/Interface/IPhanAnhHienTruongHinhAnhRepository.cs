using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IPhanAnhHienTruongHinhAnhRepository
    {
        Task<IEnumerable<PhanAnhHienTruongHinhAnh>> GetsPhanAnhHienTruongHinhAnh();
        Task<IEnumerable<PhanAnhHienTruongHinhAnh>> GetsPhanAnhHienTruongHinhAnhByPhanAnhId(int PhanAnhId);
        Task<PhanAnhHienTruongHinhAnh> InsertPhanAnhHienTruongHinhAnh(PhanAnhHienTruongHinhAnh data);
        Task<PhanAnhHienTruongHinhAnh> UpdatePhanAnhHienTruongHinhAnh(PhanAnhHienTruongHinhAnh data);
        Task<int> Delete(int id);
    }
}
