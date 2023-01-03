using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddChuyenMucTinTuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 172, DateTimeKind.Local).AddTicks(4430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 525, DateTimeKind.Local).AddTicks(4191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 176, DateTimeKind.Local).AddTicks(7294),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 531, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 298, DateTimeKind.Local).AddTicks(448),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 625, DateTimeKind.Local).AddTicks(5567));

            migrationBuilder.CreateTable(
                name: "TinTucChuyenMuc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinTucId = table.Column<int>(nullable: false),
                    ChuyenMucId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTucChuyenMuc", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1a6c60ce-04aa-450a-94ca-4506071ab4f6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3de2c25-c9d6-446b-91b1-ce4e8112e169", "AQAAAAEAACcQAAAAED2deI1R0jz2IE2QQlcTu6XT6FbCkYrFH7BVPL/nmgy+PLI/1ke2jHh6pLc7F1JkLw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinTucChuyenMuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 525, DateTimeKind.Local).AddTicks(4191),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 172, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 531, DateTimeKind.Local).AddTicks(821),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 176, DateTimeKind.Local).AddTicks(7294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 5, 15, 38, 17, 625, DateTimeKind.Local).AddTicks(5567),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 298, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e05922eb-89e2-42b1-b7c4-b642c007802a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd1946ec-e87d-4800-9493-f145ceb4f7d0", "AQAAAAEAACcQAAAAEPAYdyPCl4+QbqW2yViWBm3i+Scv9k7Cj7Ss5c/zV8xZm0aJaPA5qfOL+ha+Mpbb7w==" });
        }
    }
}
