using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 889, DateTimeKind.Local).AddTicks(2008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 552, DateTimeKind.Local).AddTicks(5699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 892, DateTimeKind.Local).AddTicks(9015),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 556, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 997, DateTimeKind.Local).AddTicks(8990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 673, DateTimeKind.Local).AddTicks(9905));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 552, DateTimeKind.Local).AddTicks(5699),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 889, DateTimeKind.Local).AddTicks(2008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 556, DateTimeKind.Local).AddTicks(6532),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 892, DateTimeKind.Local).AddTicks(9015));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 16, 9, 8, 673, DateTimeKind.Local).AddTicks(9905),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 24, 22, 24, 55, 997, DateTimeKind.Local).AddTicks(8990));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e84b300-236b-4a86-b201-a4fe8de3c4c8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c906e1f6-787f-4244-ba41-243aac9f5b16", "AQAAAAEAACcQAAAAEI1h+bpgFs6emWMQNtLsGjNm7WJ2dGn8vSCrbJQ+4+jw4LsZhBV1va9+3UT+/vjQ5A==" });
        }
    }
}
