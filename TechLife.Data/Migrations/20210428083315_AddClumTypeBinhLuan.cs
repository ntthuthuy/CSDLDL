using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddClumTypeBinhLuan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 28, DateTimeKind.Local).AddTicks(8115),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 270, DateTimeKind.Local).AddTicks(3859));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 31, DateTimeKind.Local).AddTicks(4906),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 272, DateTimeKind.Local).AddTicks(8230));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 121, DateTimeKind.Local).AddTicks(1210),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 337, DateTimeKind.Local).AddTicks(6668));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "BinhLuan",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2e4c55b0-e989-4869-b722-26455dcfe9e6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86ca7eb7-b511-4ab0-81f5-322607eec697", "AQAAAAEAACcQAAAAEC8G6mJf82oeH3l0TnkAs5RydMxdK1r+FDhT50c/W3wLqc4G3y5jDDK0eDJw0sIclw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "BinhLuan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 270, DateTimeKind.Local).AddTicks(3859),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 28, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 272, DateTimeKind.Local).AddTicks(8230),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 31, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 337, DateTimeKind.Local).AddTicks(6668),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 33, 14, 121, DateTimeKind.Local).AddTicks(1210));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "bad5217b-4a8b-435d-863f-5093466f7c27");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82183aab-61a1-41ef-9aa6-5a8f41ded75e", "AQAAAAEAACcQAAAAEGoZ/LmaHtOC7YYisyl9HZiupIVqMYvySGQ3uNjiJzB7OpA23ATlJocfkpUgMenSKg==" });
        }
    }
}
