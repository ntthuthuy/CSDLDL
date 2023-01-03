using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateLoaiPhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoSoLuTruId",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "DichVuId",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "MaPhong",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "SoGiuong",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "TenLoai",
                table: "LoaiPhong");

            migrationBuilder.AddColumn<int>(
                name: "SoNguoiLon",
                table: "Phong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoTreEm",
                table: "Phong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoSoLuTruId",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DienTich",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "GiaPhong",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SoNguoiLon",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoPhong",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoTreEm",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TenPhong",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: false),
                    FileUrl = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsImage = table.Column<bool>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "HanhTrinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(nullable: false),
                    NoiDiId = table.Column<int>(nullable: false),
                    NoiDenId = table.Column<int>(nullable: false),
                    Ngay = table.Column<int>(nullable: false),
                    Gio = table.Column<int>(nullable: false),
                    Phut = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<int>(nullable: false),
                    Mota = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HanhTrinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiId = table.Column<int>(nullable: false),
                    CongTyLuHanhId = table.Column<int>(nullable: false),
                    SoNgay = table.Column<int>(nullable: false),
                    TenChuyenDi = table.Column<string>(nullable: false),
                    MoTaChuyenDi = table.Column<string>(nullable: true),
                    Gia = table.Column<decimal>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f97ba304-af2c-440c-a4db-245df3c8ace4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "04d84938-a90e-45a3-a002-a269148b6f99", "AQAAAAEAACcQAAAAEPmPitSxMe8+hlElm1i6eI+WK99SkGUWFZjxbjfLcPgnVndc6zuTydLLXotvrJMM8Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "HanhTrinh");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropColumn(
                name: "SoNguoiLon",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "SoTreEm",
                table: "Phong");

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

            migrationBuilder.AddColumn<int>(
                name: "CoSoLuTruId",
                table: "Phong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DichVuId",
                table: "Phong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaPhong",
                table: "Phong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoGiuong",
                table: "Phong",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "TenLoai",
                table: "LoaiPhong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "47dc2a11-0dde-404e-9152-11de924eeb5c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0df86eda-ff7c-49dc-ad53-6aa3dff4e568", "AQAAAAEAACcQAAAAEGfXaii8dPtdCRRASYUwPuDy+PxsS1v7U3Wm1bOK0HT5DI+GpVOyedVb2aVH3lZn6A==" });
        }
    }
}
