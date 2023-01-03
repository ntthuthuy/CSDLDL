using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UDDB071220_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0f0c77bd-fe43-4280-8a84-7b092f5f9898");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c49714d0-9bca-47ad-b956-6edde551b1df", "AQAAAAEAACcQAAAAECu+7wGugHoMxVf8g5BmawWNchHVejTYo9l/jwVECERhUp1y43O1ev4letfxLstJ3g==" });
        }
    }
}
