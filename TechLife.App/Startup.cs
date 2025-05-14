using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.IO;
using TechLife.App.ApiClients;
using TechLife.App.ApiClients.HueCIT;
using TechLife.App.Areas.HueCIT;
using TechLife.App.Extensions.Authorizations;
using TechLife.App.Extensions.FileLogger;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Model;
using TechLife.Service;
using TechLife.Service.Common;
using TechLife.Service.HueCIT;

namespace TechLife.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddControllersWithViews()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            //HueCIT
            services.AddDbContext<TLDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)), ServiceLifetime.Transient, ServiceLifetime.Singleton);
            services.AddInfrastructure();

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<TLDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = new PathString("/Login");
                options.AccessDeniedPath = new PathString("/Logout");
                options.AccessDeniedPath = new PathString("/AccessDenied");

                options.SlidingExpiration = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
             .AddCookie(options =>
             {
                 options.LoginPath = new PathString($"/Login");
                 options.AccessDeniedPath = new PathString("/AccessDenied");
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                 options.SlidingExpiration = true;
             });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<IGroupApiClient, GroupApiClient>();
            services.AddTransient<IRoleGroupApiClient, RoleGroupApiClient>();


            services.AddTransient<IHscvApiClient, HscvApiClient>();

            services.AddTransient<IDuLieuDuLichApiClient, DuLieuDuLichApiClient>();
            services.AddTransient<IBoPhanApiClient, BoPhanApiClient>();
            services.AddTransient<IDiaPhuongApiClient, DiaPhuongApiClient>();
            services.AddTransient<IDichVuApiClient, DichVuApiClient>();
            services.AddTransient<ILoaiDichVuApiClient, LoaiDichVuApiClient>();
            services.AddTransient<ILoaiHinhApiClient, LoaiHinhApiClient>();
            services.AddTransient<ILoaiHinhLaoDongApiClient, LoaiHinhLaoDongApiClient>();
            services.AddTransient<ILoaiPhongApiClient, LoaiPhongApiClient>();
            services.AddTransient<ILoaiPhongApiClient, LoaiPhongApiClient>();
            services.AddTransient<IMucDoThongThaoNgoaiNguApiClient, MucDoThongThaoNgoaiNguApiClient>();
            services.AddTransient<INganhDaoTaoApiClient, NganhDaoTaoApiClient>();
            services.AddTransient<INgoaiNguApiClient, NgoaiNguApiClient>();
            services.AddTransient<IPhongApiClient, PhongApiClient>();
            services.AddTransient<IQuocTichApiClient, QuocTichApiClient>();
            services.AddTransient<IQuocTichApiClient, QuocTichApiClient>();
            services.AddTransient<ITinhChatLaoDongApiClient, TinhChatLaoDongApiClient>();
            services.AddTransient<ITrinhDoApiClient, TrinhDoApiClient>();
            services.AddTransient<IDonViTinhApiClient, DonViTinhApiClient>();
            services.AddTransient<ILoaiGiuongApiClient, LoaiGiuongApiClient>();
            services.AddTransient<ITienNghiApiClient, TienNghiApiClient>();
            services.AddTransient<IHuongDanVienApiClient, HuongDanVienApiClient>();
            services.AddTransient<ITourApiClient, TourApiClient>();
            services.AddTransient<IDanhMucApiClient, DanhMucApiClient>();
            services.AddTransient<IDiemVeSinhApiClient, DiemVeSinhApiClient>();
            services.AddTransient<IFileApiClient, FileApiClient>();
            //HueCIT
            services.AddTransient<IFileUploaderApiClient, FileUploaderApiClient>();


            services.AddTransient<IHoSoThanhTraService, HoSoThanhTraService>();
            services.AddTransient<IDuLieuDuLichService, DuLieuDuLichService>();
            services.AddTransient<ILoaiHinhService, LoaiHinhService>();
            services.AddTransient<IBoPhanService, BoPhanService>();
            services.AddTransient<INgoaiNguService, NgoaiNguService>();
            services.AddTransient<INgoaiNguService, NgoaiNguService>();
            services.AddTransient<ILoaiPhongService, LoaiPhongService>();
            services.AddTransient<ILoaiGiuongService, LoaiGiuongService>();
            services.AddTransient<IDichVuService, DichVuService>();
            services.AddTransient<IMucDoThongThaoNgoaiNguService, MucDoThongThaoNgoaiNguService>();
            services.AddTransient<ITienNghiService, TienNghiService>();
            services.AddTransient<ITrinhDoService, TrinhDoService>();
            services.AddTransient<ITrinhDoService, TrinhDoService>();
            services.AddTransient<IThucDonService, ThucDonService>();
            services.AddTransient<IDanhGiaService, DanhGiaService>();
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<IDiaPhuongService, DiaPhuongService>();
            services.AddTransient<INhaCungCapService, NhaCungCapService>();
            services.AddTransient<IPhongBanService, PhongBanService>();
            services.AddTransient<IGiayPhepService, GiayPhepService>();
            services.AddTransient<IHuongDanVienService, HuongDanVienService>();
            services.AddTransient<IQuaTrinhHoatDongService, QuaTrinhHoatDongService>();
            services.AddTransient<INgonNguService, NgonNguService>();
            services.AddTransient<ILoaiPhongService, LoaiPhongService>();
            services.AddTransient<ILoaiDichVuService, LoaiDichVuService>();
            services.AddTransient<IDonViTinhService, DonViTinhService>();
            services.AddTransient<IDanhMucService, DanhMucService>();

            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IFileUploaderDongBoService, FileUploaderDongBoService>();
            services.AddTransient<ITrackingService, TrackingService>();
            services.AddTransient<IStorageService, FileStorageService>();

            //services.AddIdentity<User, Role>().AddEntityFrameworkStores<TLDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleGroupService, RoleGroupService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient(typeof(ILogger<>), (typeof(Logger<>)));
            //HueCIT
            services.AddTransient<IFileUploaderService, FileUploaderService>();
            services.AddTransient<IDiemVeSinhDongBoService, DiemVeSinhDongBoService>();
            services.AddTransient<IHoSoDongBoService, HoSoDongBoService>();
            services.AddTransient<IDanhMucDongBoService, DanhMucDongBoService>();
            services.AddTransient<IDiaPhuongDongBoService, DiaPhuongDongBoService>();
            services.AddTransient<IThongKeService, ThongKeService>();
            services.AddTransient<ILoaiDichVuDongBoService, LoaiDichVuDongBoService>();

            services.AddTransient<IAuthorizationHandler, AuthorizationHandler>();
            services.AddControllers().AddNewtonsoftJson();

            IMvcBuilder builder = services.AddRazorPages();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                loggerFactory.AddFile(Directory.GetCurrentDirectory());
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            //HueCIT
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    "HueCIT",
                    "HueCIT",
                    "HueCIT/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}