using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TechLife.Data.Configurations;
using TechLife.Data.Entities;
using TechLife.Data.Extensions;
using TechLife.Model.DuLieuDuLich;

namespace TechLife.Data
{
    public class TLDbContext : IdentityDbContext<User, Role, Guid>
    {
        public TLDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            modelBuilder.ApplyConfiguration(new TrackingConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());

            modelBuilder.ApplyConfiguration(new GiayPhepConfigruation());
            modelBuilder.ApplyConfiguration(new BoPhanConfiguration());
            modelBuilder.ApplyConfiguration(new HoSoConfigruation());
            modelBuilder.ApplyConfiguration(new DichVuConfigruation());
            modelBuilder.ApplyConfiguration(new DichVuHoSoConfigruation());
            modelBuilder.ApplyConfiguration(new LoaiDichVuConfiguration());
            modelBuilder.ApplyConfiguration(new LoaiHinhConfiguration());
            modelBuilder.ApplyConfiguration(new LoaiHinhLaoDongConfiguration());
            modelBuilder.ApplyConfiguration(new LoaiPhongConfiguration());
            modelBuilder.ApplyConfiguration(new MucDoTTNNHoSoConfiguration());
            modelBuilder.ApplyConfiguration(new MucDoThongThaoNgoaiNguConfiguration());
            modelBuilder.ApplyConfiguration(new NganhDaoTaoConfigruation());
            modelBuilder.ApplyConfiguration(new HuongDanVienConfigruation());
            modelBuilder.ApplyConfiguration(new NgoaiNguConfigruation());
            modelBuilder.ApplyConfiguration(new PhongConfigruation());
            modelBuilder.ApplyConfiguration(new QuocTichConfigruation());
            modelBuilder.ApplyConfiguration(new TinhChatLaoDongConfiguartion());
            modelBuilder.ApplyConfiguration(new TrinhDoConfigruation());
            modelBuilder.ApplyConfiguration(new DiaPhuongConfigruation());
            modelBuilder.ApplyConfiguration(new DonViTinhConfigruation());
            modelBuilder.ApplyConfiguration(new LoaiGiuongConfigruation());
            modelBuilder.ApplyConfiguration(new TienNghiConfiguration());
            modelBuilder.ApplyConfiguration(new LoaiPhongHoSoConfiguration());
            modelBuilder.ApplyConfiguration(new NgoaiNguHoSoConfigruation());
            modelBuilder.ApplyConfiguration(new TrinhDoHoSoConfigruation());
            modelBuilder.ApplyConfiguration(new ThucDonHoSoConfiguration());
            modelBuilder.ApplyConfiguration(new VeDichVuHoSoConfiguration());
            modelBuilder.ApplyConfiguration(new QuaTrinhHoatDongConfigruation());
            modelBuilder.ApplyConfiguration(new DanhMucConfiguration());
            modelBuilder.ApplyConfiguration(new DanhGiaConfiguration());
            modelBuilder.ApplyConfiguration(new NhaCungCapConfiguration());
            modelBuilder.ApplyConfiguration(new HoSoVanBanConfigruation());
            modelBuilder.ApplyConfiguration(new HuongDanVienLoaiHinhConfigruation());
            modelBuilder.ApplyConfiguration(new HuongDanVienNgonNguConfigruation());
            modelBuilder.ApplyConfiguration(new PhongBanConfigruation());
            modelBuilder.ApplyConfiguration(new QuyMoNhaHangLuuTruConfigruation());

            modelBuilder.ApplyConfiguration(new TourConfigruation());
            modelBuilder.ApplyConfiguration(new HanhTrinhConfigruation());
            modelBuilder.ApplyConfiguration(new FileUploadConfigruation());

            modelBuilder.ApplyConfiguration(new HoSoThanhTraConfigruation());
            modelBuilder.ApplyConfiguration(new VanBanHoSoThanhTraConfigruation());
            modelBuilder.ApplyConfiguration(new BinhLuanConfiguration());



            modelBuilder.ApplyConfiguration(new ChuyenMucConfigruation());
            modelBuilder.ApplyConfiguration(new TinTucConfigruation());
            modelBuilder.ApplyConfiguration(new TinTucChuyenMucConfigruation());

            modelBuilder.ApplyConfiguration(new ChuyenDiConfigruation());
            modelBuilder.ApplyConfiguration(new HanhTrinhChuyenDiConfigruation());

