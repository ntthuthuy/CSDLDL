using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UDDB071220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DichVuHoSo",
                table: "DichVuHoSo");

            migrationBuilder.DropColumn(
                name: "CoSoLuTruId",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "DienTich",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "GiaPhong",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "SoNguoiLon",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "SoPhong",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "SoTreEm",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "TenPhong",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "ThoiDiemBatDauKinhDoan",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiDiemBatDauKinhDoanh",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DichVuHoSo",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DichVuHoSo",
                table: "DichVuHoSo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BoPhanHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoPhanId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoPhanHoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhongHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHienThi = table.Column<string>(nullable: true),
                    LoaiPhongId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    LoaiGiuongId = table.Column<int>(nullable: false),
                    SoPhong = table.Column<int>(nullable: false),
                    DienTich = table.Column<int>(nullable: false),
                    GiaPhong = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhongHoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MucDoTTNNHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MucDoId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonViTinhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MucDoTTNNHoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgoaiNguHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgoaiNguId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonViTinhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgoaiNguHoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TienNghi",
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
                    table.PrimaryKey("PK_TienNghi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TienNghiHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TienNghiId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    IsPhuPhi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienNghiHoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrinhDoHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrinhDoId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonViTinhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDoHoSo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "abf85537-8ee3-4a3e-9395-ac4705085ec6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aadcaf9b-d246-4ae4-b007-f1281f06a35b", "AQAAAAEAACcQAAAAEESjBBDe2p3QEuGMsYyWEvCvEtn/D5kDcCjDy/kE0o7Phmc3KCBsAqHk4zcd33n4Bg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoPhanHoSo");

            migrationBuilder.DropTable(
                name: "LoaiPhongHoSo");

            migrationBuilder.DropTable(
                name: "MucDoTTNNHoSo");

            migrationBuilder.DropTable(
                name: "NgoaiNguHoSo");

            migrationBuilder.DropTable(
                name: "TienNghi");

            migrationBuilder.DropTable(
                name: "TienNghiHoSo");

            migrationBuilder.DropTable(
                name: "TrinhDoHoSo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DichVuHoSo",
                table: "DichVuHoSo");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "LoaiPhong");

            migrationBuilder.DropColumn(
                name: "ThoiDiemBatDauKinhDoanh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DichVuHoSo");

            migrationBuilder.AddColumn<int>(
                name: "CoSoLuTruId",
                table: "LoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DienTich",
                table: "LoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "GiaPhong",
                table: "LoaiPhong",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SoNguoiLon",
                table: "LoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoPhong",
                table: "LoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoTreEm",
                table: "LoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TenPhong",
                table: "LoaiPhong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiDiemBatDauKinhDoan",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DichVuHoSo",
                table: "DichVuHoSo",
                columns: new[] { "HoSoId", "DichVuId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e8e51f59-e79c-4859-8547-783a27f1633a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ea12e02-c85a-4c2d-b4a7-768bd6b8cf73", "AQAAAAEAACcQAAAAEAsvXIp3f50w3iGYuapGf7RfJHEcrE4+8unL9QC2YZ+czBTgcV5I/dblrJ3DlSCtYw==" });
        }
    }
}
