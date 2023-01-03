using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateGiayPhepId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 97, DateTimeKind.Local).AddTicks(678),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 621, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 101, DateTimeKind.Local).AddTicks(8430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 624, DateTimeKind.Local).AddTicks(6840));

            migrationBuilder.AddColumn<int>(
                name: "GiayPhepId",
                table: "HoSoVanBan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 232, DateTimeKind.Local).AddTicks(7553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 686, DateTimeKind.Local).AddTicks(8381));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiayPhepId",
                table: "HoSoVanBan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 621, DateTimeKind.Local).AddTicks(9727),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 97, DateTimeKind.Local).AddTicks(678));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 624, DateTimeKind.Local).AddTicks(6840),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 101, DateTimeKind.Local).AddTicks(8430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 16, 10, 22, 2, 686, DateTimeKind.Local).AddTicks(8381),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 16, 10, 58, 40, 232, DateTimeKind.Local).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0ec94c5d-a126-4a12-bdd7-78ae3b02b632");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f83a93e0-4f6e-4642-9f1c-e3607858c9ce", "AQAAAAEAACcQAAAAEPXtThz5OTEruaj5/Ih2F3Y2I6yR/SaxpNcPz9vyb/RjEo9xtT4un5zDdcVxBQiCCA==" });
        }
    }
}
