using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;
using TechLife.Data.Entities;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IVeDiTichRepository
    {
        Task<IEnumerable<VeDiTichTrinhDien>> Gets(VeDiTichRequest request);
        Task<IEnumerable<VeDiTichTrinhDien>> GetsByNgayBan(DateTime ngayban);
        Task<VeDiTich> Insert(VeDiTich data);
        Task<int> Delete(int id);
        #region ĐỒNG BỘ
        Task GetDataVeDiTich();
        #endregion
    }
}
