using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddHoSoVanBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 749, DateTimeKind.Local).AddTicks(9853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 419, DateTimeKind.Local).AddTicks(7397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 751, DateTimeKind.Local).AddTicks(6924),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 430, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.CreateTable(
                name: "HoSoVanBan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HosoId = table.Column<int>(nullable: false),
                    TenGoi = table.Column<string>(nullable: true),
                    MaSo = table.Column<string>(nullable: true),
                    NoiCap = table.Column<string>(nullable: true),
                    NgayCap = table.Column<DateTime>(nullable: false),
                    NgayHetHan = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoVanBan", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoVanBan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 419, DateTimeKind.Local).AddTicks(7397),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 749, DateTimeKind.Local).AddTicks(9853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 10, 14, 57, 20, 430, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 11, 11, 8, 46, 751, DateTimeKind.Local).AddTicks(6924));

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
    }
}
