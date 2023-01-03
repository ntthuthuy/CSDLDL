using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IVeDiTichDiaDiemRepository
    {
        Task<IEnumerable<VeDiTichDiaDiem>> Gets();
        Task<VeDiTichDiaDiem> Get(int id);

        Task<VeDiTichDiaDiem> Insert(VeDiTichDiaDiem data);
        Task<VeDiTichDiaDiem> Update(VeDiTichDiaDiem data);

        #region ĐỒNG BỘ
        Task GetDataDiaDiem();
        #endregion
    }
}
