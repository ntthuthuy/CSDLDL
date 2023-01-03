using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdtaeOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 474, DateTimeKind.Local).AddTicks(4205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 198, DateTimeKind.Local).AddTicks(1823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 751, DateTimeKind.Local).AddTicks(1703),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 406, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 481, DateTimeKind.Local).AddTicks(5219),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 202, DateTimeKind.Local).AddTicks(811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 719, DateTimeKind.Local).AddTicks(4374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 390, DateTimeKind.Local).AddTicks(3697));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThietBi = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LoaiDinhVu = table.Column<string>(nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 755, DateTimeKind.Local).AddTicks(5688)),
                    DichVuId = table.Column<int>(nullable: false),
                    NhaCungCapId = table.Column<int>(nullable: false),
                    Gia = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    SoLuong = table.Column<string>(nullable: true, defaultValue: "0"),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "270ee4c7-f72f-45da-9e2a-8e212f026ed3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc5f4043-b5fc-469a-a65b-5059be6746c4", "AQAAAAEAACcQAAAAEBqWUG22F5rjQB2RfOe1I8/Zr/jhq5T3uLwrVTcqYITwMKrdc0hoCIH2e/lYB4sqTw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 198, DateTimeKind.Local).AddTicks(1823),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 474, DateTimeKind.Local).AddTicks(4205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 406, DateTimeKind.Local).AddTicks(9181),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 751, DateTimeKind.Local).AddTicks(1703));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 202, DateTimeKind.Local).AddTicks(811),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 481, DateTimeKind.Local).AddTicks(5219));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 39, 17, 390, DateTimeKind.Local).AddTicks(3697),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 12, 52, 22, 719, DateTimeKind.Local).AddTicks(4374));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fac08c2b-24a4-4b13-8290-7ab8238d36ff");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6441e28-4cd2-46d3-90fc-8c0f198a3cfa", "AQAAAAEAACcQAAAAEJZ61FB1H9Gxvj1hoxjSKkx61+iCn0owmIaQH7nW92vgOgBpMxQzbjCZj+UXO9ZxTw==" });
        }
    }
}
