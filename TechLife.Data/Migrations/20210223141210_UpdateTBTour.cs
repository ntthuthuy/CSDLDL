using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateTBTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 455, DateTimeKind.Local).AddTicks(9334),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 31, DateTimeKind.Local).AddTicks(9657));

            migrationBuilder.AddColumn<int>(
                name: "HinhThucId",
                table: "Tours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHangNgay",
                table: "Tours",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KhoiHanhTu",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LichTrinh",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTour",
                table: "Tours",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 459, DateTimeKind.Local).AddTicks(7195),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 33, DateTimeKind.Local).AddTicks(8808));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "23f08f50-c2fa-476e-8a47-d2f7b65f0918");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05b718d3-94bc-40b1-bdf9-11549468e329", "AQAAAAEAACcQAAAAECRHPrIwuoTU3Rnr3D4YBpt4rSGnyWbwhssvxpwYkjSd+TMZKvTwTIFK/XoX/R6O2Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhThucId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "IsHangNgay",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "KhoiHanhTu",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LichTrinh",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MaTour",
                table: "Tours");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 31, DateTimeKind.Local).AddTicks(9657),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 455, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 33, DateTimeKind.Local).AddTicks(8808),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 459, DateTimeKind.Local).AddTicks(7195));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f217060c-e67e-4bc5-87e8-1bc64ad5e509");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d642b93-1648-49d5-ba9e-300359676ec7", "AQAAAAEAACcQAAAAEJDG/1XA+ED1VRGO15b5hTvmaKKUjVtQazWMzaj3ZTSIqL8KOTMvNBlyFFznVtOAHA==" });
        }
    }
}
