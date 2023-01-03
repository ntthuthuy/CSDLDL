using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddNhaHangLuuTru : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 444, DateTimeKind.Local).AddTicks(8103),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 146, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 448, DateTimeKind.Local).AddTicks(5503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 150, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 539, DateTimeKind.Local).AddTicks(8581),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 234, DateTimeKind.Local).AddTicks(6779));

            migrationBuilder.CreateTable(
                name: "QuyMoNhaHangLuuTru",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoId = table.Column<int>(nullable: false),
                    TenGoi = table.Column<string>(nullable: false),
                    DienTich = table.Column<int>(nullable: false, defaultValue: 0),
                    SoGhe = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDelete = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyMoNhaHangLuuTru", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1ee8bbae-58a5-4ddd-8fab-db5d36f765da");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c1998594-31f7-4087-9616-ef01f6c4e3b5", "AQAAAAEAACcQAAAAEOoDpprTpZp9a/phwQR8Ktt7cO5q29Jh7ioHNNbyvd10AevX2T1QJ00cIy7MioQn+A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuyMoNhaHangLuuTru");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 146, DateTimeKind.Local).AddTicks(2712),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 444, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 150, DateTimeKind.Local).AddTicks(7797),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 448, DateTimeKind.Local).AddTicks(5503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 12, 9, 55, 36, 234, DateTimeKind.Local).AddTicks(6779),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 13, 9, 24, 31, 539, DateTimeKind.Local).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "755b0255-9afd-43b6-9087-580bf5e314e9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1e141202-a935-463f-9082-ad3df6d03bc9", "AQAAAAEAACcQAAAAEB0KLxdHse1ISReiSGu3HUrCIY0z2W3M8jcTLuHig0KSvrYeky0vUyV55/HNwxwnxA==" });
        }
    }
}
