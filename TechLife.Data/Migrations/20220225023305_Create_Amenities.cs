using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class Create_Amenities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 921, DateTimeKind.Local).AddTicks(1934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 681, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 982, DateTimeKind.Local).AddTicks(9391),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 750, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 984, DateTimeKind.Local).AddTicks(4298),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 752, DateTimeKind.Local).AddTicks(5624));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 924, DateTimeKind.Local).AddTicks(6953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 683, DateTimeKind.Local).AddTicks(5807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 974, DateTimeKind.Local).AddTicks(6573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 741, DateTimeKind.Local).AddTicks(1785));

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false),
                    TypeOfBusinessId = table.Column<int>(nullable: false),
                    AmenityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3d43ad0a-8a1f-4686-af33-d268401c0592");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "792a8bbb-6ff9-4942-8aa6-6d86674160f9", "AQAAAAEAACcQAAAAEOCcEJxAmRZLeNmBGcfck/ISHDYzTWcNklGc8ZGQDj01z7zmVqSGqzJL8Pjl3qXLgA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 681, DateTimeKind.Local).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 921, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 750, DateTimeKind.Local).AddTicks(6806),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 982, DateTimeKind.Local).AddTicks(9391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 752, DateTimeKind.Local).AddTicks(5624),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 984, DateTimeKind.Local).AddTicks(4298));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 683, DateTimeKind.Local).AddTicks(5807),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 924, DateTimeKind.Local).AddTicks(6953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 10, 2, 11, 741, DateTimeKind.Local).AddTicks(1785),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 2, 25, 9, 33, 3, 974, DateTimeKind.Local).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5e8ef272-1045-486c-a9ed-f384375d3e66");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55d62e73-db5a-41b5-baa1-9dedd0d2c719", "AQAAAAEAACcQAAAAEOJBOWMxG/c0o/lJqOFhbKPPKYezGT4ESEB3S9pEMm9ja3X+KXU5FuhyyX8xEWs0Wg==" });
        }
    }
}
