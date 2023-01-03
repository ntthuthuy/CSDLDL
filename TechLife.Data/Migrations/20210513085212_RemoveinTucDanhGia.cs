using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class RemoveinTucDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 804, DateTimeKind.Local).AddTicks(4271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 865, DateTimeKind.Local).AddTicks(1232));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDanhGia",
                table: "TinTucDanhGia",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 857, DateTimeKind.Local).AddTicks(6160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 52, DateTimeKind.Local).AddTicks(1198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 861, DateTimeKind.Local).AddTicks(1483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 66, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 862, DateTimeKind.Local).AddTicks(5891),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 77, DateTimeKind.Local).AddTicks(5634));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 806, DateTimeKind.Local).AddTicks(3582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 868, DateTimeKind.Local).AddTicks(3772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 851, DateTimeKind.Local).AddTicks(5813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 40, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3ad21bc2-5bd5-45aa-8c14-98742856c5d8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8240bb0-c9b8-49fe-8033-7f42a59d763c", "AQAAAAEAACcQAAAAEH8mt44XHp4ywaxMBDOcrPhGvME7gSQ46NFkjvr37UwOtEYWjxiIl2x10IHexqCglQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 865, DateTimeKind.Local).AddTicks(1232),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 804, DateTimeKind.Local).AddTicks(4271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDanhGia",
                table: "TinTucDanhGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 52, DateTimeKind.Local).AddTicks(1198),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 857, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 66, DateTimeKind.Local).AddTicks(4263),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 861, DateTimeKind.Local).AddTicks(1483));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 77, DateTimeKind.Local).AddTicks(5634),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 862, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 868, DateTimeKind.Local).AddTicks(3772),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 806, DateTimeKind.Local).AddTicks(3582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 40, DateTimeKind.Local).AddTicks(462),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 851, DateTimeKind.Local).AddTicks(5813));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2b7ade44-c626-4515-87c1-48cae31122f7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ad8a1d82-7d51-489e-868e-436291c49be7", "AQAAAAEAACcQAAAAEMQp62/x8KNKrQIoP0nL+tAAKkZaQbvMBGZuWNeNEqfH9fP81iTJCsalCJz6mDAhRg==" });
        }
    }
}
