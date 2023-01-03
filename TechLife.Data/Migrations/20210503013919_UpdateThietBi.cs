using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateThietBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayGoCaiDat",
                table: "ThietBi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 198, DateTimeKind.Local).AddTicks(1823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 930, DateTimeKind.Local).AddTicks(4519));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 406, DateTimeKind.Local).AddTicks(9181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 202, DateTimeKind.Local).AddTicks(811),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 932, DateTimeKind.Local).AddTicks(9693));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 390, DateTimeKind.Local).AddTicks(3697),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 41, 18, DateTimeKind.Local).AddTicks(7758));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fac08c2b-24a4-4b13-8290-7ab8238d36ff");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6441e28-4cd2-46d3-90fc-8c0f198a3cfa", "AQAAAAEAACcQAAAAEJZ61FB1H9Gxvj1hoxjSKkx61+iCn0owmIaQH7nW92vgOgBpMxQzbjCZj+UXO9ZxTw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 930, DateTimeKind.Local).AddTicks(4519),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 198, DateTimeKind.Local).AddTicks(1823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 406, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGoCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 932, DateTimeKind.Local).AddTicks(9693),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 202, DateTimeKind.Local).AddTicks(811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 41, 18, DateTimeKind.Local).AddTicks(7758),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 390, DateTimeKind.Local).AddTicks(3697));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6906ce61-0b78-4b0e-98ba-01aca0aa9121");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "76308f61-c2d1-4a0a-913e-5c74f3c00635", "AQAAAAEAACcQAAAAEKMQy+p/w+dGXh2XAsjKGCIKJaUlDvLssFD7y3sIaUBWXgNQ3b2GWY9RgYBExqPAIw==" });
        }
    }
}
