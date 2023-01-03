using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDBNgayHetHan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 945, DateTimeKind.Local).AddTicks(4941),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 262, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 975, DateTimeKind.Local).AddTicks(4824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 266, DateTimeKind.Local).AddTicks(500));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHetHan",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 262, DateTimeKind.Local).AddTicks(5240),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 945, DateTimeKind.Local).AddTicks(4941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 2, 13, 53, 40, 266, DateTimeKind.Local).AddTicks(500),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 3, 10, 58, 13, 975, DateTimeKind.Local).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6fd02845-09fc-4c52-bb35-43c7cc1b9515");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "924fceb5-a30d-45e5-a3ff-a40fa8bc8380", "AQAAAAEAACcQAAAAEJHD5muWxT9FVIWZp0yMMGTguyL6bSD/14IbKSwOk8hbBgcU8ntMyQKC36b7K/TRpg==" });
        }
    }
}
