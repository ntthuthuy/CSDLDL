using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateTinTucDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 670, DateTimeKind.Local).AddTicks(524),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 363, DateTimeKind.Local).AddTicks(6685));

            migrationBuilder.AddColumn<int>(
                name: "LuotXem",
                table: "TinTuc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 879, DateTimeKind.Local).AddTicks(4445),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 542, DateTimeKind.Local).AddTicks(5166));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 892, DateTimeKind.Local).AddTicks(7379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 546, DateTimeKind.Local).AddTicks(9611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 672, DateTimeKind.Local).AddTicks(9225),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 370, DateTimeKind.Local).AddTicks(3076));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 814, DateTimeKind.Local).AddTicks(7526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 514, DateTimeKind.Local).AddTicks(2181));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LuotXem",
                table: "TinTuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 363, DateTimeKind.Local).AddTicks(6685),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 670, DateTimeKind.Local).AddTicks(524));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 542, DateTimeKind.Local).AddTicks(5166),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 879, DateTimeKind.Local).AddTicks(4445));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 546, DateTimeKind.Local).AddTicks(9611),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 892, DateTimeKind.Local).AddTicks(7379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 370, DateTimeKind.Local).AddTicks(3076),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 672, DateTimeKind.Local).AddTicks(9225));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 514, DateTimeKind.Local).AddTicks(2181),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 7, 39, 814, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e59596ba-0561-46b1-981c-995f2c80a149");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62e0fc84-0160-4c1b-b960-d20514936eb0", "AQAAAAEAACcQAAAAEHQRHmJ+YCWuOlWgVDzBv9U8QLTCX8EOCE5YnHVwGybc+mZ5v1lTSONO6CDCpAJq+A==" });
        }
    }
}
