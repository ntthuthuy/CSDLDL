using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class HuongDanVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 426, DateTimeKind.Local).AddTicks(5213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 312, DateTimeKind.Local).AddTicks(4107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 439, DateTimeKind.Local).AddTicks(4565),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 315, DateTimeKind.Local).AddTicks(9324));

            migrationBuilder.AddColumn<int>(
                name: "CongTyLuHanhId",
                table: "HuongDanVien",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoiCapThe",
                table: "HuongDanVien",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HuongDanVienLoaiHinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HuongDanVienId = table.Column<int>(nullable: false),
                    LoaiHinhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuongDanVienLoaiHinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HuongDanVienNgonNgu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HuongDanVienId = table.Column<int>(nullable: false),
                    NgonNguId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuongDanVienNgonNgu", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "655b74a8-0a12-4380-973d-fec2b5e1ff4c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5c17b35-68e6-437c-911c-8f8d44a6d292", "AQAAAAEAACcQAAAAEKzaeUYxfdblTtFf5mnvkKrMzgRCniJHzsyIKM+hVKjDxGUa7zurBJIPXe2d13YMkQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuongDanVienLoaiHinh");

            migrationBuilder.DropTable(
                name: "HuongDanVienNgonNgu");

            migrationBuilder.DropColumn(
                name: "CongTyLuHanhId",
                table: "HuongDanVien");

            migrationBuilder.DropColumn(
                name: "NoiCapThe",
                table: "HuongDanVien");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 312, DateTimeKind.Local).AddTicks(4107),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 426, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 315, DateTimeKind.Local).AddTicks(9324),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 12, 14, 25, 36, 439, DateTimeKind.Local).AddTicks(4565));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "55661c9a-8438-497a-aca1-702856fbe3d1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae031b3f-d68c-4b59-a801-fe1f4fc4c9c2", "AQAAAAEAACcQAAAAEErtVkSYUClTE0RIifciFipMxbgVE9SVPttp97WrE/h+X717frbVzVbhwjpsxlql6Q==" });
        }
    }
}