            modelBuilder.ApplyConfiguration(new ThietBiConfigruation());
            modelBuilder.ApplyConfiguration(new OrderConfigruation());
            modelBuilder.ApplyConfiguration(new OrderDetailConfigruation());

            //HeuCIT
            modelBuilder.ApplyConfiguration(new LoaiDiaDiemAnUongConfigruation());
            modelBuilder.ApplyConfiguration(new LoaiAmThucDiaDiemAnUongConfigruation());

            //Data seeding
            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Tracking> Trackings { get; set; }

        public DbSet<GiayPhep> GiayPhep { get; set; }
        public DbSet<PhongBan> PhongBan { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<DiemVeSinh> DiemVeSinh { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<TienNghi> TienNghi { get; set; }
        public DbSet<TienNghiHoSo> TienNghiHoSo { get; set; }
        public DbSet<BoPhan> BoPhan { get; set; }
        public DbSet<BoPhanHoSo> BoPhanHoSo { get; set; }
        public DbSet<LoaiGiuong> LoaiGiuong { get; set; }
        public DbSet<HoSo> HoSo { get; set; }
        public DbSet<QuyMoNhaHangLuuTru> QuyMoNhaHangLuuTru { get; set; }
        public DbSet<HoSoVanBan> HoSoVanBan { get; set; }
        public DbSet<DichVu> DichVu { get; set; }
        public DbSet<DichVuHoSo> DichVuHoSo { get; set; }
        public DbSet<LoaiDichVu> LoaiDichVu { get; set; }
        public DbSet<LoaiHinh> LoaiHinh { get; set; }
        public DbSet<LoaiHinhLaoDong> LoaiHinhLaoDong { get; set; }
        public DbSet<LoaiPhong> LoaiPhong { get; set; }
        public DbSet<LoaiPhongHoSo> LoaiPhongHoSo { get; set; }
        public DbSet<MucDoThongThaoNgoaiNgu> MucDoThongThaoNgoaiNgu { get; set; }
        public DbSet<MucDoTTNNHoSo> MucDoTTNNHoSo { get; set; }
        public DbSet<NganhDaoTao> NganhDaoTao { get; set; }
        public DbSet<NgoaiNgu> NgoaiNgu { get; set; }
        public DbSet<NgoaiNguHoSo> NgoaiNguHoSo { get; set; }
        public DbSet<Phong> Phong { get; set; }
        public DbSet<QuocTich> QuocTich { get; set; }
        public DbSet<TinhChatLaoDong> TinhChatLaoDong { get; set; }
        public DbSet<TrinhDo> TrinhDo { get; set; }
        public DbSet<TrinhDoHoSo> TrinhDoHoSo { get; set; }
        public DbSet<DiaPhuong> DiaPhuong { get; set; }
        public DbSet<DonViTinh> DonViTinh { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<HanhTrinh> HanhTrinh { get; set; }
        public DbSet<HuongDanVien> HuongDanVien { get; set; }
        public DbSet<HuongDanVienLoaiHinh> HuongDanVienLoaiHinh { get; set; }
        public DbSet<HuongDanVienNgonNgu> HuongDanVienNgonNgu { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<ThucDonHoSo> ThucDonHoSo { get; set; }
        public DbSet<VeDichVuHoSo> VeDichVuHoSo { get; set; }
        public DbSet<QuaTrinhHoatDong> QuaTrinhHoatDong { get; set; }

        public DbSet<HoSoThanhTra> HoSoThanhTra { get; set; }
        public DbSet<VanBanHoSoThanhTra> VanBanHoSoThanhTra { get; set; }

        public DbSet<NgonNgu> NgonNgu { get; set; }
        public DbSet<ChuyenMuc> ChuyenMuc { get; set; }
        public DbSet<TinTuc> TinTuc { get; set; }
        public DbSet<TinTucChuyenMuc> TinTucChuyenMuc { get; set; }
        public DbSet<BinhLuan> BinhLuan { get; set; }
        public DbSet<ChuyenDi> ChuyenDi { get; set; }
        public DbSet<HanhTrinhChuyenDi> HanhTrinhChuyenDi { get; set; }
        public DbSet<ThietBi> ThietBi { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //TechLifeUpdate ngày 28
        public DbSet<LoaiGiuongPhong> LoaiGiuongPhongs { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        //HueCIT
        public DbSet<LoaiDiaDiemAnUong> LoaiDiaDiemAnUong { get; set; }
        public DbSet<LoaiAmThucDiaDiemAnUong> LoaiAmThucDiaDiemAnUong { get; set; }

    }
}