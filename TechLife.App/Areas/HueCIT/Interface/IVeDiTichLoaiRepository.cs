using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IVeDiTichLoaiRepository
    {
        #region CRUD
        Task<IEnumerable<VeDiTichLoai>> Gets();
        Task<IEnumerable<VeDiTichLoaiTrinhDien>> GetsTrinhDien(VeDiTichLoaiRequest req);
        Task<VeDiTichLoai> Get(int VeId, int LoaiDoiTuong);
        Task<VeDiTichLoai> Insert(VeDiTichLoai data);
        Task<VeDiTichLoai> Update(VeDiTichLoai data);

        #endregion

        #region ĐỒNG BỘ
        Task GetDataLoaiVe();
        #endregion

    }
}
