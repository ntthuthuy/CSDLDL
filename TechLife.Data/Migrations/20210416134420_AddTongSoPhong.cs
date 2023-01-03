using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTongSoPhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 66, DateTimeKind.Local).AddTicks(7441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 623, DateTimeKind.Local).AddTicks(2377));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 73, DateTimeKind.Local).AddTicks(118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 627, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 270, DateTimeKind.Local).AddTicks(1687),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 747, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.AddColumn<int>(
                name: "TongSoGiuong",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TongSoPhong",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongSoGiuong",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TongSoPhong",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 623, DateTimeKind.Local).AddTicks(2377),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 66, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 627, DateTimeKind.Local).AddTicks(2955),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 73, DateTimeKind.Local).AddTicks(118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 747, DateTimeKind.Local).AddTicks(4244),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 20, 44, 18, 270, DateTimeKind.Local).AddTicks(1687));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8e61c3cb-bc20-4ce4-8916-7d63b3857b93");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2268d189-072a-4bec-af7d-b9a10c4e151f", "AQAAAAEAACcQAAAAEP8WfnUPKWcz7e9TFaykw/FWdO8TC6cbutcn/nukQZPV58I/rKLt/sDP3+GKgYU8iA==" });
        }
    }
}
