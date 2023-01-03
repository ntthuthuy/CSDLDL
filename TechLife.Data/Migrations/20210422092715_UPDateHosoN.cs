using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UPDateHosoN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 640, DateTimeKind.Local).AddTicks(4412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 447, DateTimeKind.Local).AddTicks(2807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 644, DateTimeKind.Local).AddTicks(5394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 450, DateTimeKind.Local).AddTicks(7708));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 764, DateTimeKind.Local).AddTicks(9079),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 543, DateTimeKind.Local).AddTicks(6072));

            migrationBuilder.AlterColumn<int>(
                name: "PhuongXaId",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6cd6b83d-85bb-44f1-8836-d24d9cd47631");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31f2a053-281c-409b-9b1d-a9f58a7e7f99", "AQAAAAEAACcQAAAAEDDEuN+ei+9lsikqhoBJMQb5U/ky/3Jw1P6hHvvg3NS8ItTFVxSo36gK0b7wRBv9hQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 447, DateTimeKind.Local).AddTicks(2807),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 640, DateTimeKind.Local).AddTicks(4412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 450, DateTimeKind.Local).AddTicks(7708),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 644, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 543, DateTimeKind.Local).AddTicks(6072),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 764, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.AlterColumn<int>(
                name: "PhuongXaId",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1a2034ab-374e-4a16-8281-afa5e900ee54");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62fa62d2-4d10-409c-9e13-9148fe1ac7ff", "AQAAAAEAACcQAAAAEJf70wsyOPqfXMoNqx0ltqV/21jdtMRUE2ZZjEJtSqJGrMRz0ilFj74WCobSGRulMQ==" });
        }
    }
}
