using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddKetQuaColm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 241, DateTimeKind.Local).AddTicks(7421),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 804, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 249, DateTimeKind.Local).AddTicks(5882),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475));


            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "32bb4c3b-2b39-46db-bc9e-b286db2a7d5e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "20dc9554-d209-4058-bb8d-c1985f5c64e5", "AQAAAAEAACcQAAAAECisiomDuPhtgRP8fp6qrW5GFqFSKLmou3ip3hX7supszpl/J8REQVKoqAAe6OO2Jw==" });
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
                oldDefaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 241, DateTimeKind.Local).AddTicks(7421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 18, 10, 18, 52, 807, DateTimeKind.Local).AddTicks(475),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 249, DateTimeKind.Local).AddTicks(5882));

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
