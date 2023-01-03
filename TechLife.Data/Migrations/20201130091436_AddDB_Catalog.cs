using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddDB_Catalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoPhan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBoPhan = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoPhan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoSoLuuTru",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCongTy = table.Column<string>(nullable: false),
                    Ten = table.Column<string>(nullable: false),
                    HangSao = table.Column<int>(nullable: false),
                    SoQuyetDinh = table.Column<string>(nullable: true),
                    NgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    LoaiHinhId = table.Column<int>(nullable: false),
                    TongVonDauTuBanDau = table.Column<decimal>(nullable: false),
                    TongVonDauTuBoSung = table.Column<decimal>(nullable: false),
                    DienTichMatBang = table.Column<decimal>(nullable: false),
                    DienTichMatBangXayDung = table.Column<decimal>(nullable: false),
                    DienTichXayDung = table.Column<decimal>(nullable: false),
                    SoTang = table.Column<int>(nullable: false),
                    SoGiayPhep = table.Column<string>(nullable: true),
                    ThoiDiemBatDauKinhDoan = table.Column<DateTime>(nullable: false),
                    SoLanChuyenChu = table.Column<int>(nullable: false),
                    SoNha = table.Column<string>(nullable: false),
                    DuongPho = table.Column<string>(nullable: false),
                    PhuongXaId = table.Column<int>(nullable: false),
                    QuanHuyenId = table.Column<int>(nullable: false),
                    TinhThanhId = table.Column<int>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    HoTenNguoiDaiDien = table.Column<string>(nullable: false),
                    ChucVuNguoiDaiDien = table.Column<string>(nullable: false),
                    GioiTinhNguoiDaiDien = table.Column<int>(nullable: false),
                    SoDienThoaiNguoiDaiDien = table.Column<string>(nullable: true),
                    SoLuongLaoDong = table.Column<int>(nullable: false),
                    DoTuoiTBNam = table.Column<int>(nullable: false),
                    DoTuoiTBNu = table.Column<int>(nullable: false),
                    KhamSucKhoeDinhKy = table.Column<int>(nullable: false),
                    TrangPhucRieng = table.Column<int>(nullable: false),
                    PhongChayNo = table.Column<int>(nullable: false),
                    CNVSMoiTruong = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoLuuTru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaPhuong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiaPhuong = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: false, defaultValue: 0),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaPhuong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(nullable: false),
                    LoaiId = table.Column<int>(nullable: false),
                    SucChua = table.Column<int>(nullable: false, defaultValue: 0),
                    DVT = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DichVuPhong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DichVu = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVuPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiDichVu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiDichVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHinhLaoDong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiHinh = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHinhLaoDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MucDoThongThaoNgoaiNgu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MucDo = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MucDoThongThaoNgoaiNgu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NganhDaoTao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNganhDaoTao = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhDaoTao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgoaiNgu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNgoaiNgu = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgoaiNgu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhong = table.Column<string>(nullable: false),
                    SoGiuong = table.Column<int>(nullable: false, defaultValue: 1),
                    LoaiPhongId = table.Column<int>(nullable: false),
                    DichVuId = table.Column<int>(nullable: false),
                    CoSoLuTruId = table.Column<int>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuocTich",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuocTich = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuocTich", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinhChatLaoDong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhChatLaoDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrinhDo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrinhDo = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5a06727f-9ef1-4493-8bcd-fea9f3c1dd9c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a77bfcc-37f5-4f79-b058-d5ee001001fe", "AQAAAAEAACcQAAAAELlP0eJmV3tsIOAVp+g0viISd+n1rB6ZrX3rGiPXfnhB0U571ocYlKfaHqwWQbp/gA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoPhan");

            migrationBuilder.DropTable(
                name: "CoSoLuuTru");

            migrationBuilder.DropTable(
                name: "DiaPhuong");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "DichVuPhong");

            migrationBuilder.DropTable(
                name: "LoaiDichVu");

            migrationBuilder.DropTable(
                name: "LoaiHinh");

            migrationBuilder.DropTable(
                name: "LoaiHinhLaoDong");

            migrationBuilder.DropTable(
                name: "LoaiPhong");

            migrationBuilder.DropTable(
                name: "MucDoThongThaoNgoaiNgu");

            migrationBuilder.DropTable(
                name: "NganhDaoTao");

            migrationBuilder.DropTable(
                name: "NgoaiNgu");

            migrationBuilder.DropTable(
                name: "Phong");

            migrationBuilder.DropTable(
                name: "QuocTich");

            migrationBuilder.DropTable(
                name: "TinhChatLaoDong");

            migrationBuilder.DropTable(
                name: "TrinhDo");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c396d078-8588-4a37-b067-db9fad625def");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9f139bc1-fa98-4c1c-a587-6dd2fe7ebb77", "AQAAAAEAACcQAAAAEPHFufiAjFnbSVXpho7oyfb540RSwsYP6MzdhfdNnXTzKNyz/Htda9vVaupoZM07Sg==" });
        }
    }
}
