using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class updateSoLuong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 599, DateTimeKind.Local).AddTicks(3750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 474, DateTimeKind.Local).AddTicks(4205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 721, DateTimeKind.Local).AddTicks(7457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 751, DateTimeKind.Local).AddTicks(1703));

            migrationBuilder.AlterColumn<int>(
                name: "SoLuong",
                table: "Orders",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "0");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDat",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 724, DateTimeKind.Local).AddTicks(4269),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 755, DateTimeKind.Local).AddTicks(5688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 602, DateTimeKind.Local).AddTicks(4642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 481, DateTimeKind.Local).AddTicks(5219));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 709, DateTimeKind.Local).AddTicks(7291),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 719, DateTimeKind.Local).AddTicks(4374));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "07822cf4-b388-494c-8d56-ce14dc3ee5d3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "383cf84c-92ed-402a-8b19-8917bcc1c68d", "AQAAAAEAACcQAAAAEJ9KUI5lX1SL0dtmf97nsR2dqJM0OA80s7hVuAM7Mh9yiO80Ghy2+kT4CUA86Q/dLw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 474, DateTimeKind.Local).AddTicks(4205),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 599, DateTimeKind.Local).AddTicks(3750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 751, DateTimeKind.Local).AddTicks(1703),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 721, DateTimeKind.Local).AddTicks(7457));

            migrationBuilder.AlterColumn<string>(
                name: "SoLuong",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "0",
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDat",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 755, DateTimeKind.Local).AddTicks(5688),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 724, DateTimeKind.Local).AddTicks(4269));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 481, DateTimeKind.Local).AddTicks(5219),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 602, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 719, DateTimeKind.Local).AddTicks(4374),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 13, 3, 12, 709, DateTimeKind.Local).AddTicks(7291));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "270ee4c7-f72f-45da-9e2a-8e212f026ed3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc5f4043-b5fc-469a-a65b-5059be6746c4", "AQAAAAEAACcQAAAAEBqWUG22F5rjQB2RfOe1I8/Zr/jhq5T3uLwrVTcqYITwMKrdc0hoCIH2e/lYB4sqTw==" });
        }
    }
}
