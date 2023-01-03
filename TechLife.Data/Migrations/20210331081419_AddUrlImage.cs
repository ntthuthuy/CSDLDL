using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddUrlImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvataUrl",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 920, DateTimeKind.Local).AddTicks(2410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 889, DateTimeKind.Local).AddTicks(2008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 924, DateTimeKind.Local).AddTicks(2533),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 892, DateTimeKind.Local).AddTicks(9015));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 31, 15, 14, 18, 28, DateTimeKind.Local).AddTicks(7780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 997, DateTimeKind.Local).AddTicks(8990));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvataUrl",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 889, DateTimeKind.Local).AddTicks(2008),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 920, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 892, DateTimeKind.Local).AddTicks(9015),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 17, 924, DateTimeKind.Local).AddTicks(2533));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 997, DateTimeKind.Local).AddTicks(8990),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 31, 15, 14, 18, 28, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "caa0162d-10cc-4e71-98e3-2ec263bd60fa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b8ed19d-a4cc-4582-b8e5-22c54b039029", "AQAAAAEAACcQAAAAEE4jwEFJBLpnme/Gu1FfH0xl/qhi/IqIFFsZd2vxemEXd3EpDGZjYj0MhVeZ7W3qnA==" });
        }
    }
}
