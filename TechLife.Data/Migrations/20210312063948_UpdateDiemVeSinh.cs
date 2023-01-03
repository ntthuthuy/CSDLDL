using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDiemVeSinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 312, DateTimeKind.Local).AddTicks(4107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 749, DateTimeKind.Local).AddTicks(9853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 315, DateTimeKind.Local).AddTicks(9324),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 751, DateTimeKind.Local).AddTicks(6924));

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "DiemVeSinh",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "55661c9a-8438-497a-aca1-702856fbe3d1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae031b3f-d68c-4b59-a801-fe1f4fc4c9c2", "AQAAAAEAACcQAAAAEErtVkSYUClTE0RIifciFipMxbgVE9SVPttp97WrE/h+X717frbVzVbhwjpsxlql6Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ten",
                table: "DiemVeSinh");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 749, DateTimeKind.Local).AddTicks(9853),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 312, DateTimeKind.Local).AddTicks(4107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 751, DateTimeKind.Local).AddTicks(6924),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 12, 13, 39, 47, 315, DateTimeKind.Local).AddTicks(9324));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5fa627af-1cf8-412d-889c-2624d1c721df");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef52bc9b-d91e-4e87-bc77-bc095cdac775", "AQAAAAEAACcQAAAAEKfX8xCGqI0NnGYBJMlys04ufkd+Ju7jHd8cq9k3iyajVoxHb6xFEaBwURnrCAt8vw==" });
        }
    }
}
