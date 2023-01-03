using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateHanhTrinhChuyenDi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 607, DateTimeKind.Local).AddTicks(3379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 811, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 616, DateTimeKind.Local).AddTicks(999),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 815, DateTimeKind.Local).AddTicks(9815));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 839, DateTimeKind.Local).AddTicks(9334),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 895, DateTimeKind.Local).AddTicks(4758));

            migrationBuilder.CreateTable(
                name: "ChuyenDi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuyenDi = table.Column<string>(nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: true),
                    MaThietBi = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    Gia = table.Column<decimal>(nullable: false),
                    SoNgay = table.Column<int>(nullable: false),
                    SoNguoi = table.Column<int>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenDi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HanhTrinhChuyenDi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaDiemId = table.Column<int>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    Ngay = table.Column<int>(nullable: false),
                    Gio = table.Column<int>(nullable: false),
                    Phut = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HanhTrinhChuyenDi", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e8224302-6e36-4186-979a-4ad39ff11e10");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2c9d9819-2501-489a-9538-75a26b35b5d0", "AQAAAAEAACcQAAAAEH5TnAxDQlOhd0w8TDe1EIv3n5hvdpRMjSRW4U9tnqn9mvnZkDWftBS6vp8VqSfmFw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuyenDi");

            migrationBuilder.DropTable(
                name: "HanhTrinhChuyenDi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 811, DateTimeKind.Local).AddTicks(9376),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 607, DateTimeKind.Local).AddTicks(3379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 815, DateTimeKind.Local).AddTicks(9815),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 616, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 895, DateTimeKind.Local).AddTicks(4758),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 839, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "dfc55db9-2eb5-4426-908a-bf4456bd347b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df9077df-3fb8-4ed1-b670-ac9d40e33749", "AQAAAAEAACcQAAAAELR+F4lix4qymfUFtCS3bUsEVSEUZIWtXFuc0ADSbVc3iojD/hQdJ/h1vMQCHFRZag==" });
        }
    }
}
