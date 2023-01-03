using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class AddTBLDiemDuLich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 31, DateTimeKind.Local).AddTicks(9657),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 279, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 33, DateTimeKind.Local).AddTicks(8808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 281, DateTimeKind.Local).AddTicks(9553));

            migrationBuilder.CreateTable(
                name: "DiemVeSinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViTri = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    IsStatus = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemVeSinh", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f217060c-e67e-4bc5-87e8-1bc64ad5e509");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d642b93-1648-49d5-ba9e-300359676ec7", "AQAAAAEAACcQAAAAEJDG/1XA+ED1VRGO15b5hTvmaKKUjVtQazWMzaj3ZTSIqL8KOTMvNBlyFFznVtOAHA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiemVeSinh");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 279, DateTimeKind.Local).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 31, DateTimeKind.Local).AddTicks(9657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 28, 13, 23, 8, 281, DateTimeKind.Local).AddTicks(9553),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 28, 14, 6, 8, 33, DateTimeKind.Local).AddTicks(8808));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f5c91f88-55f7-4678-9ca6-2751ab24b5f2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "437d3d7e-0938-4992-8d1c-188a5af7a884", "AQAAAAEAACcQAAAAEKrVzmhA0HydkTxATAA5QgiioohGJtk6LbVgGPcwsU9kDBhgjI1mc2OhP+CyzQv7CA==" });
        }
    }
}
