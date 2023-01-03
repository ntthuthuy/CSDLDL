using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UPDBGioDongMoCua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 621, DateTimeKind.Local).AddTicks(9727),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 787, DateTimeKind.Local).AddTicks(4606));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 624, DateTimeKind.Local).AddTicks(6840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 791, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 686, DateTimeKind.Local).AddTicks(8381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 904, DateTimeKind.Local).AddTicks(4420));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0ec94c5d-a126-4a12-bdd7-78ae3b02b632");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f83a93e0-4f6e-4642-9f1c-e3607858c9ce", "AQAAAAEAACcQAAAAEPXtThz5OTEruaj5/Ih2F3Y2I6yR/SaxpNcPz9vyb/RjEo9xtT4un5zDdcVxBQiCCA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 787, DateTimeKind.Local).AddTicks(4606),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 621, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 791, DateTimeKind.Local).AddTicks(4711),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 624, DateTimeKind.Local).AddTicks(6840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 11, 31, 904, DateTimeKind.Local).AddTicks(4420),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 686, DateTimeKind.Local).AddTicks(8381));

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
    }
}
