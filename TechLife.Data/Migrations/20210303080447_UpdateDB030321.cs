using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB030321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 642, DateTimeKind.Local).AddTicks(5759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 945, DateTimeKind.Local).AddTicks(4941));

            migrationBuilder.AddColumn<int>(
                name: "DonViTinhId",
                table: "TienNghi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LinhVucId",
                table: "TienNghi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 647, DateTimeKind.Local).AddTicks(9886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 975, DateTimeKind.Local).AddTicks(4824));

            migrationBuilder.AddColumn<int>(
                name: "CSLTId",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNhaHangTrongCSLT",
                table: "HoSo",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViTinhId",
                table: "TienNghi");

            migrationBuilder.DropColumn(
                name: "LinhVucId",
                table: "TienNghi");

            migrationBuilder.DropColumn(
                name: "CSLTId",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "IsNhaHangTrongCSLT",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 945, DateTimeKind.Local).AddTicks(4941),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 642, DateTimeKind.Local).AddTicks(5759));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 975, DateTimeKind.Local).AddTicks(4824),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 15, 4, 45, 647, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "82ce17df-12a8-4a2f-bdac-1bbd16031427");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47104d83-ed3b-4c0f-a1c4-a9d5d9e16a67", "AQAAAAEAACcQAAAAEFe6sgU9VfhB4hmKfoyHH0bF4X1DQ7auvqsm7UT4yY8ZsUHi4yJbuzOzc44hLOgOzg==" });
        }
    }
}
