using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB_ThemCotSoLuong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 863, DateTimeKind.Local).AddTicks(2728),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 642, DateTimeKind.Local).AddTicks(5759));

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "TienNghiHoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 867, DateTimeKind.Local).AddTicks(5124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 647, DateTimeKind.Local).AddTicks(9886));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "TienNghiHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 642, DateTimeKind.Local).AddTicks(5759),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 863, DateTimeKind.Local).AddTicks(2728));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 647, DateTimeKind.Local).AddTicks(9886),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 14, 15, 867, DateTimeKind.Local).AddTicks(5124));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "094f6b62-0dad-4e81-872b-37d60b5527b8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a72aa3b7-5382-4c63-8ed3-70950e7141cd", "AQAAAAEAACcQAAAAEEvVZ6nU13ikLguIGTvgfMnaCUWY7umkZg0kAJB10K+JI3xmhr8C2fHewJc0h/z8zQ==" });
        }
    }
}
