using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IThoiTietRepository
    {
        Task<IEnumerable<ThoiTiet>> GetsThoiTiet();
        Task<IEnumerable<ThoiTietTrinhDien>> GetsThoiTietTrinhDien(ThoiTietRequest request);
        Task<ThoiTiet> GetThoiTiet(string id);
        Task<ThoiTiet> InsertThoiTiet(ThoiTiet data);
        Task<ThoiTiet> UpdateThoiTiet(ThoiTiet data);

        #region ĐỒNG BỘ
        Task GetData();
        #endregion

    }
}
