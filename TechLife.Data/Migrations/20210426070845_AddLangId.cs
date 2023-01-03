using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddLangId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 658, DateTimeKind.Local).AddTicks(4603),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 174, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 663, DateTimeKind.Local).AddTicks(4540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 178, DateTimeKind.Local).AddTicks(7896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 775, DateTimeKind.Local).AddTicks(3712),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 287, DateTimeKind.Local).AddTicks(7437));

            migrationBuilder.AddColumn<string>(
                name: "NgonNguId",
                table: "HoSo",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgonNguId",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 174, DateTimeKind.Local).AddTicks(9488),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 658, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 178, DateTimeKind.Local).AddTicks(7896),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 663, DateTimeKind.Local).AddTicks(4540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 23, 9, 43, 4, 287, DateTimeKind.Local).AddTicks(7437),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 26, 14, 8, 43, 775, DateTimeKind.Local).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2ca00f6e-9785-45b4-9201-c6c5b9686024");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b6e02a7-977c-4340-8e85-5a4b692e193b", "AQAAAAEAACcQAAAAED9lvfjnZnV8DctI+smhXm7k5xxVgbW4wXFmiSKdXRGlhGV4V1S8hz4+Tqr2xOLHxA==" });
        }
    }
}
