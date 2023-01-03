using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Update_Loaiphong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 927, DateTimeKind.Local).AddTicks(1795),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 987, DateTimeKind.Local).AddTicks(1887),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 988, DateTimeKind.Local).AddTicks(8920),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 929, DateTimeKind.Local).AddTicks(1804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672));

            migrationBuilder.AddColumn<int>(
                name: "LuuTruId",
                table: "LoaiPhong",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 978, DateTimeKind.Local).AddTicks(2497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3c3fc14b-6705-4396-8790-faf776895003");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7bb9c67b-a327-454b-904e-302044cd6dae", "AQAAAAEAACcQAAAAEEHjS6TjGdtrox0PJ9ba3WpWk/vCyAj5MQgpOGcNoRUkWI0KAhI2VadFDqMtoasXTQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LuuTruId",
                table: "LoaiPhong");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 596, DateTimeKind.Local).AddTicks(8900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 927, DateTimeKind.Local).AddTicks(1795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 709, DateTimeKind.Local).AddTicks(4725),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 987, DateTimeKind.Local).AddTicks(1887));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 713, DateTimeKind.Local).AddTicks(2900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 988, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 599, DateTimeKind.Local).AddTicks(6672),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 929, DateTimeKind.Local).AddTicks(1804));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 10, 15, 45, 693, DateTimeKind.Local).AddTicks(4189),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 11, 24, 10, 54, 45, 978, DateTimeKind.Local).AddTicks(2497));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e244955-cf25-4dd4-9b40-e0e0cf65ac3f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64c54980-1fa7-46c9-944e-b1cf44d08676", "AQAAAAEAACcQAAAAEJpah1bB1YWEtM91vCXm8U4q3IvuP9P/CoPSVvO3Mn9S1wgVijKzp3iFKDqnuwY/2A==" });
        }
    }
}
