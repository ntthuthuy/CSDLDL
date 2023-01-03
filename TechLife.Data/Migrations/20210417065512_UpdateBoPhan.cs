using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateBoPhan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 788, DateTimeKind.Local).AddTicks(7027),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 854, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 792, DateTimeKind.Local).AddTicks(7578),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 858, DateTimeKind.Local).AddTicks(1889));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 974, DateTimeKind.Local).AddTicks(7427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 963, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.AddColumn<string>(
                name: "LinhVucId",
                table: "BoPhan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViTri",
                table: "BoPhan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f131bb95-ede8-4582-abbb-211512037bff");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef92ac5e-8c96-4ed9-92c9-a0acd0bb4508", "AQAAAAEAACcQAAAAEKH9m993A+UfSwJN5jvY6Pe6q/41TNy3V1cggVzo4OP2B8aV5vlhtu7UXpSgM1mB5g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinhVucId",
                table: "BoPhan");

            migrationBuilder.DropColumn(
                name: "ViTri",
                table: "BoPhan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 854, DateTimeKind.Local).AddTicks(74),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 788, DateTimeKind.Local).AddTicks(7027));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 858, DateTimeKind.Local).AddTicks(1889),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 792, DateTimeKind.Local).AddTicks(7578));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 12, 27, 31, 963, DateTimeKind.Local).AddTicks(8968),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 974, DateTimeKind.Local).AddTicks(7427));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6ccac521-effb-4b88-9639-a2dd04890cfb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e4a993d0-4751-4270-8194-e5a48ad058e6", "AQAAAAEAACcQAAAAEJ4nt1OkNwCnH5EX/2VZieCBaqlOdz4PaSAZVjWgs5y7pRAwBy8+UK4HGz2IaGEvkQ==" });
        }
    }
}
