using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class ChangeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoSoLuuTru");

            migrationBuilder.AddColumn<int>(
                name: "LinhVucKinhDoanhId",
                table: "LoaiHinh",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LinhVucKinhDoanhId",
                table: "DichVu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCongTy = table.Column<string>(nullable: false),
                    Ten = table.Column<string>(nullable: false),
                    LinhVucKinhDoanhId = table.Column<int>(nullable: false),
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
                    KhamSucKhoeDinhKy = table.Column<bool>(nullable: false),
                    TrangPhucRieng = table.Column<bool>(nullable: false),
                    PhongChayNo = table.Column<bool>(nullable: false),
                    CNVSMoiTruong = table.Column<bool>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "52a472eb-858f-4e3d-a2a0-063c7332cd85");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d11537f3-f6ac-44d9-a4bc-ba3f43fa2e3d", "AQAAAAEAACcQAAAAEEJ89jVc+NPfcCU/zT2Lhy8iStS70rr/FEpxHdGUy0wVosbp3juz+hOcHbMBWVSFdw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSo");

            migrationBuilder.DropColumn(
                name: "LinhVucKinhDoanhId",
                table: "LoaiHinh");

            migrationBuilder.DropColumn(
                name: "LinhVucKinhDoanhId",
                table: "DichVu");

            migrationBuilder.CreateTable(
                name: "CoSoLuuTru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNVSMoiTruong = table.Column<int>(type: "int", nullable: false),
                    ChucVuNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTichMatBang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichMatBangXayDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichXayDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoTuoiTBNam = table.Column<int>(type: "int", nullable: false),
                    DoTuoiTBNu = table.Column<int>(type: "int", nullable: false),
                    DuongPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinhNguoiDaiDien = table.Column<int>(type: "int", nullable: false),
                    HangSao = table.Column<int>(type: "int", nullable: false),
                    HoTenNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    KhamSucKhoeDinhKy = table.Column<int>(type: "int", nullable: false),
                    LoaiHinhId = table.Column<int>(type: "int", nullable: false),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhongChayNo = table.Column<int>(type: "int", nullable: false),
                    PhuongXaId = table.Column<int>(type: "int", nullable: false),
                    QuanHuyenId = table.Column<int>(type: "int", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoaiNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoGiayPhep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLanChuyenChu = table.Column<int>(type: "int", nullable: false),
                    SoLuongLaoDong = table.Column<int>(type: "int", nullable: false),
                    SoNha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTang = table.Column<int>(type: "int", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiDiemBatDauKinhDoan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhThanhId = table.Column<int>(type: "int", nullable: false),
                    TongVonDauTuBanDau = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongVonDauTuBoSung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangPhucRieng = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoLuuTru", x => x.Id);
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
    }
}
