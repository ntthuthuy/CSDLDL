using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UDDB071220_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPhuPhi",
                table: "TienNghiHoSo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuDung",
                table: "TienNghiHoSo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4e6a8f75-d3e5-49c0-83c1-6918e2eeee1a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e577efd-d9f8-428e-997f-313deccc2968", "AQAAAAEAACcQAAAAEFYgvIMKEyMlAY7oZKoQ+FOGZm7aQ/Uk9L2g7x1YMDaTSwgMydyWYzViLnaeKQAMUw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuDung",
                table: "TienNghiHoSo");

            migrationBuilder.AlterColumn<int>(
                name: "IsPhuPhi",
                table: "TienNghiHoSo",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "abf85537-8ee3-4a3e-9395-ac4705085ec6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aadcaf9b-d246-4ae4-b007-f1281f06a35b", "AQAAAAEAACcQAAAAEESjBBDe2p3QEuGMsYyWEvCvEtn/D5kDcCjDy/kE0o7Phmc3KCBsAqHk4zcd33n4Bg==" });
        }
    }
}
