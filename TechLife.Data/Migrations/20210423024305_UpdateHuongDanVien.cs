using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateHuongDanVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 174, DateTimeKind.Local).AddTicks(9488),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 640, DateTimeKind.Local).AddTicks(4412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 178, DateTimeKind.Local).AddTicks(7896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 644, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "HuongDanVien",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 287, DateTimeKind.Local).AddTicks(7437),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 764, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2ca00f6e-9785-45b4-9201-c6c5b9686024");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b6e02a7-977c-4340-8e85-5a4b692e193b", "AQAAAAEAACcQAAAAED9lvfjnZnV8DctI+smhXm7k5xxVgbW4wXFmiSKdXRGlhGV4V1S8hz4+Tqr2xOLHxA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "HuongDanVien");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 640, DateTimeKind.Local).AddTicks(4412),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 174, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 644, DateTimeKind.Local).AddTicks(5394),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 178, DateTimeKind.Local).AddTicks(7896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 16, 27, 13, 764, DateTimeKind.Local).AddTicks(9079),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 287, DateTimeKind.Local).AddTicks(7437));

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
    }
}
