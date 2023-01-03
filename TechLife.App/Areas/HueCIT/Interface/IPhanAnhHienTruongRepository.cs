using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IPhanAnhHienTruongRepository
    {
        Task<IEnumerable<PhanAnhHienTruong>> GetsPhanAnhHienTruongByMaDinhDanh(string madinhdanh);
        Task<IEnumerable<PhanAnhHienTruong>> GetsPhanAnhHienTruongByLoaiXuLy(int loaixuly);
        Task<PhanAnhHienTruong> GetByDongBoID(int id);
        Task<PhanAnhHienTruong> InsertPhanAnhHienTruong(PhanAnhHienTruong data);
        Task<PhanAnhHienTruong> UpdatePhanAnhHienTruong(PhanAnhHienTruong data);
        Task<IEnumerable<PhanAnhHienTruongTrinhDien>> GetsByLinhVuc(int linhvucId);
        Task<IEnumerable<PhanAnhHienTruongTrinhDien>> GetsPhanAnhHienTruongTrinhDien(PhanAnhHienTruongTrinhDienRequest request);
        Task<PhanAnhHienTruongTrinhDien> GetPhanAnhHienTruongTrinhDien(int id);
        Task<PhanAnhHienTruong> Edit(PhanAnhHienTruongEditRequest data);
        Task<int> Delete(int id);
    }
}
