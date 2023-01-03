using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class addQuaTrinhHoatDong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuaTrinhHoatDong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HDVId = table.Column<int>(nullable: false),
                    HoatDong = table.Column<string>(nullable: true),
                    ThoiGian = table.Column<string>(nullable: true),
                    KetQua = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuaTrinhHoatDong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuaTrinhHoatDong_HuongDanVien_HDVId",
                        column: x => x.HDVId,
                        principalTable: "HuongDanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a46e40db-fa56-4789-98f1-367f83bbc5bb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65e75d93-7028-41ae-ad84-7f9dc8175ab4", "AQAAAAEAACcQAAAAEEQVViQfUUnjK5kCDN5fkLwVfbHkfYRaFCHIYYRyVvDze24fGX7/UijRg4s9uYboLw==" });

            migrationBuilder.CreateIndex(
                name: "IX_QuaTrinhHoatDong_HDVId",
                table: "QuaTrinhHoatDong",
                column: "HDVId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuaTrinhHoatDong");

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
    }
}
