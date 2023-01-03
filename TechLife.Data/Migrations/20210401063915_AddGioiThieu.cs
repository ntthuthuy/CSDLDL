using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddGioiThieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 271, DateTimeKind.Local).AddTicks(1891),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 920, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 277, DateTimeKind.Local).AddTicks(3544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 924, DateTimeKind.Local).AddTicks(2533));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 462, DateTimeKind.Local).AddTicks(7056),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 18, 28, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.AddColumn<string>(
                name: "GioiThieu",
                table: "HoSo",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "095d0aee-9ced-4d33-9dec-43f378a00ee4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90dd09c6-736f-4dcf-b5bc-8b956679f06d", "AQAAAAEAACcQAAAAEMOYrHrsjbZ7az5/aNvdf+at3hHtqALUojVI9DEPA75ypymIzeEr6KMB3aEtfmogNA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioiThieu",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 920, DateTimeKind.Local).AddTicks(2410),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 271, DateTimeKind.Local).AddTicks(1891));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 924, DateTimeKind.Local).AddTicks(2533),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 277, DateTimeKind.Local).AddTicks(3544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 18, 28, DateTimeKind.Local).AddTicks(7780),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 1, 13, 39, 13, 462, DateTimeKind.Local).AddTicks(7056));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1fd34e81-bf1d-470d-a8eb-1b2289c3ecfd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "480c9bd4-3e12-44b1-ab72-b29609aac695", "AQAAAAEAACcQAAAAENfKJDHEoF9Vz98AxdFr6SjeIoo53DWTMdOQnHwrMFiSC+9/xc7xlKHK8hGH1A684Q==" });
        }
    }
}
