using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdatethietBiTruyCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 79, DateTimeKind.Local).AddTicks(7790),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 132, DateTimeKind.Local).AddTicks(3359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 202, DateTimeKind.Local).AddTicks(421),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 280, DateTimeKind.Local).AddTicks(6386));

            migrationBuilder.AddColumn<string>(
                name: "LoaiThietBi",
                table: "ThietBi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySuDungMoiNhat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 205, DateTimeKind.Local).AddTicks(9510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 288, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 83, DateTimeKind.Local).AddTicks(8772),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 136, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 183, DateTimeKind.Local).AddTicks(4753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 256, DateTimeKind.Local).AddTicks(8954));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "58935647-0f6b-4cd7-a33d-2859d4bd28c1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0acfa4b9-831d-4a12-8cde-7c3959aca6a6", "AQAAAAEAACcQAAAAEKWjgHuPf0iPgjVpOItLp60J3I7KgSo3TdI91WDwSt5uzVDnDtWoipxrbP9EtYkgSQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiThietBi",
                table: "ThietBi");

            migrationBuilder.DropColumn(
                name: "NgaySuDungMoiNhat",
                table: "ThietBi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 132, DateTimeKind.Local).AddTicks(3359),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 79, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 280, DateTimeKind.Local).AddTicks(6386),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 202, DateTimeKind.Local).AddTicks(421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 288, DateTimeKind.Local).AddTicks(3791),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 205, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 136, DateTimeKind.Local).AddTicks(4688),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 83, DateTimeKind.Local).AddTicks(8772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 256, DateTimeKind.Local).AddTicks(8954),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 183, DateTimeKind.Local).AddTicks(4753));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "da316b64-c32b-49ae-a146-6c68e2ab84c9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9360482-f1d1-45e6-a0bf-180c2cef0130", "AQAAAAEAACcQAAAAEAuY81fhe9UkAz7Dr7qLiYO/vs+P3jpz0Nxvc1yEVoyhmhw95isX++0WEoZ9SRZ3cw==" });
        }
    }
}
