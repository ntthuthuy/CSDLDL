using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Alter_HOSO250222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 916, DateTimeKind.Local).AddTicks(1918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 921, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 967, DateTimeKind.Local).AddTicks(8727),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 982, DateTimeKind.Local).AddTicks(9391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 969, DateTimeKind.Local).AddTicks(1760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 984, DateTimeKind.Local).AddTicks(4298));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 918, DateTimeKind.Local).AddTicks(1357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 924, DateTimeKind.Local).AddTicks(6953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 960, DateTimeKind.Local).AddTicks(3457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 974, DateTimeKind.Local).AddTicks(6573));

            migrationBuilder.AddColumn<decimal>(
                name: "GiaThamKhao",
                table: "HoSo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7b38df00-575c-4624-90c9-8b4f6c8528ac");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a6a87a05-d68e-46fa-a078-2a64e04af7a3", "AQAAAAEAACcQAAAAEF1V2XQW3oBQZLqf6tlbqpM7EJsAaCf3MwpjlDz/cXRJTTfUEzeB/5rURBLwcp0uug==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaThamKhao",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 921, DateTimeKind.Local).AddTicks(1934),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 916, DateTimeKind.Local).AddTicks(1918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 982, DateTimeKind.Local).AddTicks(9391),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 967, DateTimeKind.Local).AddTicks(8727));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 984, DateTimeKind.Local).AddTicks(4298),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 969, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 924, DateTimeKind.Local).AddTicks(6953),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 918, DateTimeKind.Local).AddTicks(1357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 974, DateTimeKind.Local).AddTicks(6573),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 14, 50, 17, 960, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3d43ad0a-8a1f-4686-af33-d268401c0592");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "792a8bbb-6ff9-4942-8aa6-6d86674160f9", "AQAAAAEAACcQAAAAEOCcEJxAmRZLeNmBGcfck/ISHDYzTWcNklGc8ZGQDj01z7zmVqSGqzJL8Pjl3qXLgA==" });
        }
    }
}
