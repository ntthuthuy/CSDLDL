using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateTBLHoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 269, DateTimeKind.Local).AddTicks(2676),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 382, DateTimeKind.Local).AddTicks(3075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 271, DateTimeKind.Local).AddTicks(5213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 384, DateTimeKind.Local).AddTicks(9765));

            migrationBuilder.AddColumn<string>(
                name: "TenVietTat",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriTrenBanDo",
                table: "HoSo",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "262cb54e-7213-4d9e-99ea-d4616a0e9621");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "51e67e6f-5328-437c-8359-d262ffbcc2cc", "AQAAAAEAACcQAAAAECSgvQiGkkOftOcezyegKHhkzFMiJ3B26WTTOrZElVQ4eDa48vuS8wk6UaZayT/YkQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenVietTat",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "ViTriTrenBanDo",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 382, DateTimeKind.Local).AddTicks(3075),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 269, DateTimeKind.Local).AddTicks(2676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 384, DateTimeKind.Local).AddTicks(9765),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 271, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ea93eac8-71d9-4ebe-ae3d-f8e2ef6eafaf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a40d382-c36c-4802-ab0d-185e6b83dcd3", "AQAAAAEAACcQAAAAEO90LqUHIGVz1kz+kVGkD4I9neZSaEgzvGMvoJJBlCpcfV7ucuvm2YyLss2bMXWXzQ==" });
        }
    }
}
