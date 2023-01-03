using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using TechLife.App.Areas.HueCIT.Interface;
using TechLife.App.Areas.HueCIT.Interface.Schedules;
using TechLife.App.Areas.HueCIT.Jobs;
using TechLife.App.Areas.HueCIT.Repository;
using TechLife.App.Areas.HueCIT.Repository.Schedules;
using TechLife.App.Areas.HueCIT.Schedules;
using TechLife.Service.HueCIT;

namespace TechLife.App.Areas.HueCIT
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDanhMucRepository, DanhMucRepository>();
            services.AddTransient<ILeHoiRepository, LeHoiRepository>();
            services.AddTransient<IAmThucRepository, AmThucRepository>();
            services.AddTransient<IDiemGiaoDichRepository, DiemGiaoDichRepository>();
            services.AddTransient<IDuongDayNongRepository, DuongDayNongRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IHoSoService, HoSoService>();
            services.AddTransient<ISuKienRepository, SuKienRepository>();
            services.AddTransient<ITyGiaRepository, TyGiaRepository>();
            services.AddTransient<IPhanAnhHienTruongLinhVucRepository, PhanAnhHienTruongLinhVucRepository>();
            services.AddTransient<IPhanAnhHienTruongRepository, PhanAnhHienTruongRepository>();
            services.AddTransient<IPhanAnhHienTruongHinhAnhRepository, PhanAnhHienTruongHinhAnhRepository>();
            services.AddTransient<IPhanAnhHienTruongCoQuanRepository, PhanAnhHienTruongCoQuanRepository>();
            services.AddTransient<IThoiTietRepository, ThoiTietRepository>();
            services.AddTransient<IThoiTietSymbolRepository, ThoiTietSymbolRepository>();
            services.AddTransient<IVeDiTichRepository, VeDiTichRepository>();
            services.AddTransient<IVeDiTichLoaiRepository, VeDiTichLoaiRepository>();
            services.AddTransient<IVeDiTichDiaDiemRepository, VeDiTichDiaDiemRepository>();
            services.AddTransient<IDoanhNghiepTrangThaiRepository, DoanhNghiepTrangThaiRepository>();
            services.AddTransient<IDoanhNghiepNganhNgheRepository, DoanhNghiepNganhNgheRepository>();
            services.AddTransient<IDoanhNghiepLoaiHinhRepository, DoanhNghiepLoaiHinhRepository>();
            services.AddTransient<IDoanhNghiepLoaiVanBanRepository, DoanhNghiepLoaiVanBanRepository>();
            services.AddTransient<IDoanhNghiepVanBanRepository, DoanhNghiepVanBanRepository>();
            services.AddTransient<IDoanhNghiepRepository, DoanhNghiepRepository>();
            services.AddTransient<IQuanTracMoiTruongRepository, QuanTracMoiTruongRepository>();
            services.AddTransient<IFileDongBoRepository, FileDongBoRepository>();
            services.AddTransient<IQuanHuyenRepository, QuanHuyenRepository>();
            services.AddTransient<IDanhMucDongBoRepository, DanhMucDongBoRepository>();
            services.AddTransient<IDichVuDiTichRepository, DichVuDiTichRepository>();
            services.AddTransient<IDichVuDiTichHinhAnhRepository, DichVuDiTichHinhAnhRepository>();

            services.AddTransient<IDoanhNghiepScheduleRepository, DoanhNghiepScheduleRepository>();
            services.AddTransient<IPhanAnhHienTruongScheduleRepository, PhanAnhHienTruongScheduleRepository>();
            services.AddTransient<ILeHoiScheduleRepositoty, LeHoiScheduleRepositoty>();
            services.AddTransient<IDiemGiaoDichScheduleRepository, DiemGiaoDichScheduleRepository>();
            services.AddTransient<IDiemVeSinhScheduleRepository, DiemVeSinhScheduleRepository>();
            services.AddTransient<IHoSoScheduleRepository, HoSoScheduleRepository>();
            services.AddTransient<IDanhMucScheduleRepository, DanhMucScheduleRepository>();

            #region Lập lịch đồng bộ dữ liệu
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Job
            services.AddSingleton<PhanAnhHienTruongJob>();
            services.AddSingleton<HienTruongTheoLinhVucJob>();
            services.AddSingleton<DoanhNghiepJob>();
            services.AddSingleton<VeDiTichJob>();
            services.AddSingleton<ThoiTietJob>();
            services.AddSingleton<QuanTracMoiTruongJob>();


            services.AddSingleton<LeHoiJob>();
            services.AddSingleton<DiemGiaoDichJob>();
            services.AddSingleton<DiemVeSinhJob>();

            services.AddSingleton<AmThucJob>();
            services.AddSingleton<DiaDiemAnUongJob>();
            services.AddSingleton<VcgtJob>();
            services.AddSingleton<CongTyVanChuyenJob>();
            services.AddSingleton<CongTyLuHanhJob>();
            services.AddSingleton<DiemDuLichJob>();
            services.AddSingleton<CoSoMuaSamJob>();
            services.AddSingleton<DiSanVanHoaJob>();

            #region Giờ
            // Mỗi 1 tiếng chạy lúc phút thứ 2, 58, 28, 32
            services.AddSingleton(new JobSchedule("Quan_Trac_Moi_Truong", typeof(QuanTracMoiTruongJob), "Quan trắc môi trường", "0 2,28,32,58 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23 1/1 * ? *"));
            // Mỗi 2 tiếng
            services.AddSingleton(new JobSchedule("Thoi_Tiet", typeof(ThoiTietJob), "Thời tiết", "0 0/30 * 1/1 * ? *"));
            #endregion

            #region Ngày

            // 7:pm
            services.AddSingleton(new JobSchedule("Phan_Anh_Hien_Truong", typeof(PhanAnhHienTruongJob), "Phản ánh hiện trường", "0 0 19 * * ?"));
            // 9:pm
            services.AddSingleton(new JobSchedule("Ve_Di_Tich", typeof(VeDiTichJob), "Vé di tích", "0 0 23 * * ?"));

            #endregion

            #region Tuần

            // Thứ Hai
            // 2:am 
            services.AddSingleton(new JobSchedule("Am_Thuc", typeof(AmThucJob), "Ẩm thực", "0 0 2 ? * MON *"));
            // 2:pm 
            services.AddSingleton(new JobSchedule("Di_San_Van_Hoa", typeof(DiSanVanHoaJob), "Di sản văn hóa", "0 0 14 ? * MON *"));

            // Thứ Ba
            // 2:am 
            services.AddSingleton(new JobSchedule("Dia_Diem_An_Uong", typeof(DiaDiemAnUongJob), "Địa điểm ăn uống", "0 0 2 ? * TUE *"));
            // 2:pm 
            services.AddSingleton(new JobSchedule("Co_So_Mua_Sam", typeof(CoSoMuaSamJob), "Cơ sở mua sắm", "0 0 14 ? * TUE *"));

            // Thứ Tư
            // 2:am 
            services.AddSingleton(new JobSchedule("Vcgt", typeof(VcgtJob), "Vui chơi giải trí", "0 0 2 ? * WED *"));
            // 2:pm 
            services.AddSingleton(new JobSchedule("Cong_Ty_Van_Chuyen", typeof(CongTyVanChuyenJob), "Công ty vận chuyển", "0 0 14 ? * WED *"));

            // Thứ Năm
            // 2:am 
            services.AddSingleton(new JobSchedule("Cong_Ty_Lu_Hanh", typeof(CongTyLuHanhJob), "Công ty lữ hành", "0 0 2 ? * THU *"));
            // 2:pm
            services.AddSingleton(new JobSchedule("Diem_Du_Lich", typeof(DiemDuLichJob), "Điểm du lịch", "0 0 14 ? * THU *"));

            #endregion

            #region Tháng
            // Ngày 15
            services.AddSingleton(new JobSchedule("Doanh_Nghiep", typeof(DoanhNghiepJob), "Doanh nghiệp", "0 0 10 15 1/1 ? *"));

            // Ngày 29
            services.AddSingleton(new JobSchedule("Le_Hoi", typeof(LeHoiJob),"Lễ hội" , "0 0 10 29 1/1 ? *"));
            services.AddSingleton(new JobSchedule("Diem_Ve_Sinh", typeof(DiemVeSinhJob), "Điểm vệ sinh", "0 0 10 29 1/1 ? *"));

            #endregion

            // Cron 1 minute : 0 0/1 * 1/1 * ? * 

            services.AddHostedService<QuartzHostedService>();
            #endregion

            return services;
        }
    }
}
