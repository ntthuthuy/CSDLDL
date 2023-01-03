using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateHoSoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 474, DateTimeKind.Local).AddTicks(5407),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 57, DateTimeKind.Local).AddTicks(2228));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 477, DateTimeKind.Local).AddTicks(3853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 61, DateTimeKind.Local).AddTicks(6267));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1efb5baa-d567-449c-b664-f29bfb3c6741");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eec534f7-9d81-4bba-baee-4b8bbcd4aaf0", "AQAAAAEAACcQAAAAENpoNEsLwIWfaip3hKTd0vMMTyiEPhbAqXI+qvk+G6OnNjcdf3jNEikoMiK+MDvO6w==" });

            migrationBuilder.CreateIndex(
                name: "IX_HoSo_LoaiHinhId",
                table: "HoSo",
                column: "LoaiHinhId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoSo_LoaiHinh_LoaiHinhId",
                table: "HoSo",
                column: "LoaiHinhId",
                principalTable: "LoaiHinh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoSo_LoaiHinh_LoaiHinhId",
                table: "HoSo");

            migrationBuilder.DropIndex(
                name: "IX_HoSo_LoaiHinhId",
                table: "HoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 57, DateTimeKind.Local).AddTicks(2228),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 474, DateTimeKind.Local).AddTicks(5407));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 10, 28, 48, 61, DateTimeKind.Local).AddTicks(6267),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 477, DateTimeKind.Local).AddTicks(3853));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0d01ae23-5ee7-4324-a567-f038ca95df57");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ad5e712-188a-47d0-8b6e-d3f4cd084a9c", "AQAAAAEAACcQAAAAEIR5rgfD/x8EUpsdLaDOdx10eqJasB77ZDbd8vB3DWD2Sf/xcxUVkEAjsnLhJUg49A==" });
        }
    }
}
