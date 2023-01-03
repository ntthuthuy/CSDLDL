using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDichVuDiTichHinhAnhRepository
    {
        Task<IEnumerable<DichVuDiTichHinhAnhTrinhDien>> Gets();
        Task<DichVuDiTichHinhAnhTrinhDien> GetByMaDVDT(int MaDVDT);
        Task<DichVuDiTichHinhAnh> Add(DichVuDiTichHinhAnh data);
        Task<DichVuDiTichHinhAnh> Edit(DichVuDiTichHinhAnh data);
    }
}
