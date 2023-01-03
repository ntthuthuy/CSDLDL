using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IThoiTietSymbolRepository
    {
        Task<IEnumerable<ThoiTietSymbol>> GetsThoiTietSymbol();
        Task<ThoiTietSymbol> GetThoiTietSymbol(string id);
        Task<ThoiTietSymbol> InsertThoiTietSymbol(ThoiTietSymbol data);
        Task<ThoiTietSymbol> UpdateThoiTietSymbol(ThoiTietSymbol data);

        #region ĐỒNG BỘ
        Task GetDataSymbol();
        #endregion
    }
}
