using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddPhongBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 20, DateTimeKind.Local).AddTicks(8396),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 172, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 26, DateTimeKind.Local).AddTicks(321),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 176, DateTimeKind.Local).AddTicks(7294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 132, DateTimeKind.Local).AddTicks(9141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 298, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(nullable: false),
                    MaDinhDanh = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9c56d404-40c2-4ce7-a6fa-75e5fe627bc8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ca6ddee-1fb6-44ba-b096-c065902232db", "AQAAAAEAACcQAAAAEOFoA4y/EKuxAdPIi9d6gEuZxGPWhcOByLaprxgM5B21n4rsklHJcxLufEgQWupghg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 172, DateTimeKind.Local).AddTicks(4430),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 20, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 176, DateTimeKind.Local).AddTicks(7294),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 26, DateTimeKind.Local).AddTicks(321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 6, 9, 12, 23, 298, DateTimeKind.Local).AddTicks(448),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 11, 19, 52, 38, 132, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1a6c60ce-04aa-450a-94ca-4506071ab4f6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3de2c25-c9d6-446b-91b1-ce4e8112e169", "AQAAAAEAACcQAAAAED2deI1R0jz2IE2QQlcTu6XT6FbCkYrFH7BVPL/nmgy+PLI/1ke2jHh6pLc7F1JkLw==" });
        }
    }
}
