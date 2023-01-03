using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdtaeTinTuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 811, DateTimeKind.Local).AddTicks(9376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 832, DateTimeKind.Local).AddTicks(3686));

            migrationBuilder.AddColumn<bool>(
                name: "IsTinLeHoi",
                table: "TinTuc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDienRa",
                table: "TinTuc",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 815, DateTimeKind.Local).AddTicks(9815),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 835, DateTimeKind.Local).AddTicks(9069));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 895, DateTimeKind.Local).AddTicks(4758),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 938, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "dfc55db9-2eb5-4426-908a-bf4456bd347b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df9077df-3fb8-4ed1-b670-ac9d40e33749", "AQAAAAEAACcQAAAAELR+F4lix4qymfUFtCS3bUsEVSEUZIWtXFuc0ADSbVc3iojD/hQdJ/h1vMQCHFRZag==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTinLeHoi",
                table: "TinTuc");

            migrationBuilder.DropColumn(
                name: "NgayDienRa",
                table: "TinTuc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 832, DateTimeKind.Local).AddTicks(3686),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 811, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 835, DateTimeKind.Local).AddTicks(9069),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 815, DateTimeKind.Local).AddTicks(9815));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 28, 15, 52, 16, 938, DateTimeKind.Local).AddTicks(6532),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 1, 12, 18, 57, 895, DateTimeKind.Local).AddTicks(4758));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4a638f5d-8204-425b-a34a-e6280b37ad25");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e857627-93e1-48cd-90d8-de25dc03cac1", "AQAAAAEAACcQAAAAEHXwrsKYycECh8JBwtJ5fERKVyp82RJ5rMcf4u+AYACR/azs84gz9lCSuYCOlILGig==" });
        }
    }
}
