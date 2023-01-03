using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IAmThucRepository
    {
        Task<IEnumerable<AmThucTrinhDien>> Gets(AmThucRequest data);
        Task<AmThucTrinhDien> Get(int id);
        Task<AmThucTrinhDien> GetByAmThucID(int id, int nguondongbo);
        Task<AmThuc> Add(AmThuc data);
        Task<AmThuc> Edit(AmThuc data);
        Task<int> Delete(int id);
        Task<IEnumerable<AmThucSearch>> GetsSearch();

        #region ĐỒNG BỘ
        Task GetData();
        #endregion

    }
}
