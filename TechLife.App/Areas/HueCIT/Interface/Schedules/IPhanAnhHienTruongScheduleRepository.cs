using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Interface.Schedules
{
    public interface IPhanAnhHienTruongScheduleRepository
    {
        Task GetLinhVuc();
        Task GetCoQuan();
        Task GetData();
        Task GetDataWait();
        Task GetDataLinhVuc(int linhvucId, bool isEnable);
        Task GetDataWaitLinhVuc(int linhvucId, bool isEnable);
    }
}
