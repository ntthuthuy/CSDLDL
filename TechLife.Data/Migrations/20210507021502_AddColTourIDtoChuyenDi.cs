using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddColTourIDtoChuyenDi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 363, DateTimeKind.Local).AddTicks(6685),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 675, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 542, DateTimeKind.Local).AddTicks(5166),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 750, DateTimeKind.Local).AddTicks(7563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 546, DateTimeKind.Local).AddTicks(9611),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 753, DateTimeKind.Local).AddTicks(1182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 370, DateTimeKind.Local).AddTicks(3076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 678, DateTimeKind.Local).AddTicks(8545));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 514, DateTimeKind.Local).AddTicks(2181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 741, DateTimeKind.Local).AddTicks(3417));

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "ChuyenDi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e59596ba-0561-46b1-981c-995f2c80a149");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62e0fc84-0160-4c1b-b960-d20514936eb0", "AQAAAAEAACcQAAAAEHQRHmJ+YCWuOlWgVDzBv9U8QLTCX8EOCE5YnHVwGybc+mZ5v1lTSONO6CDCpAJq+A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TourId",
                table: "ChuyenDi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 675, DateTimeKind.Local).AddTicks(7170),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 363, DateTimeKind.Local).AddTicks(6685));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 750, DateTimeKind.Local).AddTicks(7563),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 542, DateTimeKind.Local).AddTicks(5166));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 753, DateTimeKind.Local).AddTicks(1182),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 546, DateTimeKind.Local).AddTicks(9611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 678, DateTimeKind.Local).AddTicks(8545),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 370, DateTimeKind.Local).AddTicks(3076));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 741, DateTimeKind.Local).AddTicks(3417),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 7, 9, 15, 1, 514, DateTimeKind.Local).AddTicks(2181));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "edf73519-4cb9-4510-9df3-9d1f445e3d33");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "454db4b4-1b40-4b84-b142-3a2a93ad9e37", "AQAAAAEAACcQAAAAECUc2DcQjiAnRhXdNNh/zCFIdhUNYgDHhYDLwnefRU+ITPcyVDhZBQG4aa+lT/Y/uA==" });
        }
    }
}
