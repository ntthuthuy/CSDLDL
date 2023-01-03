using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class ChangeDBNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 787, DateTimeKind.Local).AddTicks(4606),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 279, DateTimeKind.Local).AddTicks(2464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 791, DateTimeKind.Local).AddTicks(4711),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 284, DateTimeKind.Local).AddTicks(3370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 904, DateTimeKind.Local).AddTicks(4420),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 380, DateTimeKind.Local).AddTicks(5771));

            migrationBuilder.AlterColumn<string>(
                name: "GioMoCua",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GioDongCua",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1f4f9dac-60cc-4edb-b198-8d1dda0f72ce");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e68645c-2650-4da7-b497-9f90bc937e69", "AQAAAAEAACcQAAAAEF9sOG6gfWOAZvesMXuludDMhKqmPkeJ/S9qhPD3jvrp+Qpuv8pDcMwDxf1VFic35A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 279, DateTimeKind.Local).AddTicks(2464),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 787, DateTimeKind.Local).AddTicks(4606));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 284, DateTimeKind.Local).AddTicks(3370),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 791, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 380, DateTimeKind.Local).AddTicks(5771),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 904, DateTimeKind.Local).AddTicks(4420));

            migrationBuilder.AlterColumn<int>(
                name: "GioMoCua",
                table: "HoSo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GioDongCua",
                table: "HoSo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "38506f3b-b864-40c2-8d9a-d61320185b93");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ebf6087-1b73-499b-b521-c462ffe09cf6", "AQAAAAEAACcQAAAAEGhFe6PUimC89gOOmzQRlNjv/A6IB09ON/mijMWdEFaRk54dFOlAIpeiG2ZK7sUBRw==" });
        }
    }
}
