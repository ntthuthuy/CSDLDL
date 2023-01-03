using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Create_LoaiGiuongPhongh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 681, DateTimeKind.Local).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 750, DateTimeKind.Local).AddTicks(6806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 752, DateTimeKind.Local).AddTicks(5624),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 683, DateTimeKind.Local).AddTicks(5807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 741, DateTimeKind.Local).AddTicks(1785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189));

            migrationBuilder.CreateTable(
                name: "LoaiGiuongPhong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    SoGiuong = table.Column<int>(nullable: false),
                    LuuTruId = table.Column<int>(nullable: false),
                    GiaPhong = table.Column<decimal>(nullable: false),
                    GiaGiuongPhu = table.Column<decimal>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGiuongPhong", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e8ef272-1045-486c-a9ed-f384375d3e66");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55d62e73-db5a-41b5-baa1-9dedd0d2c719", "AQAAAAEAACcQAAAAEOJBOWMxG/c0o/lJqOFhbKPPKYezGT4ESEB3S9pEMm9ja3X+KXU5FuhyyX8xEWs0Wg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoaiGiuongPhong");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 681, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 750, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 752, DateTimeKind.Local).AddTicks(5624));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 683, DateTimeKind.Local).AddTicks(5807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 741, DateTimeKind.Local).AddTicks(1785));

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
    }
}
