using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDiaDiemAnUongRepository
    {
        Task<IEnumerable<DuLieuDuLichModel>> GetsDiaDiemAnUong();
    }
}
