using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Udate2204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 729, DateTimeKind.Local).AddTicks(2583),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 73, DateTimeKind.Local).AddTicks(1221));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 736, DateTimeKind.Local).AddTicks(3762),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 76, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 906, DateTimeKind.Local).AddTicks(899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 179, DateTimeKind.Local).AddTicks(9425));

            migrationBuilder.AlterColumn<int>(
                name: "PhuongXaId",
                table: "HoSo",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "449c12d9-0951-4384-a3b7-1c9a671962fa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8fe51732-61a0-4710-a0da-514a8c3d7b45", "AQAAAAEAACcQAAAAEFmznt9gzcKOPBLtbgRA3KU1xUgPPffOCSfuQvb3uIyqB0AAPciIyjajSuMFyNfZbA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 73, DateTimeKind.Local).AddTicks(1221),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 729, DateTimeKind.Local).AddTicks(2583));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 76, DateTimeKind.Local).AddTicks(9074),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 736, DateTimeKind.Local).AddTicks(3762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 179, DateTimeKind.Local).AddTicks(9425),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 906, DateTimeKind.Local).AddTicks(899));

            migrationBuilder.AlterColumn<int>(
                name: "PhuongXaId",
                table: "HoSo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "93d83dfd-c011-4b7a-87fe-a04d054bbf04");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5ba774d8-fd82-4b30-8d9e-dfebc51e0400", "AQAAAAEAACcQAAAAEEXN+vGJT1X51k6fdNwbwTcwObfR2133h64MceRB54xjOgiOvbaZIEUItJcWFlJdPg==" });
        }
    }
}
