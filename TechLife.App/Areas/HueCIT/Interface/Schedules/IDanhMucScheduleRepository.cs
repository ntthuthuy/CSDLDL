using System.Threading.Tasks;

namespace TechLife.App.Areas.HueCIT.Interface.Schedules
{
    public interface IDanhMucScheduleRepository
    {
        // Hồ sơ
        Task GetDataDanhMucDiemDuLich();
        Task GetDataDanhMucCoSoMuaSam();
        Task GetDataDanhMucLuHanh();
        Task GetDataDanhMucVanChuyen();
        Task GetDataDanhMucKhuVuiChoi();
        Task GetDataDanhMucTheThao();
        Task GetDataDanhCSSK();

        // Loại ẩm thực
        Task GetDataDanhMucAmThuc();

        // Loại địa điểm ăn uống
        Task GetDataDanhMucDiaDiemAnUong();

        // Loại lễ hội
        Task GetDataDanhMucLeHoi();

        // Loại điểm giao dịch
        Task GetDataDanhMucDiemGiaoDich();

    }
}
