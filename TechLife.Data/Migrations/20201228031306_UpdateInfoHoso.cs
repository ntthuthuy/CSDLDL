using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateInfoHoso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 808, DateTimeKind.Local).AddTicks(8966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 269, DateTimeKind.Local).AddTicks(2676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 811, DateTimeKind.Local).AddTicks(175),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 271, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.AddColumn<string>(
                name: "DonViCapPhep",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoCapPhep",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapPhep",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4f96dc7f-82bb-434a-9f31-933ecfdfe7a1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4f566c78-b3f1-4a47-abb9-03a1d17fefc6", "AQAAAAEAACcQAAAAENz+crWfuWcwmZ5LzIw51ejoJCoRuPcwqyKW6Yf3Qce/zb9u7P0AiLIMkk5uBOtQ6A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViCapPhep",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MaSoCapPhep",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayCapPhep",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 269, DateTimeKind.Local).AddTicks(2676),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 808, DateTimeKind.Local).AddTicks(8966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 24, 15, 28, 50, 271, DateTimeKind.Local).AddTicks(5213),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 10, 13, 5, 811, DateTimeKind.Local).AddTicks(175));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "262cb54e-7213-4d9e-99ea-d4616a0e9621");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "51e67e6f-5328-437c-8359-d262ffbcc2cc", "AQAAAAEAACcQAAAAECSgvQiGkkOftOcezyegKHhkzFMiJ3B26WTTOrZElVQ4eDa48vuS8wk6UaZayT/YkQ==" });
        }
    }
}
