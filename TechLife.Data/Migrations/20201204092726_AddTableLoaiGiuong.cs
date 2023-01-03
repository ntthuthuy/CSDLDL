using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTableLoaiGiuong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6fdfc1e6-03b2-4392-b5a8-b8221e57eb8c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "adca0633-e62c-43de-8784-61f7a61a076d", "AQAAAAEAACcQAAAAEHa2T9IWO/ffhj0CJbPN4w5xXPAxHfuN7Fl16oGQIM2xhF7aoHgxFQkHjMx5lwQAxA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f97ba304-af2c-440c-a4db-245df3c8ace4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "04d84938-a90e-45a3-a002-a269148b6f99", "AQAAAAEAACcQAAAAEPmPitSxMe8+hlElm1i6eI+WK99SkGUWFZjxbjfLcPgnVndc6zuTydLLXotvrJMM8Q==" });
        }
    }
}
