using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTBLDanhMuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 642, DateTimeKind.Local).AddTicks(3604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 808, DateTimeKind.Local).AddTicks(8966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 646, DateTimeKind.Local).AddTicks(5451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 811, DateTimeKind.Local).AddTicks(175));

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    LoaiId = table.Column<int>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 808, DateTimeKind.Local).AddTicks(8966),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 642, DateTimeKind.Local).AddTicks(3604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 811, DateTimeKind.Local).AddTicks(175),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 12, 5, 31, 646, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4f96dc7f-82bb-434a-9f31-933ecfdfe7a1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4f566c78-b3f1-4a47-abb9-03a1d17fefc6", "AQAAAAEAACcQAAAAENz+crWfuWcwmZ5LzIw51ejoJCoRuPcwqyKW6Yf3Qce/zb9u7P0AiLIMkk5uBOtQ6A==" });
        }
    }
}
