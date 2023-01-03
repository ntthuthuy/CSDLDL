using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTienNghi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 73, DateTimeKind.Local).AddTicks(1221),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 788, DateTimeKind.Local).AddTicks(7027));

            migrationBuilder.AddColumn<double>(
                name: "DonGia",
                table: "TienNghiHoSo",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 76, DateTimeKind.Local).AddTicks(9074),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 792, DateTimeKind.Local).AddTicks(7578));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 179, DateTimeKind.Local).AddTicks(9425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 974, DateTimeKind.Local).AddTicks(7427));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "93d83dfd-c011-4b7a-87fe-a04d054bbf04");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5ba774d8-fd82-4b30-8d9e-dfebc51e0400", "AQAAAAEAACcQAAAAEEXN+vGJT1X51k6fdNwbwTcwObfR2133h64MceRB54xjOgiOvbaZIEUItJcWFlJdPg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "TienNghiHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 788, DateTimeKind.Local).AddTicks(7027),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 73, DateTimeKind.Local).AddTicks(1221));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 792, DateTimeKind.Local).AddTicks(7578),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 76, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 17, 13, 55, 10, 974, DateTimeKind.Local).AddTicks(7427),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 18, 10, 28, 25, 179, DateTimeKind.Local).AddTicks(9425));

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
    }
}
