using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateHTCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 644, DateTimeKind.Local).AddTicks(478),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 607, DateTimeKind.Local).AddTicks(3379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 647, DateTimeKind.Local).AddTicks(4949),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 616, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 711, DateTimeKind.Local).AddTicks(7623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 839, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.AddColumn<int>(
                name: "ChuyenDiId",
                table: "HanhTrinhChuyenDi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "aedb6f09-a4e9-4d35-9ee7-9471deb40662");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "caeb7ae0-5d5d-44d5-91ff-0e8d8f55dff4", "AQAAAAEAACcQAAAAEPdmV0GWckOTycUI39jhjSPJ8UeMY7ys5kdldNJIUdIZTR0XW0RG1pCq5lDo0i8n1Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChuyenDiId",
                table: "HanhTrinhChuyenDi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 607, DateTimeKind.Local).AddTicks(3379),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 644, DateTimeKind.Local).AddTicks(478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 616, DateTimeKind.Local).AddTicks(999),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 647, DateTimeKind.Local).AddTicks(4949));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 16, 39, 42, 839, DateTimeKind.Local).AddTicks(9334),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 2, 20, 53, 32, 711, DateTimeKind.Local).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e8224302-6e36-4186-979a-4ad39ff11e10");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2c9d9819-2501-489a-9538-75a26b35b5d0", "AQAAAAEAACcQAAAAEH5TnAxDQlOhd0w8TDe1EIv3n5hvdpRMjSRW4U9tnqn9mvnZkDWftBS6vp8VqSfmFw==" });
        }
    }
}
