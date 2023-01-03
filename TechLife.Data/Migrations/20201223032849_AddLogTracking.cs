using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddLogTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: false),
                    StackTrace = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 61, DateTimeKind.Local).AddTicks(6267)),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 57, DateTimeKind.Local).AddTicks(2228)),
                    UserName = table.Column<string>(nullable: false),
                    Action = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0d01ae23-5ee7-4324-a567-f038ca95df57");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ad5e712-188a-47d0-8b6e-d3f4cd084a9c", "AQAAAAEAACcQAAAAEIR5rgfD/x8EUpsdLaDOdx10eqJasB77ZDbd8vB3DWD2Sf/xcxUVkEAjsnLhJUg49A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Trackings");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d0e24ca5-9ca1-4883-b113-8d826278c40d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e4ec6db-96ba-4bbc-b772-c7f787ed9c02", "AQAAAAEAACcQAAAAEPKXNFLY/Br54zpzLWzZoBPmTIY2dExf4yjTTtog+r5E6Rco4gaJUAypSnHFRdEfGA==" });
        }
    }
}
