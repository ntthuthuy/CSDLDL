using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateBinhLuan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 270, DateTimeKind.Local).AddTicks(3859),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 658, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 272, DateTimeKind.Local).AddTicks(8230),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 663, DateTimeKind.Local).AddTicks(4540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 337, DateTimeKind.Local).AddTicks(6668),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 775, DateTimeKind.Local).AddTicks(3712));

            migrationBuilder.CreateTable(
                name: "BinhLuan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    NoiDung = table.Column<string>(nullable: false),
                    NgayBinhLuan = table.Column<DateTime>(nullable: false),
                    AvataUrl = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuan", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "bad5217b-4a8b-435d-863f-5093466f7c27");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82183aab-61a1-41ef-9aa6-5a8f41ded75e", "AQAAAAEAACcQAAAAEGoZ/LmaHtOC7YYisyl9HZiupIVqMYvySGQ3uNjiJzB7OpA23ATlJocfkpUgMenSKg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 658, DateTimeKind.Local).AddTicks(4603),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 270, DateTimeKind.Local).AddTicks(3859));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 663, DateTimeKind.Local).AddTicks(4540),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 272, DateTimeKind.Local).AddTicks(8230));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 775, DateTimeKind.Local).AddTicks(3712),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 28, 55, 337, DateTimeKind.Local).AddTicks(6668));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "80fd106f-0381-4fea-8db9-acd92e29b5fd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4276936e-0493-4323-823e-da0d4b981ddd", "AQAAAAEAACcQAAAAEIkJ2YtTnBSobUviiwO5Nwg8cEH4Xh9bOUu/OeuxFrGHg2WoZGyiz22CG7ibl3eEYw==" });
        }
    }
}
