using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UDDB071220_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GioDongCua",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GioMoCua",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDGianTiep",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDNamNgoaiNuoc",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDNamTrongNuoc",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDNuNgoaiNuoc",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDNuTrongNuoc",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDThoiVu",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDThuongXuyen",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLDTrucTiep",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DonViTinhId",
                table: "BoPhanHoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "BoPhanHoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0f0c77bd-fe43-4280-8a84-7b092f5f9898");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c49714d0-9bca-47ad-b956-6edde551b1df", "AQAAAAEAACcQAAAAECu+7wGugHoMxVf8g5BmawWNchHVejTYo9l/jwVECERhUp1y43O1ev4letfxLstJ3g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioDongCua",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GioMoCua",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDGianTiep",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDNamNgoaiNuoc",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDNamTrongNuoc",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDNuNgoaiNuoc",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDNuTrongNuoc",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDThoiVu",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDThuongXuyen",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoLDTrucTiep",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DonViTinhId",
                table: "BoPhanHoSo");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "BoPhanHoSo");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4e6a8f75-d3e5-49c0-83c1-6918e2eeee1a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e577efd-d9f8-428e-997f-313deccc2968", "AQAAAAEAACcQAAAAEFYgvIMKEyMlAY7oZKoQ+FOGZm7aQ/Uk9L2g7x1YMDaTSwgMydyWYzViLnaeKQAMUw==" });
        }
    }
}
