using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDuLieuDL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 262, DateTimeKind.Local).AddTicks(5240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 455, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 266, DateTimeKind.Local).AddTicks(500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 459, DateTimeKind.Local).AddTicks(7195));

            migrationBuilder.AddColumn<bool>(
                name: "IsDatChuan",
                table: "HoSo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCVDatChuan",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SoCVDatChuan",
                table: "HoSo",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6fd02845-09fc-4c52-bb35-43c7cc1b9515");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "924fceb5-a30d-45e5-a3ff-a40fa8bc8380", "AQAAAAEAACcQAAAAEJHD5muWxT9FVIWZp0yMMGTguyL6bSD/14IbKSwOk8hbBgcU8ntMyQKC36b7K/TRpg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDatChuan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayCVDatChuan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoCVDatChuan",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 455, DateTimeKind.Local).AddTicks(9334),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 262, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 23, 21, 12, 9, 459, DateTimeKind.Local).AddTicks(7195),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 266, DateTimeKind.Local).AddTicks(500));

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
    }
}
