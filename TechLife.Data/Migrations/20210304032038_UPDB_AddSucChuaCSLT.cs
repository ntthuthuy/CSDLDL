using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UPDB_AddSucChuaCSLT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 804, DateTimeKind.Local).AddTicks(4390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 863, DateTimeKind.Local).AddTicks(2728));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 807, DateTimeKind.Local).AddTicks(3215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 867, DateTimeKind.Local).AddTicks(5124));

            migrationBuilder.AddColumn<int>(
                name: "SoNguoiLon",
                table: "LoaiPhongHoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoTreEm",
                table: "LoaiPhongHoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ed831f98-1cad-40f6-8899-447a70268ac6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba5e4499-2981-40f1-bf75-41fcdbdd5c38", "AQAAAAEAACcQAAAAEJedIvsKHfQaklV1OsddICsPK082vJ3mnYKJLxN25hrq0uVL1oTlvZpkuPJSuM8uWQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoNguoiLon",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropColumn(
                name: "SoTreEm",
                table: "LoaiPhongHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 863, DateTimeKind.Local).AddTicks(2728),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 804, DateTimeKind.Local).AddTicks(4390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 867, DateTimeKind.Local).AddTicks(5124),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 807, DateTimeKind.Local).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c7acd109-c317-488b-959f-a5b17597f659");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3268efc-10e2-4958-89f0-dcfb014a0277", "AQAAAAEAACcQAAAAELgBISKW7Jt7BwS3nTLGk31WtIbuZB9OQsoDcib1yZN9j/Ke35Cdq5kMEwYNBFvxSQ==" });
        }
    }
}
