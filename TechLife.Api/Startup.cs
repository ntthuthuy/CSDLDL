using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechLife.Common;
using TechLife.Data;
using TechLife.Data.Entities;
using TechLife.Service;
using TechLife.Service.Common;
using TechLife.Service.HueCIT;

namespace TechLife.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<TLDbContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));


            services.AddDbContext<TLDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)),
             ServiceLifetime.Transient);

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<TLDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleGroupService, RoleGroupService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ILogService, LogService>();


            services.AddTransient<IBinhLuanService, BinhLuanService>();
            services.AddTransient<IDanhMucService, DanhMucService>();
            services.AddTransient<IBoPhanService, BoPhanService>();
            services.AddTransient<IThucDonService, ThucDonService>();
            services.AddTransient<IDuLieuDuLichService, DuLieuDuLichService>();
            services.AddTransient<IDiaPhuongService, DiaPhuongService>();
            services.AddTransient<IDichVuService, DichVuService>();
            services.AddTransient<ILoaiDichVuService, LoaiDichVuService>();
            services.AddTransient<ILoaiHinhService, LoaiHinhService>();
            services.AddTransient<ILoaiHinhLaoDongService, LoaiHinhLaoDongService>();
            services.AddTransient<ILoaiPhongService, LoaiPhongService>();
            services.AddTransient<IMucDoThongThaoNgoaiNguService, MucDoThongThaoNgoaiNguService>();
            services.AddTransient<INganhDaoTaoService, NganhDaoTaoService>();
            services.AddTransient<INgoaiNguService, NgoaiNguService>();
            services.AddTransient<IPhongService, PhongService>();
            services.AddTransient<IQuocTichService, QuocTichService>();
            services.AddTransient<ITinhChatLaoDongService, TinhChatLaoDongService>();
            services.AddTransient<ITrinhDoService, TrinhDoService>();
            services.AddTransient<IDonViTinhService, DonViTinhService>();
            services.AddTransient<ILoaiGiuongService, LoaiGiuongService>();
            services.AddTransient<ITienNghiService, TienNghiService>();
            services.AddTransient<IHuongDanVienService, HuongDanVienService>();
            services.AddTransient<IQuaTrinhHoatDongService, QuaTrinhHoatDongService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<IDanhGiaService, DanhGiaService>();
            services.AddTransient<IDiemVeSinhService, DiemVeSinhService>();
            services.AddTransient<IThietBiService, ThietBiService>();

            services.AddTransient<IChuyenMucService, ChuyenMucService>();
            services.AddTransient<ITinTucService, TinTucService>();
            services.AddTransient<IChuyenDiService, ChuyenDiService>();

            services.AddTransient<IOrderService, OrderService>();

            //HueCIT
            services.AddTransient<IFileUploaderService, FileUploaderService>();
            services.AddTransient<IHoSoService, HoSoService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Techlife API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes),
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:44311/",
                                                          "http://csdldulich.hue365.com/")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
                                  });
            });
            const int maxRequestLimit = 209715200;
            // If using IIS
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = maxRequestLimit;
            });
            // If using Kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = maxRequestLimit;
            });
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = maxRequestLimit;
                x.MultipartBodyLengthLimit = maxRequestLimit;
                x.MultipartHeadersLengthLimit = maxRequestLimit;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Techlife API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
