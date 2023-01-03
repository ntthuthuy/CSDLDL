using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddIstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 623, DateTimeKind.Local).AddTicks(2377),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 97, DateTimeKind.Local).AddTicks(678));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 627, DateTimeKind.Local).AddTicks(2955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 101, DateTimeKind.Local).AddTicks(8430));

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "HoSoVanBan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 747, DateTimeKind.Local).AddTicks(4244),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 232, DateTimeKind.Local).AddTicks(7553));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "HoSoVanBan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 97, DateTimeKind.Local).AddTicks(678),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 623, DateTimeKind.Local).AddTicks(2377));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 101, DateTimeKind.Local).AddTicks(8430),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 627, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 232, DateTimeKind.Local).AddTicks(7553),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 11, 5, 26, 747, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3b9e0e35-17bf-4135-a3d1-37c3a3a8cace");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d209e0c-6dd0-4f12-bc06-ae778bed2bdb", "AQAAAAEAACcQAAAAEOXMZjOl300FLbqCFDkeR4Qw/9JksByqVQjFIRfKR/d2xVS0hcDPU0H/QUQS6WVGUQ==" });
        }
    }
}
