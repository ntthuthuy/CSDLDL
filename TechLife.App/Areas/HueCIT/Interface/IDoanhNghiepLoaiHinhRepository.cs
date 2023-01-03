using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDoanhNghiepLoaiHinhRepository
    {
        Task<IEnumerable<DoanhNghiepLoaiHinhTrinhDien>> Gets();
        Task<DoanhNghiepLoaiHinhTrinhDien> Get(int id);
        Task<DoanhNghiepLoaiHinhTrinhDien> GetByDongBoID(int? id);
        Task<DoanhNghiepLoaiHinh> Insert(DoanhNghiepLoaiHinh data);
        Task<DoanhNghiepLoaiHinh> Update(DoanhNghiepLoaiHinh data);
    }
}
