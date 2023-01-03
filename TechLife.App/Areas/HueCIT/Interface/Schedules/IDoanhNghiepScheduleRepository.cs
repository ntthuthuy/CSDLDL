using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Interface.Schedules
{
    public interface IDoanhNghiepScheduleRepository
    {
        Task GetDataTrangThai();
        Task GetDataLoaiHinh();
        Task GetDataNganhNghe();
        Task GetDataLoaiVanBan();
        Task GetDataDoanhNghiep();
        Task GetDataVanBan();
    }
}
