using System.Threading.Tasks;
using TechLife.App.Areas.HueCIT.Models;

namespace TechLife.App.Areas.HueCIT.Interface
{
    public interface IDanhMucDongBoRepository
    {
        Task<int> AddOrEditDiemDuLich(DanhMucDiemDuLichFormData param);
        Task<int> AddOrEditCoSoMuaSam(DanhMucCoSoMuaSamFormData param);
        Task<int> AddOrEditLuHanh(DanhMucLuHanhFormData param);
        Task<int> AddOrEditEformid(DanhMucEformidFormData param);
    }
}
