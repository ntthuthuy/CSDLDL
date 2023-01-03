using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateQuyMoNhaHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 828, DateTimeKind.Local).AddTicks(4200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 444, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.AlterColumn<string>(
                name: "TenGoi",
                table: "QuyMoNhaHangLuuTru",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 832, DateTimeKind.Local).AddTicks(8174),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 448, DateTimeKind.Local).AddTicks(5503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 949, DateTimeKind.Local).AddTicks(2726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 539, DateTimeKind.Local).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c54dc4a1-4980-4c57-a0ab-b998731d4509");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5bbeff8b-8a75-4b57-84c0-cc4d18435c7a", "AQAAAAEAACcQAAAAEIn5PepzNakou0F1Mgqp9FXEurePU9d5RRBeQJF1i9y1PmvTZLXyZdBUxSUbvOXCKQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 444, DateTimeKind.Local).AddTicks(8103),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 828, DateTimeKind.Local).AddTicks(4200));

            migrationBuilder.AlterColumn<string>(
                name: "TenGoi",
                table: "QuyMoNhaHangLuuTru",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 448, DateTimeKind.Local).AddTicks(5503),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 832, DateTimeKind.Local).AddTicks(8174));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 539, DateTimeKind.Local).AddTicks(8581),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 949, DateTimeKind.Local).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1ee8bbae-58a5-4ddd-8fab-db5d36f765da");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c1998594-31f7-4087-9616-ef01f6c4e3b5", "AQAAAAEAACcQAAAAEOoDpprTpZp9a/phwQR8Ktt7cO5q29Jh7ioHNNbyvd10AevX2T1QJ00cIy7MioQn+A==" });
        }
    }
}
