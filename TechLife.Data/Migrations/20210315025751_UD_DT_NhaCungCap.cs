using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UD_DT_NhaCungCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 319, DateTimeKind.Local).AddTicks(6573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 426, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDangKy",
                table: "NhaCungCap",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 322, DateTimeKind.Local).AddTicks(5460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 439, DateTimeKind.Local).AddTicks(4565));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "801d1fa9-7b1f-4832-87bb-845fb22cb741");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "293686cf-63a5-4c52-88a9-29c79060e4a4", "AQAAAAEAACcQAAAAENtQI0tz9Mp/FGlwmSuMrJL+hU0PQ40VkaOiClfaZxs/iwJQ4THvEDK0uAvHyoz9rQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 426, DateTimeKind.Local).AddTicks(5213),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 319, DateTimeKind.Local).AddTicks(6573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDangKy",
                table: "NhaCungCap",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 439, DateTimeKind.Local).AddTicks(4565),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 15, 9, 57, 50, 322, DateTimeKind.Local).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "655b74a8-0a12-4380-973d-fec2b5e1ff4c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5c17b35-68e6-437c-911c-8f8d44a6d292", "AQAAAAEAACcQAAAAEKzaeUYxfdblTtFf5mnvkKrMzgRCniJHzsyIKM+hVKjDxGUa7zurBJIPXe2d13YMkQ==" });
        }
    }
}
