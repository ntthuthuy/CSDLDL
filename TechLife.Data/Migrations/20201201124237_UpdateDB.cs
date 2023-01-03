using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DichVuPhong");

            migrationBuilder.DropColumn(
                name: "LinhVucKinhDoanhId",
                table: "DichVu");

            migrationBuilder.CreateTable(
                name: "DichVuHoSo",
                columns: table => new
                {
                    DichVuId = table.Column<int>(nullable: false),
                    HoSoId = table.Column<int>(nullable: false),
                    QuyMo = table.Column<int>(nullable: false),
                    DonViTinhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVuHoSo", x => new { x.HoSoId, x.DichVuId });
                });

            migrationBuilder.CreateTable(
                name: "DonViTinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViTinh", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "47dc2a11-0dde-404e-9152-11de924eeb5c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0df86eda-ff7c-49dc-ad53-6aa3dff4e568", "AQAAAAEAACcQAAAAEGfXaii8dPtdCRRASYUwPuDy+PxsS1v7U3Wm1bOK0HT5DI+GpVOyedVb2aVH3lZn6A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DichVuHoSo");

            migrationBuilder.DropTable(
                name: "DonViTinh");

            migrationBuilder.AddColumn<int>(
                name: "LinhVucKinhDoanhId",
                table: "DichVu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DichVuPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVuPhong", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "52a472eb-858f-4e3d-a2a0-063c7332cd85");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d11537f3-f6ac-44d9-a4bc-ba3f43fa2e3d", "AQAAAAEAACcQAAAAEEJ89jVc+NPfcCU/zT2Lhy8iStS70rr/FEpxHdGUy0wVosbp3juz+hOcHbMBWVSFdw==" });
        }
    }
}
