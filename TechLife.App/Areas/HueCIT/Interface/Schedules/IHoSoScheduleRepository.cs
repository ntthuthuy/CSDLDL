using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Interface.Schedules
{
    public interface IHoSoScheduleRepository
    {
        Task GetDataVcgt();
        Task GetDataCongTyVanChuyen();
        Task GetDataCongTyLuHanh();
        Task GetDataDiemDuLich();
        Task GetDataDiaDiemAnUong();
        Task GetDataCoSoMuaSam();
        Task GetDataDiSanVanHoa();
    }
}
