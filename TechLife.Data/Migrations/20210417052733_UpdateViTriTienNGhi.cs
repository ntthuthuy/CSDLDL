using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateViTriTienNGhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 854, DateTimeKind.Local).AddTicks(74),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 531, DateTimeKind.Local).AddTicks(8584));

            migrationBuilder.AddColumn<int>(
                name: "ViTri",
                table: "TienNghi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 858, DateTimeKind.Local).AddTicks(1889),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 535, DateTimeKind.Local).AddTicks(2353));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 963, DateTimeKind.Local).AddTicks(8968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 660, DateTimeKind.Local).AddTicks(2197));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6ccac521-effb-4b88-9639-a2dd04890cfb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e4a993d0-4751-4270-8194-e5a48ad058e6", "AQAAAAEAACcQAAAAEJ4nt1OkNwCnH5EX/2VZieCBaqlOdz4PaSAZVjWgs5y7pRAwBy8+UK4HGz2IaGEvkQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViTri",
                table: "TienNghi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 531, DateTimeKind.Local).AddTicks(8584),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 854, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 535, DateTimeKind.Local).AddTicks(2353),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 858, DateTimeKind.Local).AddTicks(1889));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 660, DateTimeKind.Local).AddTicks(2197),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 963, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e1fe9f69-a70c-447f-8539-ffc08ca60f5c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0013ca6c-6711-4765-81fc-a36afebced73", "AQAAAAEAACcQAAAAEB7GGuD188PY/B2gbCEuz6cyCYSdfqQwzzQm98JPJsCYgkBRvdy4qVpBakwz3FpiyA==" });
        }
    }
}
