using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddColIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 525, DateTimeKind.Local).AddTicks(4191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 151, DateTimeKind.Local).AddTicks(650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 531, DateTimeKind.Local).AddTicks(821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 157, DateTimeKind.Local).AddTicks(4482));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 625, DateTimeKind.Local).AddTicks(5567),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 287, DateTimeKind.Local).AddTicks(2972));

            migrationBuilder.AddColumn<string>(
                name: "IconMobile",
                table: "ChuyenMuc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconWeb",
                table: "ChuyenMuc",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e05922eb-89e2-42b1-b7c4-b642c007802a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd1946ec-e87d-4800-9493-f145ceb4f7d0", "AQAAAAEAACcQAAAAEPAYdyPCl4+QbqW2yViWBm3i+Scv9k7Cj7Ss5c/zV8xZm0aJaPA5qfOL+ha+Mpbb7w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconMobile",
                table: "ChuyenMuc");

            migrationBuilder.DropColumn(
                name: "IconWeb",
                table: "ChuyenMuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 151, DateTimeKind.Local).AddTicks(650),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 525, DateTimeKind.Local).AddTicks(4191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 157, DateTimeKind.Local).AddTicks(4482),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 531, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 9, 36, 2, 287, DateTimeKind.Local).AddTicks(2972),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 625, DateTimeKind.Local).AddTicks(5567));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "91b6d215-ccb2-479e-8aeb-67a3ededa366");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f4d89b38-28a9-481b-b2f3-992c32a7dafd", "AQAAAAEAACcQAAAAEMtPaEPvG12ODkqVhs6JWYey8/dapQqH9uwdyQWsnyUv0adGV9D5Bxyx0sQMeS3qTA==" });
        }
    }
}
