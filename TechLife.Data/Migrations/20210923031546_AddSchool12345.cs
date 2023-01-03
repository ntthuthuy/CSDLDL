using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddSchool12345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 906, DateTimeKind.Local).AddTicks(2187));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 47, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 50, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 911, DateTimeKind.Local).AddTicks(6079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 30, DateTimeKind.Local).AddTicks(1051));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e244955-cf25-4dd4-9b40-e0e0cf65ac3f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64c54980-1fa7-46c9-944e-b1cf44d08676", "AQAAAAEAACcQAAAAEJpah1bB1YWEtM91vCXm8U4q3IvuP9P/CoPSVvO3Mn9S1wgVijKzp3iFKDqnuwY/2A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 906, DateTimeKind.Local).AddTicks(2187),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 47, DateTimeKind.Local).AddTicks(5732),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 50, DateTimeKind.Local).AddTicks(9886),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 911, DateTimeKind.Local).AddTicks(6079),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 30, DateTimeKind.Local).AddTicks(1051),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "253d8975-73de-4a02-8c0c-4107d8edff71");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d696501-97b8-4214-b097-eeda807885e3", "AQAAAAEAACcQAAAAEEMi1tLh5ci6+vX8Zao+GA+h60KXES/xgKHE3zdCDy1o9j1UCPpvW7OFD3MT+bo88A==" });
        }
    }
}
