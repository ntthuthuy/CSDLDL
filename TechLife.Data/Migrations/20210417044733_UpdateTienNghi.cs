using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateTienNghi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 531, DateTimeKind.Local).AddTicks(8584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 66, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.AlterColumn<string>(
                name: "LinhVucId",
                table: "TienNghi",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 535, DateTimeKind.Local).AddTicks(2353),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 73, DateTimeKind.Local).AddTicks(118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 660, DateTimeKind.Local).AddTicks(2197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 270, DateTimeKind.Local).AddTicks(1687));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 66, DateTimeKind.Local).AddTicks(7441),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 531, DateTimeKind.Local).AddTicks(8584));

            migrationBuilder.AlterColumn<int>(
                name: "LinhVucId",
                table: "TienNghi",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 73, DateTimeKind.Local).AddTicks(118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 535, DateTimeKind.Local).AddTicks(2353));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 270, DateTimeKind.Local).AddTicks(1687),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 11, 47, 31, 660, DateTimeKind.Local).AddTicks(2197));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1c50cdc6-d311-4718-b888-11425caccf1c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80c436ce-c75d-4082-86e7-3ef9995dadce", "AQAAAAEAACcQAAAAEDSO53XM3KxrKJoIJqnSXoa8HI9njC3YY1KpRSWgp5HKeXWvfMhDZAF/UrD2YQ1ycw==" });
        }
    }
}
