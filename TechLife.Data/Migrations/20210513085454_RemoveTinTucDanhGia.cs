using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class RemoveTinTucDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinTucDanhGia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 132, DateTimeKind.Local).AddTicks(3359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 804, DateTimeKind.Local).AddTicks(4271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 280, DateTimeKind.Local).AddTicks(6386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 861, DateTimeKind.Local).AddTicks(1483));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 288, DateTimeKind.Local).AddTicks(3791),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 862, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 136, DateTimeKind.Local).AddTicks(4688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 806, DateTimeKind.Local).AddTicks(3582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 256, DateTimeKind.Local).AddTicks(8954),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 851, DateTimeKind.Local).AddTicks(5813));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "da316b64-c32b-49ae-a146-6c68e2ab84c9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9360482-f1d1-45e6-a0bf-180c2cef0130", "AQAAAAEAACcQAAAAEAuY81fhe9UkAz7Dr7qLiYO/vs+P3jpz0Nxvc1yEVoyhmhw95isX++0WEoZ9SRZ3cw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 804, DateTimeKind.Local).AddTicks(4271),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 132, DateTimeKind.Local).AddTicks(3359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 861, DateTimeKind.Local).AddTicks(1483),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 280, DateTimeKind.Local).AddTicks(6386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 862, DateTimeKind.Local).AddTicks(5891),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 288, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 806, DateTimeKind.Local).AddTicks(3582),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 136, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 851, DateTimeKind.Local).AddTicks(5813),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 13, 15, 54, 52, 256, DateTimeKind.Local).AddTicks(8954));

            migrationBuilder.CreateTable(
                name: "TinTucDanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemDanhGia = table.Column<int>(type: "int", nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 5, 13, 15, 52, 11, 857, DateTimeKind.Local).AddTicks(6160)),
                    TinTucId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTucDanhGia", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3ad21bc2-5bd5-45aa-8c14-98742856c5d8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8240bb0-c9b8-49fe-8033-7f42a59d763c", "AQAAAAEAACcQAAAAEH8mt44XHp4ywaxMBDOcrPhGvME7gSQ46NFkjvr37UwOtEYWjxiIl2x10IHexqCglQ==" });
        }
    }
}
