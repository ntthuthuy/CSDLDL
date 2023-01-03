using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTBLDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 279, DateTimeKind.Local).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 642, DateTimeKind.Local).AddTicks(3604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 281, DateTimeKind.Local).AddTicks(9553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 646, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    SoSao = table.Column<int>(nullable: false, defaultValue: 0),
                    HoSoId = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f5c91f88-55f7-4678-9ca6-2751ab24b5f2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "437d3d7e-0938-4992-8d1c-188a5af7a884", "AQAAAAEAACcQAAAAEKrVzmhA0HydkTxATAA5QgiioohGJtk6LbVgGPcwsU9kDBhgjI1mc2OhP+CyzQv7CA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 642, DateTimeKind.Local).AddTicks(3604),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 279, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 646, DateTimeKind.Local).AddTicks(5451),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 281, DateTimeKind.Local).AddTicks(9553));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0e665816-3ee3-4d04-9d1b-ef8dbab6d17d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08524119-7e51-487d-b977-0c8398984573", "AQAAAAEAACcQAAAAEC956kHTfxsSAKSIoUWJAq5m1HLKPg75eZPG+lPv/7BJrm8vebFA7OxEG/6H86oyLw==" });
        }
    }
}
