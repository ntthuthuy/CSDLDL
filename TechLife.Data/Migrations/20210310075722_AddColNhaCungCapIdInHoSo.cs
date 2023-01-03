using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddColNhaCungCapIdInHoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenCongTy",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 419, DateTimeKind.Local).AddTicks(7397),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 730, DateTimeKind.Local).AddTicks(8569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 430, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 735, DateTimeKind.Local).AddTicks(2374));

            migrationBuilder.AddColumn<int>(
                name: "NhaCungCapId",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2db7ac5b-d117-4afb-897a-396752a46e24");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f5e2617a-e8a4-408e-bb5d-3fe1dfbc3087", "AQAAAAEAACcQAAAAEL0pkijiyE9WEzk2BOIOx2vjPgH1GE9T8fT2SzQWVVLt0MgtYqExM7kbuTZpZyceBg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NhaCungCapId",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 730, DateTimeKind.Local).AddTicks(8569),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 419, DateTimeKind.Local).AddTicks(7397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 9, 3, 46, 735, DateTimeKind.Local).AddTicks(2374),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 430, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.AddColumn<string>(
                name: "TenCongTy",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "619efd92-daaf-4743-a8e8-00637e178f28");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f54bc29-80a4-48a2-9e26-df64971fbcea", "AQAAAAEAACcQAAAAEHUuzK0rKaWEpc4NBdpJ5Xuub33/ZpKNss7C1aZBZw/mdMi5T7Hw/mLMb0nt4TNPwA==" });
        }
    }
}
