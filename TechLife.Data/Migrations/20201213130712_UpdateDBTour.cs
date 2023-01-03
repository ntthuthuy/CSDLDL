using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDBTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDiId",
                table: "HanhTrinh");

            migrationBuilder.AddColumn<int>(
                name: "HoSoId",
                table: "HanhTrinh",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "07dd130e-2438-4cd0-a9d7-f21c049af22a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bf437311-861c-4032-b2ad-d47d85c587b7", "AQAAAAEAACcQAAAAEPY4tvCRxW9hg5HNEjwVfnv+fa7SP0HL9VRvPTCHDEu5lstk6twtttJnQgEwcWo2Zg==" });

            migrationBuilder.CreateIndex(
                name: "IX_HanhTrinh_HoSoId",
                table: "HanhTrinh",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_HanhTrinh_TourId",
                table: "HanhTrinh",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_Tours_TourId",
                table: "HanhTrinh",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_Tours_TourId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_TourId",
                table: "HanhTrinh");

            migrationBuilder.DropColumn(
                name: "HoSoId",
                table: "HanhTrinh");

            migrationBuilder.AddColumn<int>(
                name: "NoiDiId",
                table: "HanhTrinh",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
