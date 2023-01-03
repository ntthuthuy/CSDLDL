using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateLoaiDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 865, DateTimeKind.Local).AddTicks(1232),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 128, DateTimeKind.Local).AddTicks(1380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDanhGia",
                table: "TinTucDanhGia",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 52, DateTimeKind.Local).AddTicks(1198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 378, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 66, DateTimeKind.Local).AddTicks(4263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 402, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 77, DateTimeKind.Local).AddTicks(5634),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 411, DateTimeKind.Local).AddTicks(9102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 868, DateTimeKind.Local).AddTicks(3772),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 133, DateTimeKind.Local).AddTicks(916));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 40, DateTimeKind.Local).AddTicks(462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 351, DateTimeKind.Local).AddTicks(5713));

            migrationBuilder.AddColumn<string>(
                name: "Loai",
                table: "DanhGia",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loai",
                table: "DanhGia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 128, DateTimeKind.Local).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 865, DateTimeKind.Local).AddTicks(1232));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDanhGia",
                table: "TinTucDanhGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 378, DateTimeKind.Local).AddTicks(1490),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 52, DateTimeKind.Local).AddTicks(1198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 402, DateTimeKind.Local).AddTicks(7687),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 66, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 411, DateTimeKind.Local).AddTicks(9102),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 77, DateTimeKind.Local).AddTicks(5634));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 133, DateTimeKind.Local).AddTicks(916),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 2, 868, DateTimeKind.Local).AddTicks(3772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 351, DateTimeKind.Local).AddTicks(5713),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 39, 3, 40, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2a1caeab-b3fd-4148-9c5b-3577c5fade60");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d976664-248b-4136-ba8e-5efe48755a28", "AQAAAAEAACcQAAAAEI7M0yvJeWMkf9aXdB07zoYvvF8pRxsmOz7BsvwpgrGPHxhzyYIAZXunswvLnwYc9A==" });
        }
    }
}
