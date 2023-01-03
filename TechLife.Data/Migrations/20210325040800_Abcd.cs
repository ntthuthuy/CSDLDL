using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Abcd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 362, DateTimeKind.Local).AddTicks(1157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 241, DateTimeKind.Local).AddTicks(7421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 365, DateTimeKind.Local).AddTicks(3780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 249, DateTimeKind.Local).AddTicks(5882));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 439, DateTimeKind.Local).AddTicks(9798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 500, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9b6017e2-454e-416f-838e-d1b2e7311648");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "502d9bbb-be79-4451-b4df-d937330500f4", "AQAAAAEAACcQAAAAEG1S4rMqx86OTsLXSzBSYUreJz9skN+Olff8IOTPj++YNgl57Y9j64Uf/iP7JTfY5w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 241, DateTimeKind.Local).AddTicks(7421),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 362, DateTimeKind.Local).AddTicks(1157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 249, DateTimeKind.Local).AddTicks(5882),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 365, DateTimeKind.Local).AddTicks(3780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 10, 58, 23, 500, DateTimeKind.Local).AddTicks(875),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 25, 11, 7, 59, 439, DateTimeKind.Local).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "32bb4c3b-2b39-46db-bc9e-b286db2a7d5e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "20dc9554-d209-4058-bb8d-c1985f5c64e5", "AQAAAAEAACcQAAAAECisiomDuPhtgRP8fp6qrW5GFqFSKLmou3ip3hX7supszpl/J8REQVKoqAAe6OO2Jw==" });
        }
    }
}
