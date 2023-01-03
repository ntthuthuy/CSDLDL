using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IQuanTracMoiTruongRepository
    {
        Task<IEnumerable<QuanTracMoiTruongTrinhDien>> Gets();
        Task<QuanTracMoiTruongTrinhDien> Get(QuanTracMoiTruongFilter filter);
        Task<QuanTracMoiTruong> Insert(QuanTracMoiTruong data);
        Task<QuanTracMoiTruong> Update(QuanTracMoiTruong data);
        Task<List<DanhSachQuanTracMoiTruong>> DanhSachTheoTenThongSo(QuanTracMoiTruongRequest request);

        #region ĐỒNG BỘ
        Task GetData();
        #endregion
    }
}
