using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTinTucId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 711, DateTimeKind.Local).AddTicks(7279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 670, DateTimeKind.Local).AddTicks(524));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 958, DateTimeKind.Local).AddTicks(7700),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 879, DateTimeKind.Local).AddTicks(4445));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 965, DateTimeKind.Local).AddTicks(7556),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 892, DateTimeKind.Local).AddTicks(7379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 717, DateTimeKind.Local).AddTicks(9515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 672, DateTimeKind.Local).AddTicks(9225));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 935, DateTimeKind.Local).AddTicks(5099),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 814, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f875ea0f-d97a-4db2-9eca-9ecf5365b5d1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "93ffb23c-2901-4de4-a423-44cbe1fdf0bd", "AQAAAAEAACcQAAAAELyaTehgCrUDt3NlJvYyo/iCdzKk/iCZvZD+EXnuaLTkw+3aEYKIFvwKUGR45SGpqA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 670, DateTimeKind.Local).AddTicks(524),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 711, DateTimeKind.Local).AddTicks(7279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 879, DateTimeKind.Local).AddTicks(4445),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 958, DateTimeKind.Local).AddTicks(7700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 892, DateTimeKind.Local).AddTicks(7379),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 965, DateTimeKind.Local).AddTicks(7556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 672, DateTimeKind.Local).AddTicks(9225),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 717, DateTimeKind.Local).AddTicks(9515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 814, DateTimeKind.Local).AddTicks(7526),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 935, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "bf2fa079-e485-4a44-a4f8-fa3c4506809e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d82d1a92-7aeb-4db8-8bcf-97ae82d02b5b", "AQAAAAEAACcQAAAAEFKZFBcX6xeIdrWK3ObleQC6PBaDGsTnFJGJiX1lqoquhYbwHfEXaY//KVSxpNoYhw==" });
        }
    }
}
