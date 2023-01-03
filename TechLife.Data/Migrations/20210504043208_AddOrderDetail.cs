using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 675, DateTimeKind.Local).AddTicks(7170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 455, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 750, DateTimeKind.Local).AddTicks(7563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 539, DateTimeKind.Local).AddTicks(4376));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoVaTen",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 753, DateTimeKind.Local).AddTicks(1182));

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 678, DateTimeKind.Local).AddTicks(8545),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 458, DateTimeKind.Local).AddTicks(5573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 741, DateTimeKind.Local).AddTicks(3417),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 527, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    DichVuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "edf73519-4cb9-4510-9df3-9d1f445e3d33");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "454db4b4-1b40-4b84-b142-3a2a93ad9e37", "AQAAAAEAACcQAAAAECUc2DcQjiAnRhXdNNh/zCFIdhUNYgDHhYDLwnefRU+ITPcyVDhZBQG4aa+lT/Y/uA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HoVaTen",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 455, DateTimeKind.Local).AddTicks(4317),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 675, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 539, DateTimeKind.Local).AddTicks(4376),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 750, DateTimeKind.Local).AddTicks(7563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 458, DateTimeKind.Local).AddTicks(5573),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 678, DateTimeKind.Local).AddTicks(8545));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 3, 16, 45, 13, 527, DateTimeKind.Local).AddTicks(9464),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 4, 11, 32, 7, 741, DateTimeKind.Local).AddTicks(3417));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a7a250d0-750c-462d-a723-0888551a84c7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "81309358-c290-425c-90b2-feca6b7ed363", "AQAAAAEAACcQAAAAEG3Oz2YAkluVqJsBxzXK9m6ovUZ3Apd4KpI6wfb8cf5nD8AqiQQ2A6qndLv+9cQfVQ==" });
        }
    }
}
