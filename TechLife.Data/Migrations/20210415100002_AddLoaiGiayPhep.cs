using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddLoaiGiayPhep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 279, DateTimeKind.Local).AddTicks(2464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 828, DateTimeKind.Local).AddTicks(4200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 284, DateTimeKind.Local).AddTicks(3370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 832, DateTimeKind.Local).AddTicks(8174));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 380, DateTimeKind.Local).AddTicks(5771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 949, DateTimeKind.Local).AddTicks(2726));

            migrationBuilder.CreateTable(
                name: "GiayPhep",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: true),
                    LinhVucId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiayPhep", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "38506f3b-b864-40c2-8d9a-d61320185b93");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ebf6087-1b73-499b-b521-c462ffe09cf6", "AQAAAAEAACcQAAAAEGhFe6PUimC89gOOmzQRlNjv/A6IB09ON/mijMWdEFaRk54dFOlAIpeiG2ZK7sUBRw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiayPhep");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 828, DateTimeKind.Local).AddTicks(4200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 279, DateTimeKind.Local).AddTicks(2464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 832, DateTimeKind.Local).AddTicks(8174),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 284, DateTimeKind.Local).AddTicks(3370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 15, 15, 26, 3, 949, DateTimeKind.Local).AddTicks(2726),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 15, 17, 0, 1, 380, DateTimeKind.Local).AddTicks(5771));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c54dc4a1-4980-4c57-a0ab-b998731d4509");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5bbeff8b-8a75-4b57-84c0-cc4d18435c7a", "AQAAAAEAACcQAAAAEIn5PepzNakou0F1Mgqp9FXEurePU9d5RRBeQJF1i9y1PmvTZLXyZdBUxSUbvOXCKQ==" });
        }
    }
}
