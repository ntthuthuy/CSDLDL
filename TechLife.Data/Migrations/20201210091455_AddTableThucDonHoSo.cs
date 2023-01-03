using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTableThucDonHoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThucDonHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HosoId = table.Column<int>(nullable: false),
                    TenThucDon = table.Column<string>(nullable: true),
                    DonGia = table.Column<decimal>(nullable: false),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThucDonHoSo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "81f82e87-35d0-4a07-bd2c-e1ff90836ca4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e35f71e6-6801-42ff-bb5f-3d46c06ffc43", "AQAAAAEAACcQAAAAEBvagcKbPvWuiQUIj98w47R2ixP3pKH5WDTHHE9galkQySNev94PING87dXgVZ9Jug==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThucDonHoSo");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "82413104-51bc-4831-b881-853ffb251953");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea080662-3298-451f-a1b8-844775c1a0f8", "AQAAAAEAACcQAAAAELcYjY+KHoCGBGs8stVUHq3sC+rI7P1nqPd7exc3ewS+SE/RzN438ArqHSm3EPxtpw==" });
        }
    }
}
