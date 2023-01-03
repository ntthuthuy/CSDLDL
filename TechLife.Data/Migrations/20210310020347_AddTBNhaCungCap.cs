using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTBNhaCungCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 730, DateTimeKind.Local).AddTicks(8569),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 942, DateTimeKind.Local).AddTicks(2547));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 735, DateTimeKind.Local).AddTicks(2374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 946, DateTimeKind.Local).AddTicks(6814));

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    MaSoDoanhNghiep = table.Column<string>(nullable: true),
                    NgayDangKy = table.Column<DateTime>(nullable: false),
                    TenNguoiDaiDien = table.Column<string>(nullable: true),
                    ChucVu = table.Column<string>(nullable: true),
                    SDTDoanhNghiep = table.Column<string>(nullable: true),
                    SDTNguoiDaiDien = table.Column<string>(nullable: true),
                    EmailNguoiDaiDien = table.Column<string>(nullable: true),
                    EmailDoanhNghiep = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "619efd92-daaf-4743-a8e8-00637e178f28");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f54bc29-80a4-48a2-9e26-df64971fbcea", "AQAAAAEAACcQAAAAEHUuzK0rKaWEpc4NBdpJ5Xuub33/ZpKNss7C1aZBZw/mdMi5T7Hw/mLMb0nt4TNPwA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 942, DateTimeKind.Local).AddTicks(2547),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 730, DateTimeKind.Local).AddTicks(8569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 946, DateTimeKind.Local).AddTicks(6814),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 735, DateTimeKind.Local).AddTicks(2374));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "77a1f9bb-d6cd-4b0e-b797-1705a7fefb69");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ff31325-82b2-4e46-a18a-7c90f540ced7", "AQAAAAEAACcQAAAAEK6r7vlqQvcf5+xyZyVx7G3lFdHeoj+MEdF5AZQLGrdL1jASQ1ScQmwRt1m/VfMu8Q==" });
        }
    }
}
