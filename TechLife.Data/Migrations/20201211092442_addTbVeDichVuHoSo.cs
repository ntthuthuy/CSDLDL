using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class addTbVeDichVuHoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeDichVuHoSo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HosoId = table.Column<int>(nullable: false),
                    TenVe = table.Column<string>(nullable: true),
                    GiaVe = table.Column<decimal>(nullable: false),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeDichVuHoSo", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeDichVuHoSo");

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
    }
}
