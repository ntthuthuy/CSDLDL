using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class ADDBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 552, DateTimeKind.Local).AddTicks(5699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 804, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 556, DateTimeKind.Local).AddTicks(6532),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "FileUploads",
                nullable: true);


            migrationBuilder.CreateTable(
                name: "HoSoThanhTra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoId = table.Column<int>(nullable: false),
                    TruongDoan = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    KetLuan = table.Column<string>(nullable: true),
                    ThoiGian = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 673, DateTimeKind.Local).AddTicks(9905)),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoThanhTra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VanBanHoSoThanhTra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoThanhTraId = table.Column<int>(nullable: false),
                    SoHieu = table.Column<string>(nullable: true),
                    TenVanBan = table.Column<string>(nullable: true),
                    NgayKy = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanBanHoSoThanhTra", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e84b300-236b-4a86-b201-a4fe8de3c4c8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c906e1f6-787f-4244-ba41-243aac9f5b16", "AQAAAAEAACcQAAAAEI1h+bpgFs6emWMQNtLsGjNm7WJ2dGn8vSCrbJQ+4+jw4LsZhBV1va9+3UT+/vjQ5A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoThanhTra");

            migrationBuilder.DropTable(
                name: "VanBanHoSoThanhTra");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "NgonNguId",
                table: "ChuyenMuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 804, DateTimeKind.Local).AddTicks(319),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 552, DateTimeKind.Local).AddTicks(5699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 556, DateTimeKind.Local).AddTicks(6532));

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
    }
}
