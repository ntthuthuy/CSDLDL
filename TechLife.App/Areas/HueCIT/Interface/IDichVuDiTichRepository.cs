using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDichVuDiTichRepository
    {
        #region CRUD
        Task<IEnumerable<DichVuDiTichTrinhDien>> Gets();
        Task<DichVuDiTichTrinhDien> GetByDongBo(string dongBoId);
        Task<DichVuDiTich> Add(DichVuDiTich data);
        Task<DichVuDiTich> Edit(DichVuDiTich data);
        #endregion

        #region ĐỒNG BỘ
        Task GetData();
        #endregion
    }
}
