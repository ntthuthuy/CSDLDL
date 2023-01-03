using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class TongSoPhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 137, DateTimeKind.Local).AddTicks(2574),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 729, DateTimeKind.Local).AddTicks(2583));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 139, DateTimeKind.Local).AddTicks(5805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 736, DateTimeKind.Local).AddTicks(3762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 224, DateTimeKind.Local).AddTicks(9641),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 906, DateTimeKind.Local).AddTicks(899));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "49818eb7-5407-408b-90a5-2b36a3a3c1e6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3578df24-3b93-4b85-a28c-20c3b6871f28", "AQAAAAEAACcQAAAAEHNYehjDvw11d7i668fJ1AFci0eN5Q1YNlkGm2Qq18NuPoIpZY5hBkKye1dfexQ7vg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 729, DateTimeKind.Local).AddTicks(2583),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 137, DateTimeKind.Local).AddTicks(2574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 736, DateTimeKind.Local).AddTicks(3762),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 139, DateTimeKind.Local).AddTicks(5805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 5, 29, 906, DateTimeKind.Local).AddTicks(899),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 224, DateTimeKind.Local).AddTicks(9641));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "449c12d9-0951-4384-a3b7-1c9a671962fa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8fe51732-61a0-4710-a0da-514a8c3d7b45", "AQAAAAEAACcQAAAAEFmznt9gzcKOPBLtbgRA3KU1xUgPPffOCSfuQvb3uIyqB0AAPciIyjajSuMFyNfZbA==" });
        }
    }
}
