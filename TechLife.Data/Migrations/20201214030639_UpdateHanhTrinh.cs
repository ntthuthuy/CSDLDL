using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateHanhTrinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropColumn(
                name: "HoSoId",
                table: "HanhTrinh");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3b03c730-becc-4ca2-9d03-b1d5a9a25f56");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "328af4a3-af6a-491d-8697-20f9ebb4befa", "AQAAAAEAACcQAAAAECqL1J+q/Hk5bjUPhQ4oLSf6QH+mUIiffpgwE7AkUPKnnlu/ovNpnZeiTs8i2IwkpQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_HanhTrinh_NoiDenId",
                table: "HanhTrinh",
                column: "NoiDenId");

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_HoSo_NoiDenId",
                table: "HanhTrinh",
                column: "NoiDenId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_HoSo_NoiDenId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_NoiDenId",
                table: "HanhTrinh");

            migrationBuilder.AddColumn<int>(
                name: "HoSoId",
                table: "HanhTrinh",
                type: "int",
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

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
