using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddThietBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 930, DateTimeKind.Local).AddTicks(4519),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 644, DateTimeKind.Local).AddTicks(478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 932, DateTimeKind.Local).AddTicks(9693),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 647, DateTimeKind.Local).AddTicks(4949));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 8, 21, 41, 18, DateTimeKind.Local).AddTicks(7758),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 711, DateTimeKind.Local).AddTicks(7623));

            migrationBuilder.CreateTable(
                name: "ThietBi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThietBi = table.Column<string>(nullable: false),
                    NgayCaiDat = table.Column<DateTime>(nullable: false),
                    NgayGoCaiDat = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThietBi", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6906ce61-0b78-4b0e-98ba-01aca0aa9121");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "76308f61-c2d1-4a0a-913e-5c74f3c00635", "AQAAAAEAACcQAAAAEKMQy+p/w+dGXh2XAsjKGCIKJaUlDvLssFD7y3sIaUBWXgNQ3b2GWY9RgYBExqPAIw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThietBi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 644, DateTimeKind.Local).AddTicks(478),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 930, DateTimeKind.Local).AddTicks(4519));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 647, DateTimeKind.Local).AddTicks(4949),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 40, 932, DateTimeKind.Local).AddTicks(9693));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 711, DateTimeKind.Local).AddTicks(7623),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 3, 8, 21, 41, 18, DateTimeKind.Local).AddTicks(7758));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "aedb6f09-a4e9-4d35-9ee7-9471deb40662");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "caeb7ae0-5d5d-44d5-91ff-0e8d8f55dff4", "AQAAAAEAACcQAAAAEPdmV0GWckOTycUI39jhjSPJ8UeMY7ys5kdldNJIUdIZTR0XW0RG1pCq5lDo0i8n1Q==" });
        }
    }
}
