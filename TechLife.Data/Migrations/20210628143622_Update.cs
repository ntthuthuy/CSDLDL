using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 906, DateTimeKind.Local).AddTicks(2187),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 24, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 47, DateTimeKind.Local).AddTicks(5732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 111, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 50, DateTimeKind.Local).AddTicks(9886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 112, DateTimeKind.Local).AddTicks(9048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 911, DateTimeKind.Local).AddTicks(6079),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 27, DateTimeKind.Local).AddTicks(5058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 30, DateTimeKind.Local).AddTicks(1051),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 100, DateTimeKind.Local).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "253d8975-73de-4a02-8c0c-4107d8edff71");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d696501-97b8-4214-b097-eeda807885e3", "AQAAAAEAACcQAAAAEEMi1tLh5ci6+vX8Zao+GA+h60KXES/xgKHE3zdCDy1o9j1UCPpvW7OFD3MT+bo88A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 24, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 906, DateTimeKind.Local).AddTicks(2187));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 111, DateTimeKind.Local).AddTicks(188),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 47, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 112, DateTimeKind.Local).AddTicks(9048),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 50, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 27, DateTimeKind.Local).AddTicks(5058),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 20, 911, DateTimeKind.Local).AddTicks(6079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 100, DateTimeKind.Local).AddTicks(4061),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 28, 21, 36, 21, 30, DateTimeKind.Local).AddTicks(1051));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "12460d6f-e29b-4310-b190-337b687b4cbc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "603c17f1-ff5a-4275-870b-a952697d2545", "AQAAAAEAACcQAAAAEGUkdaTE/iwXM2sIA5i6Stj+usX+81yo8D9Z8SD7xf3v/mAR1+4CxNBCArgHcD2WRg==" });
        }
    }
}
