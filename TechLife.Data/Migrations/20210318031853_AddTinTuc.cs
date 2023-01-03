using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTinTuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 804, DateTimeKind.Local).AddTicks(319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 319, DateTimeKind.Local).AddTicks(6573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 322, DateTimeKind.Local).AddTicks(5460));

            migrationBuilder.CreateTable(
                name: "ChuyenMuc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    TieuDe = table.Column<string>(nullable: true),
                    TuKhoa = table.Column<string>(nullable: true),
                    UrlImage = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    ThuTuHienThi = table.Column<int>(nullable: false),
                    IsHienThiMenu = table.Column<bool>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    UserId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgonNgu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Ten = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgonNgu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuyenMucId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    NgonNguId = table.Column<string>(nullable: false),
                    TieuDe = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    AnhDaiDien = table.Column<string>(nullable: false),
                    MoTaAnh = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: false),
                    TuKhoa = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: false),
                    NguonTin = table.Column<string>(nullable: true),
                    TacGia = table.Column<string>(nullable: true),
                    TacQuyen = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    NguonNgonNguId = table.Column<string>(nullable: true),
                    IsTinBaiChiaSe = table.Column<bool>(nullable: false),
                    IsTinTieuDiem = table.Column<bool>(nullable: false),
                    IsTinKhuyenMai = table.Column<bool>(nullable: false),
                    NgayCongBo = table.Column<DateTime>(nullable: true),
                    NgayKetThuc = table.Column<DateTime>(nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    NguoiDangId = table.Column<int>(nullable: false),
                    NguoiDuyetId = table.Column<int>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTuc", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NgonNgu",
                columns: new[] { "Id", "IsDefault", "Ten" },
                values: new object[,]
                {
                    { "vi", true, "Tiếng Việt" },
                    { "en", false, "English" },
                    { "ja", false, "Japan" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d76f437a-8bec-41a4-9b60-0f565b8e3178");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2bffd3db-46a4-4923-a82a-4cf7a7e4f544", "AQAAAAEAACcQAAAAELYm8HR3Fy68oG6B4evJdbCdkCpIcRZPflRKUIa3J1rpNRhC3BjWSEJEV1OGQh4qjg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuyenMuc");

            migrationBuilder.DropTable(
                name: "NgonNgu");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 319, DateTimeKind.Local).AddTicks(6573),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 804, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 322, DateTimeKind.Local).AddTicks(5460),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "801d1fa9-7b1f-4832-87bb-845fb22cb741");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "293686cf-63a5-4c52-88a9-29c79060e4a4", "AQAAAAEAACcQAAAAENtQI0tz9Mp/FGlwmSuMrJL+hU0PQ40VkaOiClfaZxs/iwJQ4THvEDK0uAvHyoz9rQ==" });
        }
    }
}
