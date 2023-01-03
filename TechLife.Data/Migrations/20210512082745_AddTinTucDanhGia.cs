using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTinTucDanhGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 128, DateTimeKind.Local).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 711, DateTimeKind.Local).AddTicks(7279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 402, DateTimeKind.Local).AddTicks(7687),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 958, DateTimeKind.Local).AddTicks(7700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 411, DateTimeKind.Local).AddTicks(9102),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 965, DateTimeKind.Local).AddTicks(7556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 133, DateTimeKind.Local).AddTicks(916),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 717, DateTimeKind.Local).AddTicks(9515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 351, DateTimeKind.Local).AddTicks(5713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 935, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.CreateTable(
                name: "TinTucDanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinTucId = table.Column<int>(nullable: false),
                    DiemDanhGia = table.Column<int>(nullable: false),
                    NgayDanhGia = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 378, DateTimeKind.Local).AddTicks(1490)),
                    UserName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTucDanhGia", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2a1caeab-b3fd-4148-9c5b-3577c5fade60");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d976664-248b-4136-ba8e-5efe48755a28", "AQAAAAEAACcQAAAAEI7M0yvJeWMkf9aXdB07zoYvvF8pRxsmOz7BsvwpgrGPHxhzyYIAZXunswvLnwYc9A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinTucDanhGia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 711, DateTimeKind.Local).AddTicks(7279),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 128, DateTimeKind.Local).AddTicks(1380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 958, DateTimeKind.Local).AddTicks(7700),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 402, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 965, DateTimeKind.Local).AddTicks(7556),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 411, DateTimeKind.Local).AddTicks(9102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 717, DateTimeKind.Local).AddTicks(9515),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 133, DateTimeKind.Local).AddTicks(916));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 12, 15, 14, 4, 935, DateTimeKind.Local).AddTicks(5099),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 12, 15, 27, 42, 351, DateTimeKind.Local).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f875ea0f-d97a-4db2-9eca-9ecf5365b5d1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "93ffb23c-2901-4de4-a423-44cbe1fdf0bd", "AQAAAAEAACcQAAAAELyaTehgCrUDt3NlJvYyo/iCdzKk/iCZvZD+EXnuaLTkw+3aEYKIFvwKUGR45SGpqA==" });
        }
    }
}
