using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateTBLHuongDanVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HuongDanVien",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(nullable: false),
                    GioiTinh = table.Column<bool>(nullable: false),
                    CMND = table.Column<string>(nullable: true),
                    NgayCapCMND = table.Column<DateTime>(nullable: false),
                    NoiCapCMND = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    HoKhau = table.Column<string>(nullable: true),
                    SoTheHDV = table.Column<string>(nullable: true),
                    LoaiTheId = table.Column<int>(nullable: false),
                    NgayCapThe = table.Column<DateTime>(nullable: false),
                    NgayHetHan = table.Column<DateTime>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuongDanVien", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "56cb9b35-93c2-4295-a3e3-222ccf38311d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1ba61f4-5dc8-4d54-b2a1-59a118a27ee0", "AQAAAAEAACcQAAAAECu22U+lRnv1d1KLOn5568BZKVshtmV/fpdBzw/PG17S9WS1Pui+PLjrEi5KBgyZvw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuongDanVien");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "dad995bd-2cdd-4c89-a69d-a31cae714975");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b7c528d3-916b-4a4e-b553-cedfb929ec3e", "AQAAAAEAACcQAAAAEDmO8SotyWnaUl3Nihc+N/moNmgL/46w7d4vdGO6QC87hLLx7y1fC6MlYZBx2dHhXg==" });
        }
    }
}
